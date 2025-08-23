using Npgsql;
using TestDB;


var connectionString = "Host=localhost;Port=5432;Username=postgres;Password=1234;Database=mydb";

using (var conn = new NpgsqlConnection(connectionString))
{
    conn.Open();  // מנסה להתחבר למסד
    var SqlCommand = new NpgsqlCommand();
    SqlCommand.Connection = conn;

    var createTablesCommand = new CreateTablesCommand(new UsersTablesFactory(SqlCommand));
    var createDataCommand  = new CreateDataCommand(new UsersDataFactory(SqlCommand));
                
    if (createTablesCommand.Execute())
        Console.WriteLine("Users tables exits or created successfully");
    else
        Console.WriteLine("Failed to create Users tables");

    if (createDataCommand.Execute())
        Console.WriteLine("Users data created successfully");
    else
        Console.WriteLine("Failed to create Users data");

    conn.Close();
}