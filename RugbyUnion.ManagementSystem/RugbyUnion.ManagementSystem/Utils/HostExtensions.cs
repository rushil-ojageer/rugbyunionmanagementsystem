using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RugbyUnion.ManagementSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RugbyUnion.ManagementSystem.Utils
{
    public static class HostExtensions
    {
        public static IHost SeedData(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetService<RugbyUnionContext>();
                
                context.Database.EnsureCreated();
            }

            return host;
        }
    }
}
