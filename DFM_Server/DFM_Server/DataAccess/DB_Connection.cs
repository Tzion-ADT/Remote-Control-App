using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;


namespace DFM_Server.DataAccess
{
    // Definition for Singleton connection to DB for every client there is only one instance
    public class DBConnection : IDisposable
    {
        private static SQLiteConnection ?connectionInstance ;
        private static readonly object lockObject = new object();
        private bool disposed = false;

        private DBConnection(string dbUrl, string db)
        {
            try
            {
                connectionInstance = new SQLiteConnection(dbUrl + db);
                connectionInstance.Open();
            }
            catch (Exception ex)
            {
                //Logger("Failed to create the database connection to: " + dbUrl + db, ex);
            }
        }

        public static SQLiteConnection GetInstance(string dbUrl, string db)
        {
            if (connectionInstance == null || connectionInstance.State == System.Data.ConnectionState.Closed)
            {
                lock (lockObject)
                {
                    if (connectionInstance == null || connectionInstance.State == System.Data.ConnectionState.Closed)
                    {
                        new DBConnection(dbUrl, db);
                    }
                }
            }
            return connectionInstance;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (connectionInstance != null && connectionInstance.State != System.Data.ConnectionState.Closed)
                    {
                        connectionInstance.Close();
                        connectionInstance.Dispose();
                    }
                }
                disposed = true;
            }
        }

        ~DBConnection()
        {
            Dispose(false);
        }
    }
}
