using System;
using System.Windows.Forms;
using System.Data.Entity;
using MyStoreDesktop.Data;

namespace MyStoreDesktop
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Database initializer
            Database.SetInitializer(new CreateDatabaseIfNotExists<DatabaseHelper>());

            try
            {
                using (var db = new DatabaseHelper())
                {
                    db.Database.CreateIfNotExists();

                    // Pass DatabaseHelper to LoginForm
                    Application.Run(new LoginForm(db));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Exception Occurred:\n\n{ex.ToString()}",
                    "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
