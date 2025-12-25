using backend.Infrastructure.EntityFramework.Repository.Interface;
using Infrastructure.EntityFramework.DataContext;

namespace backend.Infrastructure.EntityFramework.Repository;

public class TransacaoRepository(DataContext context) : GenericRepository(context), ITransacaoRepository 
{
    private readonly DataContext _context = context;
}