using DAPTUD.DbConfig;
using DAPTUD.IDbConfig;
using DAPTUD.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Stripe;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using DAPTUD.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DAPTUD
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
            services.AddHttpClient();
            services.Configure<DatabaseConfig>(Configuration.GetSection(nameof(DbConfig)));
            services.AddSingleton<IDatabaseConfig>(provider => provider.GetRequiredService<IOptions<DatabaseConfig>>().Value);
            services.AddScoped<SanPhamService>();
            services.AddScoped<DonHangService>();
            services.AddScoped<NguoiDungService>();
            services.AddScoped<ChiTietDonHangService>();
            services.AddScoped<VanDonService>();
            services.AddScoped<ShipperService>();
            services.AddScoped<CheckoutService>();
            services.AddScoped<CuaHangService>();
            services.AddScoped<LocationService>();
            services.AddControllers();
            services.AddCors(policy => policy.AddPolicy("CorsPolicy", option => option
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()));

            services.AddCors(options =>
            {
                options.AddPolicy(name: "MyPolicy",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200", "https://checkout.stripe.com")
                                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                                    .AllowAnyOrigin()
                                    .AllowAnyHeader()
                                    .AllowAnyMethod();
                    });
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "https://localhost:44349",
                    ValidAudience = "https://localhost:44349",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"))
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            StripeConfiguration.ApiKey = "sk_test_51KIOLjHfeS4VmIfge6OsRS6lPH6IMrH7yJWVmZb5SJbH8KkDgV9RLXp9T1uPVF6wLngO1I7F5iJLXDKMI3yyCD1u00pgkFbzo5";
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseCors("CorsPolicy"); app.UseCors();

            app.UseAuthentication();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
