//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Nhom6_DoAn_
{
    using System;
    using System.Collections.Generic;
    
    public partial class PHONGCHIEU
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PHONGCHIEU()
        {
            this.SUATCHIEUx = new HashSet<SUATCHIEU>();
        }
    
        public string MAPHONG { get; set; }
        public string TENPHONG { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SUATCHIEU> SUATCHIEUx { get; set; }
    }
}
