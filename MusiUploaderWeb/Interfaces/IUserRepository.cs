using MusiUploaderWeb.Models.DB;
using MusiUploaderWeb.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusiUploaderWeb.Interfaces
{
    public interface IUserRepository
    {
        IQueryable<User> GetAll();
        User GetSingle(int userID);
        void Add(User user);
        void Add(UserProfile userProfile);
        void Add(UserRole userRole);
        void Edit(User user);
        void Delete(User user);
        void Save();
        bool UserExists(string userName);
        string GetPassword(string userName);
        User GetByUserName(string userName);
        string GetUserRole(int userID);
        UserProfile GetUserProfileByUserID(int userID);
        UserRole GetUserRoleByUserID(int userID);
        void UpdateUser(UserSignUpView user);
        void DeleteUser(int userID);
        UserSignUpView GetUserProfileView(int userID);

    }
}