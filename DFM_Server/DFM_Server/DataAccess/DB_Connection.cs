using System.Data.SQLite;
using LogInfo;


namespace DFM_Server.DataAccess
{
    // Definition for Singleton connection to DB for every client there is only one instance
    public class DBConnection : IDisposable
    {
        private static SQLiteConnection? connectionInstance;
        private static readonly object lockObject = new object();
        private bool disposed = false;

        private DBConnection(string dbUrl)
        {
            try
            {
                connectionInstance = new SQLiteConnection(dbUrl);
                connectionInstance.Open();
            }
            catch (Exception ex)
            {
                LoggerInfo.GetLogger().Error("Failed to create the database connection to: " + dbUrl, ex);
            }
        }

        public static SQLiteConnection GetInstance(string dbUrl)
        {
            if (connectionInstance == null || connectionInstance.State == System.Data.ConnectionState.Closed)
            {
                lock (lockObject)
                {
                    if (connectionInstance == null || connectionInstance.State == System.Data.ConnectionState.Closed)
                    {
                        new DBConnection(dbUrl);
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
            LoggerInfo.GetLogger().Info(" ~DBConnection() called");
            Dispose(false);
        }
    }
}
