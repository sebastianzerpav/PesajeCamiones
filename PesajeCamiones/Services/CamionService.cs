using Microsoft.EntityFrameworkCore;
using PesajeCamiones.Data;
using PesajeCamiones.Data.Models;

namespace PesajeCamiones.Services
{
    public class CamionService : ICamionService
    {
        private readonly AppDbContext context;

        public CamionService(AppDbContext context) {
            this.context = context;
        }

        public async Task<bool> Insert(Camion camion)
        {
            try
            {
                context.Camiones.Add(camion);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return false; }
        }

        public async Task<bool> Update(String placa, Camion camion) {
            Camion? camionFounded = await context.Camiones.FindAsync(placa);
            if (camionFounded == null) { return false;  } else
            {
                context.Entry(camionFounded).CurrentValues.SetValues(camion);
                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> Delete(String placa) {
            Camion? camionFounded = await context.Camiones.FindAsync(placa);
            if (camionFounded == null) { return false; } else {
                context.Camiones.Remove(camionFounded);
                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<List<Camion>> ListaCamiones() {
            try {
                List<Camion> lista = await context.Camiones.ToListAsync();
                return lista;
            }catch (Exception ex) { 
                Console.WriteLine(ex.Message);
                return new List<Camion>();
            }
        }

        public async Task<Camion?> ConsultarCamion(String placa) {
            try {
                Camion? camion = await context.Camiones.FindAsync(placa);
                return camion;
            }catch (Exception ex) { Console.WriteLine(ex.Message);
                return null;
            }
        }
    }

    public interface ICamionService
    {
        Task<bool> Insert(Camion camion);
        Task<bool> Update(String placa, Camion camion);
        Task<bool> Delete(String placa);
        Task<List<Camion>> ListaCamiones();
        Task<Camion?> ConsultarCamion(String placa);
    }
}
