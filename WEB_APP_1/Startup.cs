using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_APP.Repository.Interface;
using WEB_APP.Repository.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Logging;
using Microsoft.AspNetCore.Localization;

namespace WEB_APP_1
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
            IdentityModelEventSource.ShowPII = true; //Add this line

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture("en-IN");
            });
            services.AddControllersWithViews();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddHttpClient<IAccountService, AccountService>();
            services.AddScoped<IAccountService, AccountService>();

            services.AddHttpClient<IAuthService, AuthService>();
            services.AddScoped<IAuthService, AuthService>();

            services.AddHttpClient<IDepositService, DepositService>();
            services.AddScoped<IDepositService, DepositService>();

            services.AddHttpClient<ILoanservice, LoanService>();
            services.AddScoped<ILoanservice, LoanService>();

            services.AddHttpClient<ICbillService, CbillService>();
            services.AddScoped<ICbillService, CbillService>();

            services.AddHttpClient<IMenuMasterService, MenuMasterService>();
            services.AddScoped<IMenuMasterService, MenuMasterService>();

            services.AddDistributedMemoryCache();
            services.AddAuthentication
    (options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = "oidc";
    })
              .AddCookie(options =>
              {
                  options.Cookie.HttpOnly = true;
                  options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                  options.LoginPath = "/Auth/Login";
                  options.AccessDeniedPath = "/Auth/AccessDenied";
                  options.SlidingExpiration = true;
              }).AddOpenIdConnect("oidc", options =>
              {
                  options.Authority = Configuration["ServiceUrls:IdentityAPI"];
                  options.GetClaimsFromUserInfoEndpoint = true;
                  options.ClientId = "magic";
                  options.ClientSecret = "secret";
                  options.ResponseType = "code";
                  options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                  options.TokenValidationParameters.NameClaimType = "unique_name";
                  options.TokenValidationParameters.RoleClaimType = "role";
                  options.Scope.Add("magic");
                  options.SaveTokens = true;
                  options.RequireHttpsMetadata = false;

                  options.ClaimActions.MapJsonKey("role", "role");

                  options.Events = new OpenIdConnectEvents
                  {
                      OnRemoteFailure = context =>
                      {
                          context.Response.Redirect("/");
                          context.HandleResponse();
                          return Task.FromResult(0);
                      }
                  };
              });


            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(100);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddAutoMapper(typeof(WEB_API.Mappings.Mapping));
            services.AddHttpClient();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                IdentityModelEventSource.ShowPII = true;
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseRequestLocalization();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.UseCookiePolicy();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
