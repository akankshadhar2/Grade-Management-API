using GradeManagement.Data;
using Microsoft.EntityFrameworkCore;


namespace GradeManagement
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


            builder.Services.AddDbContext<GradeDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("GradeConnectionString"));
            });
            // Configure CORS to allow requests from the Angular frontend
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });


            var app = builder.Build();


            // Redirect HTTP to HTTPS
            app.Use(async (context, next) =>
            {
                if (!context.Request.IsHttps)
                {
                    var httpsUrl = "https://" + context.Request.Host + context.Request.Path;
                    context.Response.Redirect(httpsUrl);
                }
                else
                {
                    await next();
                }
            });


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseCors("AllowAll");
            app.UseHttpsRedirection();


            app.UseAuthorization();




            app.MapControllers();


            app.Run();
        }
    }
}
