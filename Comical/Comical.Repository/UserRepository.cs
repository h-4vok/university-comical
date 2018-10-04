using Comical.Models;
using Comical.Models.Common;
using DBNostalgia;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comical.Repository
{
    public class UserRepository : BaseRepository
    {
        protected void InsertUserRoles(User model)
        {
            foreach (var role in model.Roles)
            {
                var id = this.UnitOfWork.Scalar(
                    "UserRole_new",
                    ParametersBuilder.With("UserId", model.Id)
                    .And("RoleId", role.Id)
                ).AsInt();

                this.SetHorizontalVerifier(id, "UserRole");
            }

            this.SetVerticalVerifier("UserRole");
        }

        public int New(User model)
        {
            var output = this.UnitOfWork.Run(() =>
            {
                model.Id = this.UnitOfWork.Scalar(
                    "User_new",
                    ParametersBuilder.With("Login", model.Login)
                    .And("Password", model.Password)
                ).AsInt();

                this.SetHorizontalVerifier(model.Id);
                this.SetVerticalVerifier();

                this.InsertUserRoles(model);

                return model.Id;

            }).AsInt();

            return output;
        } 

        public void Update(User model)
        {
            this.UnitOfWork.Run(() =>
            {
                this.UnitOfWork.NonQuery(
                    "UserRole_delete",
                    ParametersBuilder.With("userId", model.Id)
                );

                this.InsertUserRoles(model);

                this.UnitOfWork.NonQuery(
                    "User_update",
                    ParametersBuilder.With("id", model.Id)
                    .And("Login", model.Login)
                    .And("Password", model.Password)
                    .And("Enabled", model.Enabled)
                    .And("Blocked", model.Blocked)
                );

                this.SetHorizontalVerifier(model.Id);
                this.SetVerticalVerifier();
            });
        }

        public int IncrementRetry(int id)
        {
            var output = this.UnitOfWork.Run(() =>
            {
                var retries = this.UnitOfWork.Scalar(
                    "User_incrementRetry",
                    ParametersBuilder.With("id", id)
                ).AsInt();

                this.SetHorizontalVerifier(id);
                this.SetVerticalVerifier();

                return retries;
            });

            return output;
        }

        public void ChangeEnabled(int id, bool value)
        {
            this.UnitOfWork.Run(() =>
            {
                this.UnitOfWork.NonQuery(
                    "User_changeEnabled",
                    ParametersBuilder.With("id", id)
                    .And("Enabled", value)
                );

                this.SetHorizontalVerifier(id);
                this.SetVerticalVerifier();
            });
        }

        public void ChangeBlocked(int id, bool value)
        {
            this.UnitOfWork.Run(() =>
            {
                this.UnitOfWork.NonQuery(
                    "User_changeBlocked",
                    ParametersBuilder.With("id", id)
                    .And("Blocked", value)
                );

                this.SetHorizontalVerifier(id);
                this.SetVerticalVerifier();
            });
        }

        public User Get(int id)
        {
            var roleRepository = new RoleRepository();

            var output = this.UnitOfWork.Run(() =>
            {
                var model = this.UnitOfWork.Get(
                    "User_getById",
                    this.FetchUser,
                    ParametersBuilder.With("id", id)
                ).First();

                model.Roles = this.UnitOfWork.Get(
                    "User_getRoles",
                    roleRepository.FetchRole,
                    ParametersBuilder.With("userId", id)
                ).ToList();

                return model;
            });

            return output;
        }

        public User GetByLogin(string login)
        {
            var output = this.UnitOfWork.GetDirect(
                "User_getByLogin",
                this.FetchUser,
                ParametersBuilder.With("Login", login)
            ).FirstOrDefault();

            return output;
        }

        public IEnumerable<User> Get()
        {
            var models = this.UnitOfWork.GetDirect(
                "User_get",
                this.FetchUser
            ).ToList();

            return models;
        }

        internal User FetchUser(IDataReader reader)
        {
            var model = new User
            {
                Id = reader.GetInt32("Id"),
                Login = reader.GetString("Login"),
                Password = reader.GetString("Password"),
                Retries = reader.GetInt32("Retries"),
                Enabled = reader.GetBoolean("Enabled"),
                Blocked = reader.GetBoolean("Blocked")
            };

            return model;
        }
    }
}
