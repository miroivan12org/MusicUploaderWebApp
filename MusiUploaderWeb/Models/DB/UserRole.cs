//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MusiUploaderWeb.Models.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserRole
    {
        public int UserRoleID { get; set; }
        public int UserID { get; set; }
        public int LookupRoleID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public int RowCreatedUserID { get; set; }
        public Nullable<System.DateTime> RowCreatedDateTime { get; set; }
        public int RowModifiedUserID { get; set; }
        public Nullable<System.DateTime> RowModifiedDateTime { get; set; }
    
        public virtual LookupRole LookupRole { get; set; }
        public virtual User User { get; set; }
    }
}
