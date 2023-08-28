using Microsoft.EntityFrameworkCore;
using SistemaInventariov7.AccesiDatos.Data;
using SistemaInventariov7.AccesoDatos.Repositorio.IRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventariov7.AccesoDatos.Repositorio
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        private readonly ApplicationDbContext _db;

        internal DbSet<T> dbSet;

        public Repositorio(ApplicationDbContext db )
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }

        public async Task Agregar(T entidad)
        {
            await dbSet.AddAsync(entidad); // Insertar un nuevo registro a la tabla
        }

        public async Task<T> Obtener(int id)
        {
            return await dbSet.FindAsync(id); // Selecciona la tabla por el id que quiere mostrar
        }

        public async Task<IEnumerable<T>> ObtenerTodos(Expression<Func<T, bool>>? filtro = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string? incluirPropiedades = null, bool isTracking = true)
        {
            IQueryable<T> query = dbSet;

            if (filtro != null)
            {
                query = query.Where(filtro); // SELECT * FROM WHERE
            }

            if (incluirPropiedades != null)
            {
                foreach (var incluirProp in incluirPropiedades.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(incluirProp); // Incluir a las propiedades de los objetos relacionados"    
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (!isTracking)
            {
                query = query.AsNoTracking(); // Sirve para que no traquee un registro
            }

            return await query.ToListAsync();
        }


        public async Task<T> ObtenerPrimero(Expression<Func<T, bool>>? filtro = null, string? incluirPropiedades = null, bool isTracking = true)
        {
            IQueryable<T> query = dbSet;

            if (filtro != null)
            {
                query = query.Where(filtro); // SELECT * FROM WHERE
            }

            if (incluirPropiedades != null)
            {
                foreach (var incluirProp in incluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(incluirProp); // Incluir a las propiedades de los objetos relacionados"    
                }
            }

            if (!isTracking)
            {
                query = query.AsNoTracking(); // Sirve para que no traquee un registro
            }

            return await query.FirstOrDefaultAsync();
        }
        
        public void Remover(T entidad)
        {
            dbSet.Remove(entidad);
        }

        public void RemoverRango(IEnumerable<T> entidad)
        {
            dbSet.RemoveRange(entidad);
        }
    }
}
