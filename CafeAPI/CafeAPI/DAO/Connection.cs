using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;

namespace CafeAPI.DAO
{
    class Connection
    {
        private string strconn = @"data source=.;initial catalog=QuanCafe;user id=sa;password=sa;multipleactiveresultsets=True;";
        private SqlConnection conn;

        public SqlConnection Conn
        {
            get { return conn; }
            set { conn = value; }
        }
        public string Strconn
        {
            get { return strconn; }
            set { strconn = value; }
        }
        public Connection()
        {
            this.Conn = new SqlConnection(this.Strconn);
            //this.Conn.Open();
        }

        public DataTable LoadTable(string sql)
        {
            this.Conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(sql, Conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            this.Conn.Close();
            return dt;
        }
        
        public bool Execute(string query)
        {
            this.Conn.Open();
            SqlCommand cmd = new SqlCommand(query, Conn);
            try
            {
                cmd.ExecuteNonQuery();
                this.Conn.Close();
                return true;
            }
            catch { this.Conn.Close(); return false; }
        }
        public int Excute_Sql(string strQuery, CommandType cmdtype, string[] para, object[] values)
        {
            this.Conn.Open();
            int efftectRecord = 0;
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.CommandText = strQuery;
            sqlcmd.Connection = Conn;
            sqlcmd.CommandType = cmdtype;
            SqlParameter sqlpara;
            for (int i = 0; i < para.Length; i++)
            {
                sqlpara = new SqlParameter(para[i], values[i]);
                sqlcmd.Parameters.Add(sqlpara);
            }
            try
            {
                efftectRecord = sqlcmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error:" + ex.Message);
            }
            this.Conn.Close();
            return efftectRecord;
        }
        public DataTable FillDataTable(string strQuery, CommandType cmdtype)
        {
            this.Conn.Open();
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = strQuery;
                cmd.CommandType = cmdtype;
                cmd.Connection = Conn;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                da.Dispose();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error: " + ex.Message);
            }
            this.Conn.Close();
            return dt;
        }
        public DataTable FillDataTable(string strQuery, CommandType cmdtype, string[] para, object[] values)
        {
            this.Conn.Open();
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = strQuery;
                cmd.CommandType = cmdtype;
                cmd.Connection = Conn;
                SqlParameter sqlpara;
                for (int i = 0; i < para.Length; i++)
                {
                    sqlpara = new SqlParameter();
                    sqlpara.ParameterName = para[i];
                    sqlpara.SqlValue = values[i];
                    cmd.Parameters.Add(sqlpara);
                }
                SqlDataAdapter sqlda = new SqlDataAdapter(cmd);
                sqlda.Fill(dt);
                sqlda.Dispose();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error: " + ex.Message);
            }
            this.Conn.Close();
            return dt;
        }
        public List<T> ConvertToList<T>(DataTable datatable) where T : new()
        {
            List<T> Temp = new List<T>();
            try
            {
                List<string> columnsNames = new List<string>();
                foreach (DataColumn DataColumn in datatable.Columns)
                    columnsNames.Add(DataColumn.ColumnName);
                Temp = datatable.AsEnumerable().ToList().ConvertAll<T>(row => getObject<T>(row, columnsNames));
                return Temp;
            }
            catch
            {
                return Temp;
            }

        }
        public T getObject<T>(DataRow row, List<string> columnsName) where T : new()
        {
            T obj = new T();
            try
            {
                string columnname = "";
                string value = "";
                PropertyInfo[] Properties;
                Properties = typeof(T).GetProperties();
                foreach (PropertyInfo objProperty in Properties)
                {
                    columnname = columnsName.Find(name => name.ToLower() == objProperty.Name.ToLower());
                    if (!string.IsNullOrEmpty(columnname))
                    {
                        value = row[columnname].ToString();
                        if (!string.IsNullOrEmpty(value))
                        {
                            if (Nullable.GetUnderlyingType(objProperty.PropertyType) != null)
                            {
                                value = row[columnname].ToString().Replace("$", "").Replace(",", "");
                                objProperty.SetValue(obj, Convert.ChangeType(value, Type.GetType(Nullable.GetUnderlyingType(objProperty.PropertyType).ToString())), null);
                            }
                            else
                            {
                                value = row[columnname].ToString().Replace("%", "");
                                objProperty.SetValue(obj, Convert.ChangeType(value, Type.GetType(objProperty.PropertyType.ToString())), null);
                            }
                        }
                    }
                }
                return obj;
            }
            catch
            {
                return obj;
            }
        }
    }
}
