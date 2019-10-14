using System;
using System.Data.OleDb;

namespace MultiThreadCommands
{
    class DataBase
    {
        static readonly string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=KasaiUsersDB.mdb;";
      //static readonly string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=KasaiUsersDB.mdb;";
        static OleDbConnection Connection = new OleDbConnection(connectionString);
        static public OleDbCommand Command;
        static public OleDbDataReader Reader;
        static int Connections = 0;
        
        public static bool Connect ()
        {
            if (!(Connection.State == System.Data.ConnectionState.Open))
            {
                try
                {
                    Connection.Open();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Соединение с базой данных не установлено.");
                    Console.WriteLine("Код ошибки: " + ex.Message);
                    return false;
                }
            } else
            {
                Connections++;
                return true;
            }
        }

        public static void Close ()
        {
            if(Connections == 0) { 
                Connection.Close();
            } else
            {
                Connections--;
            }
        }

        public static OleDbCommand Query (string query)
        {
            return new OleDbCommand(query,Connection);
        }

    }
}
