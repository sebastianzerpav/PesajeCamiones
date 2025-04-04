namespace PesajeCamiones.Services.Configuration
{
    public static class ServicesConfiguration
    {
        public static void Configuration(IServiceCollection services) {
            services.AddScoped<IPesajeService, PesajeService>();
        }
    }
}
