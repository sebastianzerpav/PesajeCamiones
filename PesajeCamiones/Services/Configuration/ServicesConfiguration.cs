namespace PesajeCamiones.Services.Configuration
{
    public static class ServicesConfiguration
    {
        public static void Configuration(IServiceCollection services) {
            services.AddScoped<IPesajeService, PesajeService>();
            services.AddScoped<IReportesPesaje, PesajeService>();
            services.AddScoped<IFotosPesajeService, FotosPesajeService>();
            services.AddScoped<ICamionService, CamionService>();
        }
    }
}
