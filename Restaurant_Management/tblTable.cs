//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Restaurant_Management
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblTable
    {
        public int Id { get; set; }
        public string TableNumber { get; set; }
        public Nullable<int> Seats { get; set; }
        public string Discription { get; set; }
        public Nullable<int> BranchId { get; set; }
    
        public virtual tblBranch tblBranch { get; set; }
    }
}