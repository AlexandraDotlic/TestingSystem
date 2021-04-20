using Authentication.ApplicationServices;
using Authentication.Domain.Entities;
using Authentication.Infrastructure.DataAccess.EfCoreDataAccess;
using Core.ApplicationServices;
using Core.Domain.Repositories;
using Core.Domain.Services.External.MailService;
using Core.Infrastructure.DataAccess.EfCoreDataAccess;
using Core.Infrastructure.Services.MailService;
using Core.Infrastructure.Services.MailService.Settings;
using Hangfire;
using Infrastructure.DataAccess.EfCoreDataAccess.Seeds;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

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
            services.AddScoped<UserService>();

            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
            services.AddTransient<IMailService, MailService>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment  env, RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            UserRolesDatabaseSeed.Seed(roleManager);

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseSerilogRequestLogging();
            app.UseCors("LocalhostPolicy");

            app.UseAuthorization();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
