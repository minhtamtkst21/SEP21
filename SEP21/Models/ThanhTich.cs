//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SEP21.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ThanhTich
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ThanhTich()
        {
            this.DatThanhTiches = new HashSet<DatThanhTich>();
        }
    
        public int ID { get; set; }
        public string TenThanhTich { get; set; }
        public int NguoiDatThanhTich { get; set; }
        public string NoiDung { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DatThanhTich> DatThanhTiches { get; set; }
    }
}
