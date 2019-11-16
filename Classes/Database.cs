using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SQLite;
using System.IO;
using System.Configuration;

namespace DebugToolCSharp.Classes
{
    public class Database
    {
        public SQLiteConnection Connection;

        public Database()
        {
            var dbName = ConfigurationManager.AppSettings["SQLitePath"];
            Connection = new SQLiteConnection(string.Format("Data Source={0}", dbName));

            if (!File.Exists(dbName)) 
            {
                SQLiteConnection.CreateFile(dbName);
            }
        }

        public void OpenConnection()
        {
            if (Connection.State != System.Data.ConnectionState.Open)
            {
                Connection.Open();
            }
        }

        public void CloseConnection() 
        {
            if (Connection.State != System.Data.ConnectionState.Closed)
            {
                Connection.Close();
            }
        }
    }
}