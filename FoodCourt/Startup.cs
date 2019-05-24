using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Doitsu.Service.Core;
using Doitsu.Service.Core.AuthorizeBuilder;
using FoodCourt.Framework.ExternalAuthentication;
using FoodCourt.Framework.Helpers;
using FoodCourt.Framework.Models;
using FoodCourt.Framework.ViewModels;
using FoodCourt.Service.CategoryService;
using FoodCourt.Service.FoodService;
using FoodCourt.Service.IdentityService;
using FoodCourt.Service.UnitOfWork;
using FoodCourt.Service.StoreService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
using Microsoft.IdentityModel.Tokens;
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

                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });

                var security = new Dictionary<string, IEnumerable<string>>
                                                  {
                                                      {"Bearer", new string[] { }},
                                                  };

                c.AddSecurityRequirement(security);


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
              .AddSignInManager()
              .AddRoles<IdentityRole<int>>()
              .AddRoleStore<RoleStore<IdentityRole<int>, FoodCourtContext, int>>()
              .AddRoleManager<RoleManager<IdentityRole<int>>>()
              .AddEntityFrameworkStores<FoodCourtContext>();

            services.AddAuthorization(cfg =>
            {
                var schemas = new List<string>() { JwtBearerDefaults.AuthenticationScheme };
                cfg.DefaultPolicy =
                new AuthorizationPolicy(cfg.DefaultPolicy.Requirements, schemas.AsEnumerable());
                
            });

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

                cfg.CreateMap<MyIdentity, RegisterExternalViewModel>();
                cfg.CreateMap<RegisterExternalViewModel, MyIdentity>();

                cfg.CreateMap<Food, FoodViewModel>();
                cfg.CreateMap<FoodViewModel, Food>();

                cfg.CreateMap<OrderViewModel, Order>();
                cfg.CreateMap<Order, OrderViewModel>();

                cfg.CreateMap<StoreViewModel, Store>();
                cfg.CreateMap<Store, StoreViewModel>();

                cfg.CreateMap<CategoryViewModel, Category>();
                cfg.CreateMap<Category, CategoryViewModel >();

            });

        }


        private void setupAuthentication(IServiceCollection services)
        {
            const string scheme = JwtBearerDefaults.AuthenticationScheme;
            var appSettings = Configuration.GetSection("AppSettings").Get<AppSettings>();

            DoitsuJWTServiceBuilder.BuildJWTService(services, options =>
            {
                options.DefaultAuthenticateScheme = scheme;
                options.DefaultChallengeScheme = scheme;
                options.DefaultScheme = scheme;

            }, options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = appSettings.Audience,
                    ValidIssuer = appSettings.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(appSettings.SecretKey))
                };
            });
        }

        private void setupDependencyInjection(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            //services.AddSingleton<AppSettings>(appSettings);
            services.AddSingleton<IConfiguration>(Configuration);

            services.AddScoped<ExtensionSettings>();
            services.AddScoped<ExternalAuthenticationFactory>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IUnitOfWork, MyUnitOfWork>();
            services.AddScoped<MyUnitOfWork>();
            services.AddScoped<IStoreService, StoreService>();
            services.AddScoped<IFoodService, FoodService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<FoodValidation>();
            services.AddScoped<StoreValidation>();
            services.AddScoped<CategoryValidation>();



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
