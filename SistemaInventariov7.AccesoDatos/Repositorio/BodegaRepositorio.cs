using Microsoft.EntityFrameworkCore;
using SistemaInventariov7.AccesiDatos.Data;
using SistemaInventariov7.AccesoDatos.Repositorio.IRepositorio;
using SistemaInventariov7.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventariov7.AccesoDatos.Repositorio
{
    public class BodegaRepositorio : Repositorio<Bodega>, IBodegaRepositorio
    {
        private readonly ApplicationDbContext _db;

        public BodegaRepositorio(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

        public void Actualizar(Bodega bodega)
        {
            var bodegaBD = _db.bodegas.FirstOrDefault(b => b.Id == bodega.Id);

            if(bodegaBD != null)
            {
                bodegaBD.Nombre = bodega.Nombre;
                bodegaBD.Descripcion = bodega.Descripcion;
                bodegaBD.Estado = bodega.Estado;
                
                _db.SaveChanges();
            }
        }
    }
}
