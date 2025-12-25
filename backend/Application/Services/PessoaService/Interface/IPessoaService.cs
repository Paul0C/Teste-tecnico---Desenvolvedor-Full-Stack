using backend.Application.Services.PessoaService.Dto;
using backend.Application.Shared.Results;

namespace backend.Application.Services.PessoaService.Interface;

public interface IPessoaService
{
    Task<Result<PessoaDto>> CreatePessoa(PessoaDto pessoaDto);
    Task<Result<bool>> DeletePessoa(Guid id);
    Task<Result<PessoaDto>> GetPessoaById(Guid id);
    Task<Result<List<PessoaDto>>> GetPessoas();
}