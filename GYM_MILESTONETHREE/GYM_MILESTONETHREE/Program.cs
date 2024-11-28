
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System;
using GYM_MILESTONETHREE.DataBase;
using GYM_MILESTONETHREE.IService;
using GYM_MILESTONETHREE.Service;
using GYM_MILESTONETHREE.IRepository;
using GYM_MILESTONETHREE.Repository;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace GYM_MILESTONETHREE
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddDbContext<AppDb>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));
            builder.Services.AddScoped<IGymProgramService, GymProgramService>();
            builder.Services.AddScoped<IGymProgramRepository, GymProgramsRepository>();

            builder.Services.AddScoped<IPayamentsRepository, PaymentRepository>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();


            builder.Services.AddScoped<IEnrollementService, EnrollementService>();
            builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();

            builder.Services.AddScoped<INotificationRepository,NoticationRepository>();
            builder.Services.AddScoped<INotificationService,NotificationService>();
          
            var jwtsetting = builder.Configuration.GetSection("Jwt");
            var key = Encoding.ASCII.GetBytes(jwtsetting["Key"]!);

            builder.Services.AddAuthentication()
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                      
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtsetting["Issuer"],                
                        ValidAudience = jwtsetting["Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(key)  
                    };
                });


            const string policyName = "CorsPolicy";
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: policyName, builder =>
                {
                    builder.WithOrigins("*")   
                        .AllowAnyHeader()     
                        .AllowAnyMethod();     
                });
            });


            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });



            var app = builder.Build();


            
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("CorsPolicy");
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
