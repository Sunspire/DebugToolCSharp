using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SQLite;
using DebugToolCSharp.Models;

namespace DebugToolCSharp.Classes
{
    public class Queries
    {
        public static bool GetLogin(string login, string pwd)
        {
            var dbObject = new Database();
            var q = "select count(1) as mycount from users where login=@login and password=@pwd";

            SQLiteCommand cmd = new SQLiteCommand(q, dbObject.Connection);
            dbObject.OpenConnection();
            cmd.Parameters.AddWithValue("@login", login);
            cmd.Parameters.AddWithValue("@pwd", pwd);
            SQLiteDataReader result = cmd.ExecuteReader();

            if (result.HasRows)
            {
                while (result.Read())
                {
                    if (int.Parse(result["mycount"].ToString()) == 1)
                    {
                        dbObject.CloseConnection();
                        return true;
                    }
                }
            }
            dbObject.CloseConnection();
            return false;
        }

        public static LoginModel GetUserDetails(string login, string pwd)
        {
            var loginModel = new LoginModel();

            var dbObject = new Database();
            var q = "select * from users where login=@login and password=@pwd";

            SQLiteCommand cmd = new SQLiteCommand(q, dbObject.Connection);
            dbObject.OpenConnection();
            cmd.Parameters.AddWithValue("@login", login);
            cmd.Parameters.AddWithValue("@pwd", pwd);
            SQLiteDataReader result = cmd.ExecuteReader();

            if (result.HasRows)
            {
                while (result.Read())
                {
                    loginModel.Id = int.Parse(result["id"].ToString());
                    loginModel.Name = result["name"].ToString();
                }
            }
            dbObject.CloseConnection();

            return loginModel;
        }

        public static string AddRole(string roleName) 
        {
            var dbObject = new Database();
            var q = "select * from roles where role=@roleName";

            using (SQLiteCommand cmd = new SQLiteCommand(q, dbObject.Connection)) 
            {
                dbObject.OpenConnection();
                cmd.Parameters.AddWithValue("@roleName", roleName);
                SQLiteDataReader result = cmd.ExecuteReader();

                if (result.HasRows)
                {
                    dbObject.CloseConnection();
                    return "This role already exists";
                }
            }                

            q = "select (max(id) + 1) as [maxId] from roles";
            int maxId = 1;
            using (SQLiteCommand cmd = new SQLiteCommand(q, dbObject.Connection)) 
            {
                dbObject.OpenConnection();
                SQLiteDataReader result = cmd.ExecuteReader();

                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        var resultMID = result["maxId"].ToString();
                        if (!string.IsNullOrEmpty(resultMID))
                        {
                            maxId = int.Parse(resultMID);
                        }
                    }
                }
            }

            q = "insert into roles values(@id,@roleName)";
            using (SQLiteCommand cmd = new SQLiteCommand(q, dbObject.Connection)) 
            {
                dbObject.OpenConnection();
                cmd.Parameters.AddWithValue("@id", maxId);
                cmd.Parameters.AddWithValue("@roleName", roleName);
                cmd.ExecuteNonQuery();
            }
            dbObject.CloseConnection();

            return string.Empty;
        }

        public static List<Roles> GetAllRoles()
        {
            var dbObject = new Database();
            var listRoles = new List<Roles>();
            var q = "select * from roles order by role";

            using (SQLiteCommand cmd = new SQLiteCommand(q, dbObject.Connection))
            {
                dbObject.OpenConnection();
                SQLiteDataReader result = cmd.ExecuteReader();

                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        listRoles.Add(new Roles() { Id = int.Parse(result["id"].ToString()), Role = result["role"].ToString() });
                    }
                }
            }
            dbObject.CloseConnection();
            return listRoles; 
        }

        public string AddUser(UserManagement userManagement) 
        {
            var dbObject = new Database();
            var q = "select count(1) as mycount from users where login=@login";

            using (SQLiteCommand cmd = new SQLiteCommand(q, dbObject.Connection))
            {
                dbObject.OpenConnection();
                cmd.Parameters.AddWithValue("@login", userManagement.UserLogin);
                SQLiteDataReader result = cmd.ExecuteReader();

                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        if (int.Parse(result["mycount"].ToString()) == 1)
                        {
                            dbObject.CloseConnection();
                            return "This login already exists";
                        }
                    }
                }
            }

            q = "select (max(id) + 1) as [maxId] from users";
            int userId = 1;
            using (SQLiteCommand cmd = new SQLiteCommand(q, dbObject.Connection))
            {
                dbObject.OpenConnection();
                SQLiteDataReader result = cmd.ExecuteReader();

                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        var resultMID = result["maxId"].ToString();
                        if (!string.IsNullOrEmpty(resultMID))
                        {
                            userId = int.Parse(resultMID);
                        }
                    }
                }
            }
            dbObject.CloseConnection();

            q = "insert into users values(@id, @name, @login, @password)";
            using (SQLiteCommand cmd = new SQLiteCommand(q, dbObject.Connection))
            {
                dbObject.OpenConnection();
                cmd.Parameters.AddWithValue("@id", userId);
                cmd.Parameters.AddWithValue("@name", userManagement.UserName);
                cmd.Parameters.AddWithValue("@login", userManagement.UserLogin);
                cmd.Parameters.AddWithValue("@password", userManagement.UserPassword);
                cmd.ExecuteNonQuery();
            }
            dbObject.CloseConnection();

            q = "select (max(id) + 1) as [maxId] from users_roles";
            int userRoleId = 1;
            using (SQLiteCommand cmd = new SQLiteCommand(q, dbObject.Connection))
            {
                dbObject.OpenConnection();
                SQLiteDataReader result = cmd.ExecuteReader();

                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        var resultMID = result["maxId"].ToString();
                        if (!string.IsNullOrEmpty(resultMID))
                        {
                            userRoleId = int.Parse(resultMID);
                        }
                    }
                }
            }
            dbObject.CloseConnection();

            q = "insert into users_roles values(@id, @user_id, @role_id)";
            using (SQLiteCommand cmd = new SQLiteCommand(q, dbObject.Connection))
            {
                dbObject.OpenConnection();
                cmd.Parameters.AddWithValue("@id", userRoleId);
                cmd.Parameters.AddWithValue("@user_id", userId);
                cmd.Parameters.AddWithValue("@role_id", userManagement.RoleId);
                cmd.ExecuteNonQuery();
            }
            dbObject.CloseConnection();

            return string.Empty;
        }
    }
}