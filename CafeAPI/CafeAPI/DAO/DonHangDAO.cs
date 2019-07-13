using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CafeAPI.DAO
{
    public class DonHangDAO
    {
        public int[] ConvertStringToArray(string s)
        {
            string[] a = s.Split(',');
            int[] so = new int[a.Length];
            for(int i = 0; i<a.Length; i++)
            {
                so[i] = Convert.ToInt32(a[i].Trim());
            }
            return so;
        }
    }
}