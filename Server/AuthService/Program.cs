
using A2C.CRM.Api.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data.Common;
using System.Text;

namespace A2C.CRM.Api
{

    public class Program
    {

        /*static void TempCreateHash()
        {
            // This method is just a placeholder to create a hash for the admin user password.
            // In a real application, you would use a proper hashing mechanism.

            var passwordHasher = new PasswordHasher<object>();
            var hashedPassword = passwordHasher.HashPassword(null, "admin");

            Console.WriteLine("Hashed password:");
            Console.WriteLine(hashedPassword);
        }*/

        public static void Main(string[] args)
        {
            try
            {
                var builder = WebApplication.CreateBuilder(args);

                builder.Services.AddCors(options =>
                {
                    options.AddPolicy("AllowReactApp",
                        builder =>
                        {
                            builder.WithOrigins("http://localhost:3000")
                                   .AllowAnyHeader()
                                   .AllowAnyMethod();
                        });
                });

                // Add services to the container.
                builder.Services.AddControllers();
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                // Configure Entity Framework Core with PostgreSQL
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                // TempCreateHash();

                builder.Services.AddDbContext<AppDbContext>(options =>
                    options.UseNpgsql(connectionString));

                // Add Authentication and Authorization
                builder.Services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                    };
                });

                builder.Services.AddAuthorization();

                var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                // Ensure Kestrel listens on all network interfaces inside the container
                // This allows other Docker containers (e.g., the React client) to access the API
                //app.Urls.Add("http://localhost:32774");

                if (!app.Environment.IsDevelopment())
                {
                    app.UseHttpsRedirection();
                }

                app.UseCors("AllowReactApp");

                // Use Authentication and Authorization middleware
                app.UseAuthentication();
                app.UseAuthorization();

                app.MapControllers();

                // Log the environment and connection string
                Console.WriteLine($"Loaded environment: {builder.Environment.EnvironmentName}");
                Console.WriteLine($"Connection string: {builder.Configuration.GetConnectionString("DefaultConnection")}");

                var addresses = app.Urls;
                Console.WriteLine("Kestrel is listening on:");
                foreach (var url in addresses)
                {
                    Console.WriteLine(url);
                }

                app.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                throw;
            }
        }
    }
}
