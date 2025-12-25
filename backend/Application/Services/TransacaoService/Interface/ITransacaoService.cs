using backend.Application.Services.TransacaoService.Dto;
using backend.Application.Shared.Results;

namespace backend.Application.Services.TransacaoService.Interface;

public interface ITransacaoService
{
    Task<Result<TransacaoDto>> CreateTransacao(TransacaoDto transacaoDto);
    Task<Result<TransacaoDto>> GetTransacaoById(Guid id);
    Task<Result<List<TransacaoDto>>> GetTransacoes();
}