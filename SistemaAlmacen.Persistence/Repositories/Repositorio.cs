using Microsoft.EntityFrameworkCore;
using SistemaAlmacen.Application.Contracts.Persistence;
using SistemaAlmacen.Domain.Entities;

namespace SistemaAlmacen.Persistence.Repositories
{
    public class Repositorio<T> : IRepositorio<T> where T : BaseEntity
    {
        private readonly SistemaAlmacenDbContext _db;

        public Repositorio(SistemaAlmacenDbContext db) => _db = db;

        public async Task<T> AgregarAsync(T entidad, CancellationToken ct = default)
        {
            await _db.AddAsync(entidad, ct);
            await _db.SaveChangesAsync(ct);

            return entidad;
        }

        public async Task<T?> ObtenerPorIdAsync(Guid id, CancellationToken ct = default)
        {
          return await _db.Set<T>().AsNoTracking().FirstOrDefaultAsync(e=>e.Id==id, ct); 
        }

        public async Task<IReadOnlyList<T>> ObtenerTodosAsync(CancellationToken ct = default)
            => await _db.Set<T>().AsNoTracking().ToListAsync(ct);

        public async Task<T> ActualizarAsync(T entidad, CancellationToken ct = default)
        {
            _db.Attach(entidad);
            _db.Entry(entidad).State = EntityState.Modified;
            await _db.SaveChangesAsync(ct);
            return entidad;
        }

        public async Task EliminarAsync(Guid id, CancellationToken ct = default)
        {
            var e = await ObtenerPorIdAsync(id, ct);
            if (e is null) return;

            _db.Set<T>().Remove(e);
            await _db.SaveChangesAsync(ct);
        }

        public Task<int> ContarAsync(CancellationToken ct = default)
            => _db.Set<T>().AsNoTracking().CountAsync(ct);

        public async Task<IReadOnlyList<T>> ObtenerPaginaAsync(int skip, int take, CancellationToken ct = default)
            => await _db.Set<T>().AsNoTracking().Skip(skip).Take(take).ToListAsync(ct);        

        public async Task<bool> ExisteAsync(Guid id, CancellationToken ct = default)
        {
            var e = await ObtenerPorIdAsync(id, ct);
            
            return e != null;
        }
    }
}
