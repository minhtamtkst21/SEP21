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
    
    public partial class NhanVienKhoa
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NhanVienKhoa()
        {
            this.BaiViets = new HashSet<BaiViet>();
            this.NguoiThucHienNCKHs = new HashSet<NguoiThucHienNCKH>();
        }
    
        public int ID { get; set; }
        public string HoTen { get; set; }
        public string ChucVu { get; set; }
        public Nullable<int> Khoa { get; set; }
        public string Mail { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BaiViet> BaiViets { get; set; }
        public virtual Khoa Khoa1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NguoiThucHienNCKH> NguoiThucHienNCKHs { get; set; }
    }
}
