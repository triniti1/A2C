using Npgsql;
using System.Data.Common;

namespace TestDB
{
    public class UsersTablesFactory(NpgsqlCommand cmd)
    {
        public bool CreateUsersTables()
        {
            try
            {
               
                // יצירת טבלת users
                string createUsersTable = @"
                CREATE TABLE IF NOT EXISTS users (
                    user_id UUID PRIMARY KEY,
                    user_name TEXT NOT NULL,
                    user_email TEXT NOT NULL UNIQUE,
                    user_password TEXT NOT NULL,
                    created_at TIMESTAMP NOT NULL DEFAULT NOW(),
                    user_role INT NOT NULL
                );";

                // יצירת טבלת users_history
                string createHistoryTable = @"
                CREATE TABLE IF NOT EXISTS users_history (
                    history_id UUID PRIMARY KEY,
                    user_id UUID NOT NULL REFERENCES users(user_id),
                    last_login_date TIMESTAMP NOT NULL,
                    login_ip TEXT NOT NULL
                );";

                cmd.CommandText = createUsersTable;
                cmd.ExecuteNonQuery();
                Console.WriteLine("Created users table");

                cmd.CommandText = createHistoryTable;
                cmd.ExecuteNonQuery();
                Console.WriteLine("Created users_history table");

                return true;
            }
            catch (DbException ex)
            {
                Console.WriteLine($"Error creating tables: {ex.Message}");
                return false;
            }
        }
    }
}
