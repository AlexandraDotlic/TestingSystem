using Auth.ApplicationServices;
using Auth.Domain.Entities;
using Auth.Infrastructure.DataAccess.EfCoreDataAccess;
using Core.ApplicationServices;
using Core.Domain.Repositories;
using Core.Domain.Services.External.JobService;
using Core.Domain.Services.External.MailService;
using Core.Infrastructure.DataAccess.EfCoreDataAccess;
using Core.Infrastructure.Services.HangfireJobService;
using Core.Infrastructure.Services.MailService;
using Core.Infrastructure.Services.MailService.Settings;
using Hangfire;
using Infrastructure.DataAccess.EfCoreDataAccess.Seeds;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace WebClient
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(opt =>
            {
                opt.AddPolicy(name: "LocalhostPolicy", builder => { builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); });
            });

            services.AddDbContextPool<CoreEfCoreDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("TestingSystemDevConnection"));
            });
            services.AddDbContextPool<AuthenticationEfCoreDbContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("AuthenticationDevConnection"))
           );
            services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("HangfireDevConnection")));
            services.AddHangfireServer();

            #region Identity
            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 5;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
            })
            .AddEntityFrameworkStores<AuthenticationEfCoreDbContext>()
            .AddDefaultTokenProviders();

            #endregion
            services.AddControllers();
            services.AddScoped<ICoreUnitOfWork, CoreEfCoreUnitOfWork>();
            services.AddScoped<TestService>();
            services.AddScoped<StudentService>();
            services.AddScoped<ExaminerService>();
            services.AddScoped<QuestionService>();
            services.AddScoped<GroupService>();
            services.AddScoped<UserService>();
            services.AddScoped<TestStatisticService>();
            services.AddScoped<IJobService, HangfireJobService>();

            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
            services.AddTransient<IMailService, MailService>();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidIssuer = Configuration["Security:Issuer"],
                        ValidateAudience = false,
                        ValidAudience = Configuration["Security:Audience"],
                        ValidateLifetime = true,
                        IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Security:SecretKey"])),
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("IsExaminer", policy => policy.RequireClaim("user_roles", "Examiner"));
                options.AddPolicy("IsStudent", policy => policy.RequireClaim("user_roles", "Student"));
            });



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, CoreEfCoreDbContext context, AuthenticationEfCoreDbContext authContext, RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            context.GetInfrastructure().GetService<IMigrator>().Migrate();
            authContext.GetInfrastructure().GetService<IMigrator>().Migrate();

            UserRolesDatabaseSeed.Seed(roleManager);

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseSerilogRequestLogging();
            app.UseCors("LocalhostPolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHangfireDashboard("/mydashboard");


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
