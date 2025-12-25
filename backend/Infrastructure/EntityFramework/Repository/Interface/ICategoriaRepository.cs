using Backend.Infrastructure.EntityFramework.Repository.Interface;
using Domain.Entities;

namespace backend.Infrastructure.EntityFramework.Repository.Interface;

public interface ICategoriaRepository : IGenericRepository
{
    Task<Categoria> GetCategoriaById(Guid id);
    Task<List<Categoria>> GetCategorias();
}