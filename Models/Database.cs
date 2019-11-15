using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SQLite;
using System.IO;

namespace DebugToolCSharp.Models
{
    public class Database
    {
        public SQLiteConnection Connection;

        public Database()
        {
            var dbName = "C:\\Work\\DebugTool\\DebugToolCSharp\\DB\\DebugTool.db";
            Connection = new SQLiteConnection("Data Source=" + dbName);

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