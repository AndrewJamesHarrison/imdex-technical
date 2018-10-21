using Imdex.Models;
using Imdex.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System.Data.Common;
using System.Security.Cryptography.X509Certificates;

namespace Imdex
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddSingleton(typeof(DbConnection), (IServiceProvider) => InitializeDatabase());
            services.AddSingleton<IDepthReadingRepository, DepthReadingRepository>();
            services.AddSingleton<IDrillHoleRepository, DrillHoleRepository>();
        }

        DbConnection InitializeDatabase()
        {
            NpgsqlConnection connection;
            var connectionString = new NpgsqlConnectionStringBuilder(
                Configuration["CloudSql:ConnectionString"])
                    {
                        SslMode = SslMode.Require,
                        TrustServerCertificate = true,
                        UseSslStream = true,
                    };

            if (string.IsNullOrEmpty(connectionString.Database))
                connectionString.Database = "Imdex0001";

            connection = new NpgsqlConnection(connectionString.ConnectionString);
            connection.ProvideClientCertificatesCallback += certs => certs.Add(new X509Certificate2(Configuration["CloudSql:CertificateFile"]));
            return connection;
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
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
