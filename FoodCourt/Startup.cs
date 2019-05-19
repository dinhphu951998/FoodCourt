using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Doitsu.Service.Core;
using Doitsu.Service.Core.AuthorizeBuilder;
using FoodCourt.Framework.Helpers;
using FoodCourt.Framework.Models;
using FoodCourt.Framework.ViewModels;
using FoodCourt.Logic.IdentityLogic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace FoodCourt
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Foodcourt API", Version = "v1" });
            });

            services.AddDbContext<FoodCourtContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("FoodCourt"));
            }).AddScoped(typeof(DbContext), typeof(FoodCourtContext));

            services.AddDefaultIdentity<MyIdentity>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;

                options.User.RequireUniqueEmail = true;

            }).AddUserManager<MyUserManager>()
              .AddRoles<IdentityRole<int>>()
              .AddRoleStore<RoleStore<IdentityRole<int>, FoodCourtContext, int>>()
              .AddRoleManager<RoleManager<IdentityRole<int>>>()
              .AddEntityFrameworkStores<FoodCourtContext>();

            SetupAutoMapper();
            setupAuthentication(services);
            setupDependencyInjection(services);

        }

        private void SetupAutoMapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<MyIdentity, LoginViewModel>();
                cfg.CreateMap<LoginViewModel, MyIdentity>();

                cfg.CreateMap<MyIdentity, RegisterViewModel>();
                cfg.CreateMap<RegisterViewModel, MyIdentity>();

                //cfg.CreateMap<SubmitAnswerViewModel, ResultDetail>();
                //cfg.CreateMap<ResultDetail, SubmitAnswerViewModel>();

            });

        }


        private void setupAuthentication(IServiceCollection services)
        {
            DoitsuJWTServiceBuilder.BuildJWTService(services);
        }

        private void setupDependencyInjection(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            //services.AddSingleton<AppSettings>(appSettings);
            services.AddSingleton<IConfiguration>(Configuration);

            services.AddScoped<ExtensionSettings>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IMapper>(Mapper.Instance);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors(options =>
            {
                options.AllowAnyMethod();
                options.AllowAnyOrigin();
                options.AllowAnyHeader();
            });

            app.UseStaticFiles();

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseAuthentication();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "FoodCourt API");
            });
            


        }
    }
}
