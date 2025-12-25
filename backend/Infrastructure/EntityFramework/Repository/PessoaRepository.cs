using backend.Domain.Entities;
using backend.Infrastructure.EntityFramework.Repository.Interface;
using Infrastructure.EntityFramework.DataContext;
using Microsoft.EntityFrameworkCore;

namespace backend.Infrastructure.EntityFramework.Repository;

public class PessoaRepository(DataContext context) : GenericRepository(context), IPessoaRepository 
{
    private readonly DataContext _context = context;

    public async Task<Pessoa> GetPessoaById(Guid id)
    {
        return await _context.Pessoas.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Pessoa>> GetPessoas()
    {
        return await _context.Pessoas.AsNoTracking().ToListAsync();
    }
}