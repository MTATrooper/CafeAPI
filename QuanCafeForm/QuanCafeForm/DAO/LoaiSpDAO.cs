using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanCafeForm.Models;

namespace QuanCafeForm.DAO
{
    class LoaiSpDAO
    {
        private ConnectAPI cnAPI = new ConnectAPI();
        public List<LOAISP> getListLOAISP()
        {
            List<LOAISP> lst = cnAPI.GetObject<List<LOAISP>>("api/LOAISP/");
            return lst;
        }
        public LOAISP getLOAISPById(int id)
        {
            LOAISP lst = cnAPI.GetObject<LOAISP>("api/LOAISP/" + id);
            return lst;
        }
        public void updateLOAISP(LOAISP lsp)
        {
            cnAPI.Put<LOAISP>("api/LOAISP", lsp);
        }
        public void insertLOAISP(LOAISP lsp)
        {
            cnAPI.Post<LOAISP>("api/LOAISP", lsp);
        }
    }
}
