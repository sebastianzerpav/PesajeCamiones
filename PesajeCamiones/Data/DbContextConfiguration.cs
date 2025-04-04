using Microsoft.EntityFrameworkCore;

namespace PesajeCamiones.Data
{
    public static class DbContextConfiguration
    {
        public static void Configuration(IServiceCollection services, IConfiguration configuration)
        {
            String? dbConnectionString = configuration.GetConnectionString("DbConnection");
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(dbConnectionString));
        }
    }
}
