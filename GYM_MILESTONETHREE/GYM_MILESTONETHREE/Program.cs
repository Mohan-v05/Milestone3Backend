
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System;
using GYM_MILESTONETHREE.DataBase;
using GYM_MILESTONETHREE.IService;
using GYM_MILESTONETHREE.Service;
using GYM_MILESTONETHREE.IRepository;
using GYM_MILESTONETHREE.Repository;

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
            builder.Services.AddScoped<IProgramService, ProgramService>();
            builder.Services.AddScoped<IGymProgramRepository, GymProgramsRepository>();
           
            builder.Services.AddScoped<IPayamentsRepository , PaymentRepository>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();


            builder.Services.AddScoped<IEnrollementService, EnrollementService>();
            builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();

            // Register the PaymentNotificationService in DI container
            builder.Services.AddScoped<PaymentNotificationService>();

            // Register other services like IEmailSender, ILogger, etc.
            builder.Services.AddScoped<IEmailSender, EmailSender>(); // Assuming EmailSender implements IEmailSender
            builder.Services.AddScoped<ILogger<PaymentNotificationService>, Logger<PaymentNotificationService>>();

            const string policyName = "CorsPolicy";
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: policyName, builder =>
                {
                    builder.WithOrigins("*")   // Allow all origins
                        .AllowAnyHeader()      // Allow all headers
                        .AllowAnyMethod();     // Allow all methods
                });
            });


            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

     

            var app = builder.Build();
          

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("CorsPolicy");
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
