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
    
    public partial class LICHHOC
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LICHHOC()
        {
            this.LOPHOCs = new HashSet<LOPHOC>();
        }
    
        public int IDTHOIGIAN { get; set; }
        public int IDPHONGHOC { get; set; }
    
        public virtual PHONGHOC PHONGHOC { get; set; }
        public virtual THOIGIANHOC THOIGIANHOC { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LOPHOC> LOPHOCs { get; set; }
    }
}
