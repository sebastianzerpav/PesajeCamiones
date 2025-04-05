using Microsoft.EntityFrameworkCore;
using PesajeCamiones.Data;
using PesajeCamiones.Data.DTOs;
using PesajeCamiones.Data.Models;

namespace PesajeCamiones.Services
{
    public class PesajeService : IPesajeService, IReportesPesaje
    {
        public readonly AppDbContext dbContext;
        public PesajeService(AppDbContext dbContext) { 
            this.dbContext = dbContext;
        }

        public async Task<bool> Insert(Pesaje pesaje) {
            try
            {
                Camion? camion = await dbContext.Camiones.FindAsync(pesaje.PlacaCamion);
                if (camion == null)
                {
                    camion = new Camion { Placa = pesaje.PlacaCamion, Marca = "defaultValue", NumeroEjes = 0 };
                    dbContext.Camiones.Add(camion);
                }
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

        public async Task<List<PesajePorPlacaReporte>> PesajePorPlaca(String placa) {
            List<PesajePorPlacaReporte> pesajes = await dbContext.Pesajes.Where(p => p.PlacaCamion == placa).Join(
                dbContext.Camiones, p => p.PlacaCamion, c => c.Placa, (p, c) => new PesajePorPlacaReporte {
                    Placa = c.Placa,
                    Marca = c.Marca,
                    NumeroEjes = c.NumeroEjes,
                    Peso = p.Peso,
                    Estacion = p.Estacion,
                    FechaPesaje = p.FechaPesaje
                }).ToListAsync();
            return pesajes;
        }
    }

    public interface IPesajeService {
        public Task<bool> Insert(Pesaje pesaje);
        public Task<bool> Update(int id, Pesaje pesaje);
        public Task<bool> Delete(int id);
        public Task<Pesaje?> Search(int id);
    }

    public interface IReportesPesaje {
        public Task<List<PesajePorPlacaReporte>> PesajePorPlaca(String placa);
    }

}
