using MusiUploaderWeb.App_GlobalResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusiUploaderWeb.Models.ViewModel
{
    public class UserSignUpView
    {
        [Key]
        public int UserID { get; set; }
        public int LookupRoleID { get; set; }
        public string RoleName { get; set; }
        public bool? IsRoleActive { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(RGlobal))]
        [Display(Name = "Username", ResourceType = typeof(RHome))]
        public string LoginName { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(RGlobal))]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(RHome))]
        public string Password { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string Gender { get; set; }
    }

    public class UserLoginView
    {
        [Key]
        public int UserID { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(RGlobal))]
        [Display(Name = "Username", ResourceType = typeof(RHome))]
        public string LoginName { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(RGlobal))]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(RHome))]
        public string Password { get; set; }
    }

    public class LookupAvailableRole
    {
        [Key]
        public int LookupRoleID { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
    }

    public class Gender
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }
    public class UserRoles
    {
        public int? SelectedRoleID { get; set; }
        public IEnumerable<LookupAvailableRole> UserRoleList { get; set; }
    }

    public class UserGender
    {
        public string SelectedGender { get; set; }
        public IEnumerable<Gender> Gender { get; set; }
    }
    public class UserDataView
    {
        public IEnumerable<UserSignUpView> UserProfile { get; set; }
        public UserRoles UserRoles { get; set; }
        public UserGender UserGender { get; set; }
    }
}