using backend.Infrastructure.EntityFramework.Repository.Interface;
using Domain.Entities;
using Infrastructure.EntityFramework.DataContext;
using Microsoft.EntityFrameworkCore;

namespace backend.Infrastructure.EntityFramework.Repository;

public class CategoriaRepository(DataContext context) : GenericRepository(context), ICategoriaRepository 
{
    private readonly DataContext _context = context;

    public async Task<Categoria> GetCategoriaById(Guid id)
    {
        return await _context.Categorias.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<List<Categoria>> GetCategorias()
    {
        return await _context.Categorias.AsNoTracking().ToListAsync();
    }
}