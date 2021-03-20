using Homework.Data;
using Homework.IRepository;
using Homework.Repository;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Homework.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var migrationAssembly = typeof(ApplicationDbContext).GetTypeInfo().Assembly.GetName().Name;
            services.AddControllers(setup =>
            {
                setup.ReturnHttpNotAcceptable = true;
                //setup.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
            }).AddXmlDataContractSerializerFormatters();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddDbContextPool<ApplicationDbContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("MySQLConnection"), o =>
                 o.MigrationsAssembly(migrationAssembly));//migrationAssembly or IdentityServerAspNetIdentity
                options.EnableSensitiveDataLogging(true);//打开敏感数据记录
            });

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
               .AddIdentityServerAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme, jwtOptions =>
               {
                   jwtOptions.Authority = "https://localhost:5000";
                   //jwtOptions.ApiName = "api1";
                   jwtOptions.RequireHttpsMetadata = false;
                   //jwtOptions.ApiSecret = "api1 secret";
                   jwtOptions.Audience = "https://localhost:5000/resources";
                   jwtOptions.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                   {
                       ValidateAudience = true,
                       RequireExpirationTime = true,//设置为Token需要过期时间
                       RequireAudience = true,
                       ClockSkew = TimeSpan.FromSeconds(30),//时间偏移 设置每隔多少秒钟验证一下Token，，官方建议不要改
                   };
               }, referenceOptions =>
               {
                   referenceOptions.Authority = "https://localhost:5000";
                   referenceOptions.ClientId = "Api1";
                   referenceOptions.ClientSecret = "Api1 secret";
                   //referenceOptions.ClaimsIssuer = "https://localhost:5000";
               });


            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "api1");
                });
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api", Version = "v1" });
            });


            services.AddScoped<ITeacherRepository, TeacherRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IStudentWorkRepository, StudentWorkRepository>();
            services.AddScoped<IStudentCourseRepository, StudentCourseRepository>();
            services.AddScoped<IStudentAnswerRepository, StudentAnswerRepository>();
            services.AddScoped<IHomeworkRepository, HomeworkRepository>();

            services.AddCors(options =>
            {
                // this defines a CORS policy called "default"
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins("http://localhost:8080/")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("default");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
