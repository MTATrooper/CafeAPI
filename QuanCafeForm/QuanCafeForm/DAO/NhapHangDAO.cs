using QuanCafeForm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanCafeForm.DAO
{
    class NhapHangDAO
    {
        private ConnectAPI cn = new ConnectAPI();
        public void updateNHAPHANG(CHITIETNHAPHANG CTNH)
        {
            cn.Put<CHITIETNHAPHANG>("api/CHITIETNHAPHANG/", CTNH);
        }

        public CHITIETNHAPHANG GetCTNHbyId(int id)
        {
            return cn.GetObject<CHITIETNHAPHANG>("api/CHITIETNHAPHANG/" + id);
        }
    }
}
