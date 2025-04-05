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

        public async Task<bool> Update(int id, Camion camion) {
            Camion? camionFounded = await context.Camiones.FindAsync(id);
            if (camionFounded == null) { return false;  } else
            {
                context.Entry(camionFounded).CurrentValues.SetValues(camion);
                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> Delete(int id) {
            Camion? camionFounded = await context.Camiones.FindAsync(id);
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

        public async Task<Camion?> ConsultarCamion(int id) {
            try {
                Camion? camion = await context.Camiones.FindAsync(id);
                return camion;
            }catch (Exception ex) { Console.WriteLine(ex.Message);
                return null;
            }
        }
    }

    public interface ICamionService
    {
        Task<bool> Insert(Camion camion);
        Task<bool> Update(int id, Camion camion);
        Task<bool> Delete(int id);
        Task<List<Camion>> ListaCamiones();
        Task<Camion?> ConsultarCamion(int id);
    }
}
