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
            services.AddControllers();
            services.AddCors(policy => policy.AddPolicy("CorsPolicy", option => option
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseCors("CorsPolicy");

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
