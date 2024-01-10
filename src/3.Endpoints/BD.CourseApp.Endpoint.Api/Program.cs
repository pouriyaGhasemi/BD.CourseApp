using BD.CourseApp.Core.Domain.Students.Contracts;
using System.Data.SqlClient;
using BD.CourseApp.Core.ApplicationService.Students;
using Asp.Versioning;
using BD.CourseApp.Endpoint.Api.Middlwares;
using Microsoft.Extensions.Configuration;
using BD.CourseApp.Infrastructures.Data.SqlServer.Repositories;
using BD.CourseApp.Core.ApplicationService.Courses;
using BD.CourseApp.Endpoint.Api.Middlewares;
using Microsoft.OpenApi.Models;
using BD.CourseApp.Core.Domain.AssignedCourses.Contracts;
using BD.CourseApp.Core.Domain.Courses.Contracts;
using BD.CourseApp.Core.Domain.Categories.Contracts;
using BD.CourseApp.Infrastructures.Services.Outbound;
using BD.CourseApp.Core.ApplicationService.Categories;
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

            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<GlobalException>();
            });
            builder.Services.AddScoped<IStudentRepository, StudentRepository>();
            builder.Services.AddScoped<IAssignedCourseRepository, AssignedCourseRepository>();
            builder.Services.AddScoped <ICourseRepository, CourseRepository>();
            builder.Services.AddScoped <ICategoryService, CategoryService>();
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

            builder.Services.AddScoped<GetAllCategoriesHandler>();
            
            builder.Services.AddHttpClient("CategoriesApiClient", client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["CategoriesApiClient:Url"]);
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(
                c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BD Course API", Version = "v1" });
                    c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        Scheme = "basic",
                        In = ParameterLocation.Header,
                        Description = "Basic Authentication header using the Bearer scheme."
                    });
                    c.AddSecurityRequirement(new OpenApiSecurityRequirement{{
                            new OpenApiSecurityScheme{
                                Reference = new OpenApiReference{
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "basic"}
                                },
                                new string[] {}
                            }});
                    });

            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<BasicAuthenticationHandler>("test area");
            app.UseAuthorization();
            app.UseHttpsRedirection();
            app.MapControllers();

            app.Run();
        }
    }
}



