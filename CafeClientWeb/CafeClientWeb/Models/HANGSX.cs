using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CafeClientWeb.Models
{
    public class HANGSX
    {
        public int ID { get; set; }
        public string TEN { get; set; }
        public HANGSX() { }
        public HANGSX(string TEN)
        {
            this.TEN = TEN;
        }
    }
}