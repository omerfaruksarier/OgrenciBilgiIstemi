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
    
    public partial class Table_SINIF
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Table_SINIF()
        {
            this.Table_OGRENCI = new HashSet<Table_OGRENCI>();
        }
    
        public int ID { get; set; }
        public Nullable<int> SINIF { get; set; }
        public Nullable<bool> SILINDI { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Table_OGRENCI> Table_OGRENCI { get; set; }
    }
}