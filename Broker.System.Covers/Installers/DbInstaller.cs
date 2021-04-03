using System;
using Broker.System.Covers.Data;
using Broker.System.Covers.Services;
using Broker.System.Installers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Broker.System.Covers.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BrokerCoversDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConn")));
            services.AddIdentityCore<IdentityUser>()
                .AddEntityFrameworkStores<BrokerCoversDbContext>();

            services.AddScoped<ICoverService, CoverService>();
        }
    }
}