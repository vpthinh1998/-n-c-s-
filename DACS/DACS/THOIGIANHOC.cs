//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DACS
{
    using System;
    using System.Collections.Generic;
    
    public partial class THOIGIANHOC
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public THOIGIANHOC()
        {
            this.LICHHOCs = new HashSet<LICHHOC>();
        }
    
        public int IDTHOIGIAN { get; set; }
        public Nullable<System.DateTime> THOAIGIANBATDAU { get; set; }
        public Nullable<System.DateTime> THOIGIANKETTHUC { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LICHHOC> LICHHOCs { get; set; }
    }
}