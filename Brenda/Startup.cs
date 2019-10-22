using Brenda.Jokes;
using Brenda.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net;
using System.Net.Http;

namespace Brenda
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
            services.AddDbContext<BrendaContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("BrendaContext")));
            WebProxy webProxy = new WebProxy("http://localhost:5000");

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            }).AddCookie("Cookies")
            .AddOpenIdConnect("oidc", options =>
            {
                options.Authority = "https://demo.identityserver.io";
                options.BackchannelHttpHandler = new HttpClientHandler
                {
                    Proxy = webProxy,
                };
                options.RequireHttpsMetadata = false;
                // name of the API resource
                options.ClientId = "implicit";
                //options.ResponseType = "id_token";
                options.SaveTokens = true;

            });
            services.AddHttpClient("with-proxy")
                    .ConfigurePrimaryHttpMessageHandler(() =>
                    {
                        return new HttpClientHandler()
                        {
                            Proxy = webProxy,
                        };
                    });


            services.AddTransient<IJokeProvider, JokeProvider>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
