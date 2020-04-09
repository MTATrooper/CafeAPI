using QuanCafeForm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanCafeForm.DAO
{
    class HangSxDAO
    {
        private ConnectAPI cnAPI = new ConnectAPI();
        public List<HANGSX> getListHANGSX()
        {
            List<HANGSX> lst = cnAPI.GetObject<List<HANGSX>>("api/HANGSX/");
            return lst;
        }
    }
}
