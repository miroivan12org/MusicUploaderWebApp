using MusiUploaderWeb.Interfaces;
using MusiUploaderWeb.Models.DB;
using MusiUploaderWeb.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusiUploaderWeb.Models.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MusicEntities context = new MusicEntities();

        public IQueryable<User> GetAll()
        {
            var users = context.Users;
            return users;
        }

        public User GetSingle(int userID)
        {
            var user = this.GetAll().FirstOrDefault(x => x.UserID == userID);
            return user;
        }

        public bool UserExists(string userName)
        {
            var exists = context.Users.Where(o => o.LoginName.Equals(userName)).Any();
            return exists;
        }

        public string GetPassword(string userName)
        {
            var password = string.Empty;
            var user = context.Users.Where(u => u.LoginName.ToLower().Equals(userName));

            if (user.Any())
                password = user.FirstOrDefault().PasswordEncryptedText;
            return password;
        }

        public void Add(User user)
        {
            context.Users.Add(user);
        }

        public void Delete(User user)
        {
            context.Users.Remove(user);
        }

        public void Edit(User user)
        {
            context.Entry<User>(user).State = System.Data.Entity.EntityState.Modified;
        }

        public void Add(UserProfile userProfile)
        {
            context.UserProfiles.Add(userProfile);
        }

        public void Add(UserRole userRole)
        {
            context.UserRoles.Add(userRole);
        }

        public User GetByUserName(string username)
        {
            return context.Users.Where(u => u.LoginName.ToLower().Equals(username)).FirstOrDefault();
        }

        public string GetUserRole(int userID)
        {
            var role = from q in context.UserRoles
                       join r in context.LookupRoles on q.LookupRoleID equals r.LookupRoleID
                       where q.UserID.Equals(userID)
                       select r.RoleName;
            return role.FirstOrDefault();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public UserProfile GetUserProfileByUserID(int userID)
        {
            var userProfile = context.UserProfiles.Where(p => p.UserID == userID).FirstOrDefault();
            return userProfile;
        }
        public UserRole GetUserRoleByUserID(int userID)
        {
            var userRole = context.UserRoles.Where(r => r.UserID == userID).FirstOrDefault();
            return userRole;
        }

        //radimo u transakciji jer moramo napraviti update tri stvari
        public void UpdateUser(UserSignUpView user)
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    var oldUser = context.Users.Find(user.UserID);
                    oldUser.LoginName = user.LoginName;
                    oldUser.PasswordEncryptedText = user.Password;
                    oldUser.RowModifiedUserID = user.UserID;
                    oldUser.RowMOdifiedDateTime = DateTime.Now;

                    context.SaveChanges();

                    var oldUserProfile = context.UserProfiles.Where(p => p.UserID == user.UserID);
                    if (oldUserProfile.Any())
                    {
                        var userProfile = oldUserProfile.FirstOrDefault();
                        userProfile.UserID = user.UserID;
                        userProfile.FirstName = user.FirstName;
                        userProfile.LastName = user.LastName;
                        userProfile.Gender = user.Gender;
                        userProfile.RowModifiedUserID = user.UserID;
                        userProfile.RowModifiedDateTime = DateTime.Now;

                        context.SaveChanges();
                    }

                    if (user.LookupRoleID > 0)
                    {
                        var oldUserRole = context.UserRoles.Where(o => o.UserID == user.UserID);
                        UserRole userRole = null;
                        if (oldUserRole.Any())
                        {
                            userRole = oldUserRole.FirstOrDefault();
                            userRole.LookupRoleID = user.LookupRoleID;
                            userRole.UserID = user.UserID;
                            userRole.IsActive = true;
                            userRole.RowModifiedUserID = user.UserID;
                            userRole.RowModifiedDateTime = DateTime.Now;
                        }
                        else
                        {
                            userRole = new UserRole();
                            userRole.LookupRoleID = user.LookupRoleID;
                            userRole.UserID = user.UserID;
                            userRole.IsActive = true;
                            userRole.RowCreatedUserID = user.UserID;
                            userRole.RowModifiedUserID = user.UserID;
                            userRole.RowCreatedDateTime = DateTime.Now;
                            userRole.RowModifiedDateTime = DateTime.Now;
                            context.UserRoles.Add(userRole);
                        }
                        context.SaveChanges();
                    }
                    dbContextTransaction.Commit();
                }
                catch
                {
                    dbContextTransaction.Rollback();
                }
            }
        }

        public void DeleteUser(int userID)
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    var userRole = context.UserRoles.Where(r => r.UserID == userID).FirstOrDefault();
                    if (userRole != null)
                    {
                        context.UserRoles.Remove(userRole);
                        context.SaveChanges();
                    }

                    var userProfile = context.UserProfiles.Where(p => p.UserID == userID).FirstOrDefault();
                    if (userProfile != null)
                    {
                        context.UserProfiles.Remove(userProfile);
                        context.SaveChanges();
                    }

                    var user = context.Users.Where(u => u.UserID == userID).FirstOrDefault();
                    if (user != null)
                    {
                        context.Users.Remove(user);
                        context.SaveChanges();
                    }
                    dbContextTransaction.Commit();
                }
                catch
                {
                    dbContextTransaction.Rollback();
                }
            }
        }

        public UserSignUpView GetUserProfileView(int userID)
        {
            var userSignUpView = new UserSignUpView();
            var user = context.Users.Find(userID);
            if (user != null)
            {
                userSignUpView.UserID = user.UserID;
                userSignUpView.LoginName = user.LoginName;
                userSignUpView.Password = user.PasswordEncryptedText;

                var userProfile = context.UserProfiles.Where(p => p.UserID == userID).FirstOrDefault();
                if (userProfile != null)
                {
                    userSignUpView.FirstName = userProfile.FirstName;
                    userSignUpView.LastName = userProfile.LastName;
                    userSignUpView.Gender = userProfile.Gender;
                }

                var userRole = context.UserRoles.Where(r => r.UserID == userID).FirstOrDefault();
                if (userRole != null)
                {
                    var lookupRoleName = context.LookupRoles.Where(l => l.LookupRoleID == userRole.LookupRoleID).FirstOrDefault().RoleName;
                    userSignUpView.LookupRoleID = userRole.LookupRoleID;
                    userSignUpView.RoleName = lookupRoleName;
                    userSignUpView.IsRoleActive = userRole.IsActive;
                }
            }
            return userSignUpView;
        }
    }
}