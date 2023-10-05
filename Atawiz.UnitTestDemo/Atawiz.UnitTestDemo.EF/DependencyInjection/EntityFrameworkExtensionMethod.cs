using Atawiz.UnitTestDemo.Core.Repositories;
using Atawiz.UnitTestDemo.EF.Context;
using Atawiz.UnitTestDemo.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Atawiz.UnitTestDemo.EF.DependencyInjection
{
    public static class EntityFrameworkExtensionMethod
    {
        public static void AddEntityFramework(this IServiceCollection services, IConfiguration configuration)
        {
            var databaseConfig = configuration.GetSection("Database");
            if (databaseConfig.Exists())
            {
                services.AddDbContext<MainDbContext>(o =>
                {
                    o.UseSqlServer(databaseConfig["ConnectionString"],
                    c => c
                        .MigrationsHistoryTable("__EFMigrationsHistory", "dbo")
                    );
                });
            }
        }

        public static void AddEntityFrameworkRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetSection("Database").Exists())
            {
                services.AddTransient<ITodoItemRepository, TodoItemRepository>();
            }
        }
    }
}
