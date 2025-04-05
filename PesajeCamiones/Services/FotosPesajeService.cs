using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using PesajeCamiones.Data;
using PesajeCamiones.Data.Models;

namespace PesajeCamiones.Services
{
    public class FotosPesajeService : IFotosPesajeService
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly AppDbContext context;

        public FotosPesajeService(IWebHostEnvironment webHostEnvironment, AppDbContext context)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.context = context;
        }

        public Stream? Download(String nombreArchivo)
        {
            string folderRoot = Path.Combine(webHostEnvironment.ContentRootPath, "Data", "Ficheros");
            string fileRoot = Path.Combine(folderRoot, nombreArchivo);
            if (!File.Exists(fileRoot)) { return null; } else {
                FileStream file = new FileStream(fileRoot, FileMode.Open, FileAccess.Read);
                return file;
            }
        }

        public bool Upload(int idPesaje, IFormFile fichero) {
            try {
                string folderRoot = Path.Combine(webHostEnvironment.ContentRootPath, "Data", "Ficheros");
                if (!Directory.Exists(folderRoot)) {
                    Directory.CreateDirectory(folderRoot);
                }
                //string fileNameGen = Guid.NewGuid().ToString() + "_" + fichero.FileName;
                string fileNameGen = fichero.FileName;
                string fileRoot = Path.Combine(folderRoot, fileNameGen);
                if (fichero.Length > 0)
                {
                    using (var stream = new FileStream(fileRoot, FileMode.Create))
                    {
                        fichero.CopyTo(stream);
                    }
                    FotoPesaje foto = new FotoPesaje {IdPesaje = idPesaje, ImagenVehiculo=fileNameGen };
                    context.FotosPesajes.Add(foto);
                    context.SaveChanges();
                    return true;
                }
                else {
                    Console.WriteLine("Fichero no fue enviado por el usuario");
                    return false;
                }
            } catch (Exception e) {
                Console.WriteLine("No pudo subirse el fichero. " +  e.Message);
                return false;
            }
        }

        public bool Update(int idFichero, IFormFile fichero) { 
            
            FotoPesaje? foto = context.FotosPesajes.Find(idFichero);
            if (foto == null) { return false; } else {
                string folderRoot = Path.Combine(webHostEnvironment.ContentRootPath, "Data", "Ficheros");
                string FoundedRoot = Path.Combine(folderRoot, foto.ImagenVehiculo);
                if (System.IO.File.Exists(FoundedRoot)) { System.IO.File.Delete(FoundedRoot); }
                //string fileNameGen = Guid.NewGuid().ToString() + "_" + fichero.FileName;
                string fileNameGen = fichero.FileName;
                string fileRoot = Path.Combine(folderRoot, fileNameGen);
                using (var stream = new FileStream(fileRoot, FileMode.Create)) {
                    fichero.CopyTo(stream);
                }
                foto.ImagenVehiculo = fileNameGen;
                context.SaveChanges();
                return true;
            }
        }

        public bool Delete(int idFichero) {
            FotoPesaje? foto = context.FotosPesajes.Find(idFichero);
            if (foto == null) { return false; } else {
                string folderRoot = Path.Combine(webHostEnvironment.ContentRootPath, "Data", "Ficheros");
                string FoundedRoot = Path.Combine(folderRoot, foto.ImagenVehiculo);
                if (System.IO.File.Exists(FoundedRoot)) { System.IO.File.Delete(FoundedRoot); }
                context.FotosPesajes.Remove(foto);
                context.SaveChanges();
                return true;
            }
        }
    }

    public interface IFotosPesajeService
    {
        public bool Upload(int idPesaje, IFormFile fichero);
        public Stream? Download(String nombreArchivo);
        public bool Update(int idFichero, IFormFile fichero);
        public bool Delete(int idFichero);
    }
}
