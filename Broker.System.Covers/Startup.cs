using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Broker.System.Covers.Installers;
using Broker.System.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Broker.System.Covers
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddHttpContextAccessor();
            services.InstallServices(Configuration);
            services.AddAutoMapper(typeof(Startup));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
            else app.UseHsts();

            var swaggerOptions = app.ApplicationServices.GetRequiredService<SwaggerOptions>();
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting(); 
            
            app.UseSwagger(option => { option.RouteTemplate = swaggerOptions.JsonRoute; });
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                options.OAuthClientId("broker_covers_rest_client");
                options.OAuthUsePkce();
            });

            app.UseAuthentication(); 
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

          
        }
    }
}