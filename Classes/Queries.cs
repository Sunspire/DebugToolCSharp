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
                        return true;
                    }
                }
            }
            dbObject.CloseConnection();

            return false;
        }
    }
}