using ConferenceManager.Repository;
using ConferenceManager.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;


namespace ConferenceManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddScoped<IEventService, EventService>();
            builder.Services.AddScoped<EventModel, EventModel>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your-very-secure-secret-which-should-be-quite-long"));
            //var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //var token = new JwtSecurityToken(
            //    issuer: "your-name",
            //    audience: "your-app-name",
            //    expires: DateTime.Now.AddMinutes(30),
            //    signingCredentials: credentials
            //);
            //var tokenHandler = new JwtSecurityTokenHandler();

            //// Turn the token object into the token string which is added to the header
            //string jwtToken = tokenHandler.WriteToken(token);

            //// This prints to the debug output in VS2022
            //System.Diagnostics.Debug.WriteLine(jwtToken);


            var key = Encoding.UTF8.GetBytes("your-very-secure-secret-which-must-be-quite-long-see-below");

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = "your-name",
                    ValidateAudience = true,
                    ValidAudience = "your-app-name",
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
