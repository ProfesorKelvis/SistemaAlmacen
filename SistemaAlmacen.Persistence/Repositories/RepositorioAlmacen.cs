using SistemaAlmacen.Application.Contracts.Persistence;
using SistemaAlmacen.Domain.Entities;

namespace SistemaAlmacen.Persistence.Repositories
{
    public class RepositorioAlmacen: Repositorio<Almacen>, IRepositorioAlmacen
    {
        public RepositorioAlmacen(SistemaAlmacenDbContext db) : base(db)
        {
        }   
    }
}
