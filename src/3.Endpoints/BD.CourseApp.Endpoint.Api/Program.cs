using BD.CourseApp.Core.Domain.Students.Contracts;
using System.Data.SqlClient;
using BD.CourseApp.Core.ApplicationService.Students;
using Asp.Versioning;
using BD.CourseApp.Endpoint.Api.Middlwares;
using Microsoft.Extensions.Configuration;
using BD.CourseApp.Infrastructures.Data.SqlServer.Repositories;
using BD.CourseApp.Core.ApplicationService.Courses;
namespace BD.CourseApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1.0);
                options.ReportApiVersions = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            })
            .AddMvc();
            // Add services to the container.

            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<GlobalException>();
            }) ;
            builder.Services.AddScoped<IStudentRepository, StudentRepository>();
            builder.Services.AddScoped(_ =>
                new SqlConnection(builder.Configuration.GetConnectionString("CourseAppConnectionString")));

            builder.Services.AddScoped<GetStudentHandler>();
            builder.Services.AddScoped<GetAllStudentsHandler>();
            builder.Services.AddScoped<CreateStudentHandler>();
            builder.Services.AddScoped<UpdateStudentHandler>();
            builder.Services.AddScoped<DeleteStudentHandler>();

            builder.Services.AddScoped<GetCourseHandler>();
            builder.Services.AddScoped<GetAllCoursesHandler>();
            builder.Services.AddScoped<CreateCourseHandler>();
            builder.Services.AddScoped<UpdateCourseHandler>();
            builder.Services.AddScoped<DeleteCourseHandler>();

            builder.Services.AddHttpClient("CategoriesApiClient", client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["CategoriesApiClient:Url"]);
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}



