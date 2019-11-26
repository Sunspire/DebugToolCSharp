using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SQLite;
using DebugToolCSharp.Models;
using System.Configuration;

namespace DebugToolCSharp.Classes
{
    public class Queries
    {
        public static string UpdatePageRoles(int pageId, int[] roleIds)
        {
            //Delete current roles
            var connectionString = ConfigurationManager.AppSettings["SQLiteConnectionString"];
            var q = "delete from pages_roles where page_id = @pageId";
            using (SQLiteConnection c = new SQLiteConnection(connectionString))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(q, c))
                {
                    cmd.Parameters.AddWithValue("@pageId", pageId);
                    cmd.ExecuteNonQuery();
                }
            }

            //AddRole new roles
            q = "insert into pages_roles values(@pageId,@roleId)";
            foreach (var id in roleIds)
            {
                using (SQLiteConnection c = new SQLiteConnection(connectionString))
                {
                    c.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(q, c))
                    {
                        cmd.Parameters.AddWithValue("@pageId", pageId);
                        cmd.Parameters.AddWithValue("@roleId", id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            return string.Empty;
        }

        public static List<Pages> GetAllPages() 
        {
            var listPages = new List<Pages>();
            var q = "select * from pages";
            
            using (SQLiteConnection c = new SQLiteConnection(ConfigurationManager.AppSettings["SQLiteConnectionString"]))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(q, c))
                {
                    using (SQLiteDataReader result = cmd.ExecuteReader())
                    {
                        if (result.HasRows)
                        {
                            while (result.Read())
                            {
                                listPages.Add(new Pages() { Id = int.Parse(result["id"].ToString()), PageName = result["page_name"].ToString() });
                            }
                        }
                    }
                }
            }

            return listPages;
        }

        public static Pages GetPageById(int id)
        {
            var mPages = new Pages();
            var q = "select * from pages where id = @id";
            var listRoles = new List<Roles>();

            using (SQLiteConnection c = new SQLiteConnection(ConfigurationManager.AppSettings["SQLiteConnectionString"]))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(q, c))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (SQLiteDataReader result = cmd.ExecuteReader())
                    {
                        if (result.HasRows)
                        {
                            while (result.Read())
                            {
                                mPages.Id = id;
                                mPages.PageName = result["page_name"].ToString();
                            }
                        }
                    }
                }
            }

            q = @"select roles.id as [id], roles.role as [role_name] from roles
                    inner join pages_roles on pages_roles.role_id = roles.id
                    where pages_roles.page_id = @id";
            using (SQLiteConnection c = new SQLiteConnection(ConfigurationManager.AppSettings["SQLiteConnectionString"]))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(q, c))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (SQLiteDataReader result = cmd.ExecuteReader())
                    {
                        if (result.HasRows)
                        {
                            while (result.Read())
                            {
                                var r = new Roles();
                                r.Id = int.Parse(result["id"].ToString());
                                r.Role = result["role_name"].ToString();
                                listRoles.Add(r);
                            }
                        }
                    }
                }
            }
            mPages.Roles = listRoles;
 
            return mPages;
        }

        public static bool GetLogin(string login, string pwd)
        {
            var q = "select count(1) as mycount from users where login=@login and password=@pwd";

            using (SQLiteConnection c = new SQLiteConnection(ConfigurationManager.AppSettings["SQLiteConnectionString"])) 
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(q, c)) 
                {
                    cmd.Parameters.AddWithValue("@login", login);
                    cmd.Parameters.AddWithValue("@pwd", pwd);

                    using (SQLiteDataReader result = cmd.ExecuteReader()) 
                    {
                        if (result.HasRows)
                        {
                            while (result.Read())
                            {
                                if (int.Parse(result["mycount"].ToString()) == 1)
                                {
                                   return true;
                                }
                            }
                        }
                    }
                }
            }

            return false;
        }

        public static LoginModel GetUserDetails(string login, string pwd)
        {
            var loginModel = new LoginModel();
            var q = "select users.id as [user_id], users.name as [user_name], users_roles.role_id as [role_id] from users inner join users_roles on users.id = users_roles.user_id where login=@login and password=@pwd";

            using (SQLiteConnection c = new SQLiteConnection(ConfigurationManager.AppSettings["SQLiteConnectionString"]))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(q, c))
                {
                    cmd.Parameters.AddWithValue("@login", login);
                    cmd.Parameters.AddWithValue("@pwd", pwd);
                    using (SQLiteDataReader result = cmd.ExecuteReader())
                    {
                        if (result.HasRows)
                        {
                            while (result.Read())
                            {
                                loginModel.UserId = int.Parse(result["user_id"].ToString());
                                loginModel.Name = result["user_name"].ToString();
                                loginModel.RoleId = int.Parse(result["role_id"].ToString());
                            }
                        }
                    }
                }
            }
            return loginModel;
        }

        public static string AddRole(string roleName) 
        {
            var q = "select * from roles where role=@roleName";
            var connectionString = ConfigurationManager.AppSettings["SQLiteConnectionString"];

            using (SQLiteConnection c = new SQLiteConnection(connectionString))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(q, c))
                {
                    cmd.Parameters.AddWithValue("@roleName", roleName);
                    using (SQLiteDataReader result = cmd.ExecuteReader()) 
                    {
                        if (result.HasRows)
                        {
                            return "This role already exists";
                        }
                    }   
                }
            }

            q = "select (max(id) + 1) as [maxId] from roles";
            int maxId = 1;
            using (SQLiteConnection c = new SQLiteConnection(connectionString))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(q, c))
                {
                    using (SQLiteDataReader result = cmd.ExecuteReader()) 
                    {
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
                }
            }

            q = "insert into roles values(@id,@roleName)";
            using (SQLiteConnection c = new SQLiteConnection(connectionString))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(q, c))
                {
                    cmd.Parameters.AddWithValue("@id", maxId);
                    cmd.Parameters.AddWithValue("@roleName", roleName);
                    cmd.ExecuteNonQuery();
                }
            }

            return string.Empty;
        }

        public static List<Roles> GetAllRoles()
        {
            var listRoles = new List<Roles>();
            var q = "select * from roles order by role";

            using (SQLiteConnection c = new SQLiteConnection(ConfigurationManager.AppSettings["SQLiteConnectionString"]))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(q, c))
                {
                    using (SQLiteDataReader result = cmd.ExecuteReader())
                    {
                        if (result.HasRows)
                        {
                            while (result.Read())
                            {
                                listRoles.Add(new Roles() { Id = int.Parse(result["id"].ToString()), Role = result["role"].ToString() });
                            }
                        }
                    }
                }
            }
            return listRoles; 
        }

        public static Roles GetRoleById(int id) 
        {
            var mRoles = new Roles();
            var q = "select * from roles where id = @id";

            using (SQLiteConnection c = new SQLiteConnection(ConfigurationManager.AppSettings["SQLiteConnectionString"]))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(q, c))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (SQLiteDataReader result = cmd.ExecuteReader())
                    {
                        if (result.HasRows)
                        {
                            while (result.Read())
                            {
                                mRoles.Id = int.Parse(result["id"].ToString());
                                mRoles.Role = result["role"].ToString();
                            }
                        }
                    }
                }
            }
            return mRoles;
        }

        public static Roles GetRoleByName(string roleName)
        {
            var mRoles = new Roles();
            var q = "select * from roles where role = @roleName";

            using (SQLiteConnection c = new SQLiteConnection(ConfigurationManager.AppSettings["SQLiteConnectionString"]))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(q, c))
                {
                    cmd.Parameters.AddWithValue("@roleName", roleName);
                    using (SQLiteDataReader result = cmd.ExecuteReader())
                    {
                        if (result.HasRows)
                        {
                            while (result.Read())
                            {
                                mRoles.Id = int.Parse(result["id"].ToString());
                                mRoles.Role = result["role"].ToString();
                            }
                        }
                    }
                }
            }

            return mRoles;
        }

        public static string UpdateRoleById(RoleManagement roleManagement)
        {
            if (!string.IsNullOrEmpty(GetRoleByName(roleManagement.Role).Role)) 
            {
                return "This role already exists";
            }

            var q = "update roles set role = @roleName where id = @roleId";

            using (SQLiteConnection c = new SQLiteConnection(ConfigurationManager.AppSettings["SQLiteConnectionString"]))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(q, c))
                {
                    cmd.Parameters.AddWithValue("@roleId", roleManagement.Id);
                    cmd.Parameters.AddWithValue("@roleName", roleManagement.Role);
                    cmd.ExecuteNonQuery();
                }
            }

            return string.Empty;
        }

        public static string AddUser(UserManagement userManagement) 
        {
            var q = "select count(1) as mycount from users where login=@login";
            var connectionString = ConfigurationManager.AppSettings["SQLiteConnectionString"];

            using (SQLiteConnection c = new SQLiteConnection(connectionString))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(q, c))
                {
                    cmd.Parameters.AddWithValue("@login", userManagement.UserLogin);
                    using (SQLiteDataReader result = cmd.ExecuteReader())
                    {
                        if (result.HasRows)
                        {
                            while (result.Read())
                            {
                                if (int.Parse(result["mycount"].ToString()) == 1)
                                {
                                    return "This login already exists";
                                }
                            }
                        }
                    }
                    
                }
            }

            q = "select (max(id) + 1) as [maxId] from users";
            int userId = 1;
            using (SQLiteConnection c = new SQLiteConnection(connectionString))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(q, c))
                {
                    using (SQLiteDataReader result = cmd.ExecuteReader()) 
                    {
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
                }
            }

            q = "select (max(id) + 1) as [maxId] from users_roles";
            int userRoleId = 1;
            using (SQLiteConnection c = new SQLiteConnection(connectionString))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(q, c))
                {
                    using (SQLiteDataReader result = cmd.ExecuteReader())
                    {
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
                }
            }

            q = "insert into users values(@id, @name, @login, @password)";
            using (SQLiteConnection c = new SQLiteConnection(connectionString))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(q, c))
                {
                    cmd.Parameters.AddWithValue("@id", userId);
                    cmd.Parameters.AddWithValue("@name", userManagement.UserName);
                    cmd.Parameters.AddWithValue("@login", userManagement.UserLogin);
                    cmd.Parameters.AddWithValue("@password", userManagement.UserPassword);
                    cmd.ExecuteNonQuery();
                }
            }

            q = "insert into users_roles values(@id, @user_id, @role_id)";
            using (SQLiteConnection c = new SQLiteConnection(connectionString))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(q, c))
                {
                    cmd.Parameters.AddWithValue("@id", userRoleId);
                    cmd.Parameters.AddWithValue("@user_id", userId);
                    cmd.Parameters.AddWithValue("@role_id", userManagement.RoleId);
                    cmd.ExecuteNonQuery();
                }
            }

            return string.Empty;
        }
    }
}