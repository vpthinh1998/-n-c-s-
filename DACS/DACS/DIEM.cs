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
    
    public partial class DIEM
    {
        public int IDSTUDENT { get; set; }
        public int IDMONHOC { get; set; }
        public Nullable<double> DIEM_TH { get; set; }
        public Nullable<double> DIEM_LT { get; set; }
        public Nullable<double> DIEM_TB { get; set; }
        public Nullable<double> DAT { get; set; }
        public Nullable<double> STC { get; set; }
    
        public virtual MONHOC MONHOC { get; set; }
        public virtual STUDENT STUDENT { get; set; }
    }
}
