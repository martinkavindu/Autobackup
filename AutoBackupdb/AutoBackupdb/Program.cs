using System;
using System.Data.SqlClient;
using System.IO;

using System.Timers;

namespace AutoBackupdb
{
    class Program
    {
        public partial class Service1 : ServiceBase
        {
            private Timer timer;
            private string connectionString = "Server=YOUR_SERVER_NAME;Database=master;Integrated Security=True;";

            public Service1()
            {
                InitializeComponent();
            }

            protected override void OnStart(string[] args)
            {
                // Set the timer to run every 24 hours (86400000 ms)
                timer = new Timer(86400000);
                timer.Elapsed += new ElapsedEventHandler(BackupDatabase);
                timer.Start();
            }

            protected override void OnStop()
            {
                timer.Stop();
            }

            private void BackupDatabase(object sender, ElapsedEventArgs e)
            {
                try
                {
                    string backupPath = @"C:\Backup\YourDatabaseName_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".bak";
                    string sqlQuery = $"BACKUP DATABASE YourDatabaseName TO DISK = '{backupPath}' WITH FORMAT, MEDIANAME = 'SQLServerBackups', NAME = 'Full Backup';";

                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    File.AppendAllText(@"C:\Backup\BackupLog.txt", DateTime.Now + " - Error: " + ex.Message + Environment.NewLine);
                }
            }
        }

    }
}
