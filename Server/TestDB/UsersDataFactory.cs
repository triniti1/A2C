using Npgsql;
using System.Data.Common;

namespace TestDB
{
    public class UsersDataFactory(NpgsqlCommand cmd)
    {
        enum UserRole
        {
            User = 0,
            Admin = 1,
            SuperAdmin = 2
        }
        public bool CreateUsersData()
        {
            try
            {

            
                // 1. הוספת תפקידים לטבלת roles
               /* string insertRoles = @"
                INSERT INTO users_roles (role_id, role_name) VALUES
                (@adminId, 1),
                (@userId, 0),
                (@superId, 2)
                ON CONFLICT (role_id) DO NOTHING;
            ";

                Guid adminRoleId = Guid.NewGuid();
                Guid userRoleId = Guid.NewGuid();
                Guid superRoleId = Guid.NewGuid();


                cmd.CommandText = insertRoles;
                cmd.Parameters.AddWithValue("adminId", adminRoleId);
                cmd.Parameters.AddWithValue("userId", userRoleId);
                cmd.Parameters.AddWithValue("superId", superRoleId);
                cmd.ExecuteNonQuery();*/


                // 2. הוספת משתמשים עם סיסמה מוצפנת
                string insertUsers = @"
                INSERT INTO users (user_id, user_name, user_email, user_password, created_at, user_role)
                VALUES (@user_id, @user_name, @user_email, @user_password, @createdAt, @user_role)
            ";

                Guid user1Id = Guid.NewGuid();
                string passwordHash = BCrypt.Net.BCrypt.HashPassword("12345");

                cmd.CommandText = insertUsers;
                cmd.Parameters.AddWithValue("user_id", user1Id);
                cmd.Parameters.AddWithValue("user_name", "David");
                cmd.Parameters.AddWithValue("user_email", "david@gmail.com");
                cmd.Parameters.AddWithValue("user_password", passwordHash);
                cmd.Parameters.AddWithValue("createdAt", DateTime.UtcNow);
                cmd.Parameters.AddWithValue("user_role", (int)UserRole.User);
                cmd.ExecuteNonQuery();
            

                // 3. הוספת רשומות היסטוריה
                string insertHistory = @"
                INSERT INTO users_history (history_id, user_id, last_login_date, login_ip)
                VALUES (@history_id, @user_id, @last_login_date, @login_ip)
            ";
                cmd.CommandText = insertHistory;
                cmd.Parameters.AddWithValue("history_id", Guid.NewGuid());
                cmd.Parameters.AddWithValue("user_id", user1Id);
                cmd.Parameters.AddWithValue("last_login_date", DateTime.UtcNow);
                cmd.Parameters.AddWithValue("login_ip", "127.0.0.1");
                cmd.ExecuteNonQuery();
            
                return true;
            }
            catch (DbException ex)
            {
                Console.WriteLine($"Error creating data: {ex.Message}");
                return false;
            }
        }
    }
}
