using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OgrenciBilgiIstemi.Models;

namespace OgrenciBilgiIstemi.App_Start
{
    public class studentSearch
    {
        public List<Table_OGRENCI> OgrenciList { get; set; }

        public string SearchKey { get; set; }
    }
}