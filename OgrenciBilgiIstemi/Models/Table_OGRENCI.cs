//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OgrenciBilgiIstemi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Table_OGRENCI
    {
        public int ID { get; set; }
        public string AD { get; set; }
        public string SOYAD { get; set; }
        public string NUMARA { get; set; }
        public string TC { get; set; }
        public Nullable<int> OKUL { get; set; }
        public Nullable<int> SINIFI { get; set; }
        public Nullable<bool> SILINDI { get; set; }
    
        public virtual Table_OKUL Table_OKUL { get; set; }
        public virtual Table_SINIF Table_SINIF { get; set; }
    }
}
