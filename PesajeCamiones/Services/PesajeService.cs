using Microsoft.EntityFrameworkCore;
using PesajeCamiones.Data;
using PesajeCamiones.Data.Models;

namespace PesajeCamiones.Services
{
    public class PesajeService : IPesajeService
    {
        public readonly AppDbContext dbContext;
        public PesajeService(AppDbContext dbContext) { 
            this.dbContext = dbContext;
        }

        public async Task<bool> Insert(Pesaje pesaje) {
            try
            {
                dbContext.Pesajes.Add(pesaje);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) {
                Console.WriteLine("Error al insertar el pesaje" + ex.Message);
                return false;
            }
        }

        public async Task<bool> Update(int id, Pesaje pesaje) {
            try
            {
                Pesaje? pesajeDb = await Search(id);
                if (pesajeDb != null) {
                    dbContext.Entry(pesajeDb).CurrentValues.SetValues(pesaje);
                    await dbContext.SaveChangesAsync();
                    return true;
                } else { 
                    return false;
                }
            }
            catch (Exception ex) {
                Console.WriteLine("Error al actualizar el pesaje" + ex.Message);
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try { 
                Pesaje? pesaje = await Search(id);
                if (pesaje != null) {
                    dbContext.Pesajes.Remove(pesaje);
                    await dbContext.SaveChangesAsync();
                    return true;
                } else { return false; }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar el pesaje" + ex.Message);
                return false;
            }
        }

        public async Task<Pesaje?> Search(int id)
        {
            Pesaje? pesaje = await dbContext.Pesajes.FindAsync(id);
            return pesaje;
        }
    }

    public interface IPesajeService {
        public Task<bool> Insert(Pesaje pesaje);
        public Task<bool> Update(int id, Pesaje pesaje);
        public Task<bool> Delete(int id);
        public Task<Pesaje?> Search(int id);
    }
}
