using MusiUploaderWeb.Models.EntityManager;
using MusiUploaderWeb.Models.ViewModel;
using MusiUploaderWeb.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusiUploaderWeb.Controllers
{
    public class HomeController : BaseController
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [AuthorizeRoles("Admin")]
        public ActionResult ManageUserPartial(string status = "")
        {
            if (User.Identity.IsAuthenticated)
            {
                string loginName = User.Identity.Name;
                var userManager = new UserManager();
                var dataView = userManager.GetUserDataView(loginName);

                var message = string.Empty;
                if (status.Equals("update"))
                    message = "Update Successful";
                else if (status.Equals("delete"))
                    message = "Delete Successful";
                else if (status.Equals("failed"))
                    message = "Update/Delete failed";

                ViewBag.Message = message;

                return PartialView(dataView);
            }
            return View();
        }

        [AuthorizeRoles("Admin")]
        public ActionResult ManageUsers()
        {
            return View();
        }

        [AuthorizeRoles("Admin")]
        public ActionResult UpdateUserData(int userID, string loginName, string password, string firstName, string lastName, string gender, int roleID = 0)
        {
            var success = false;
            var userProfileView = new UserSignUpView();
            userProfileView.UserID = userID;
            userProfileView.LoginName = loginName;
            userProfileView.Password = password;
            userProfileView.FirstName = firstName;
            userProfileView.LastName = lastName;
            userProfileView.Gender = gender;

            if (roleID > 0)
                userProfileView.LookupRoleID = roleID;

            try
            {
                var userManager = new UserManager();
                userManager.UpdateUserAccount(userProfileView);
                success = true;
            }
            catch
            {

            }

            return Json(new { success });
        }

        [AuthorizeRoles("Admin")]
        public ActionResult DeleteUser(int userID)
        {
            var success = false;

            try
            {
                var userManager = new UserManager();
                userManager.DeleteUser(userID);
                success = true;
            }
            catch
            {

            }

            return Json(new { success });
        }


        public ActionResult UnAuthorized()
        {
            return View();
        }
    }
}