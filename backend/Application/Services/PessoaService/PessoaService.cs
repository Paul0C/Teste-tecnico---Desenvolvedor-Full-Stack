using backend.Application.Services.PessoaService.Dto;
using backend.Application.Services.PessoaService.Interface;
using backend.Application.Shared.Results;
using backend.Infrastructure.EntityFramework.Repository.Interface;
using backend.Domain.Entities;

namespace backend.Application.Services.PessoaService;

public class PessoaService (IPessoaRepository pessoaRepository, IUnitOfWork unitOfWork) : IPessoaService
{
    public IPessoaRepository _pessoaRepository { get; set; } = pessoaRepository;
    public IUnitOfWork _unitOfWork { get; set; } = unitOfWork;
    public async Task<Result<PessoaDto>> CreatePessoa(PessoaDto pessoaDto)
    {
        var pessoa = new Pessoa
        {
            Nome = pessoaDto.Nome,
            Idade = pessoaDto.Idade
        };
        _pessoaRepository.Add(pessoa);
        return await _unitOfWork.SaveChangesAsync() ? 
        Result<PessoaDto>.Success(new PessoaDto
        {
            Id = pessoa.Id,
            Nome = pessoa.Nome,
            Idade = pessoa.Idade
        }) :
        Result<PessoaDto>.Failure("Erro ao salvar a pessoa.");

    }

    public async Task<Result<bool>> DeletePessoa(Guid id)
    {
        var pessoa = await _pessoaRepository.GetPessoaById(id);
        if (pessoa == null)
            return Result<bool>.Failure("Essa pessoa não foi encontrada para exclusão.");

        _pessoaRepository.Delete(pessoa);
        return await _unitOfWork.SaveChangesAsync() ? 
            Result<bool>.Success(true) : 
            Result<bool>.Failure("Erro ao excluir a pessoa.");
    }

    public async Task<Result<PessoaDto>> GetPessoaById(Guid id)
    {
        var pessoa = await _pessoaRepository.GetPessoaById(id);
        if (pessoa == null)
            return Result<PessoaDto>.Failure("Pessoa não encontrada.");

        var pessoaDto = new PessoaDto
        {
            Id = pessoa.Id,
            Nome = pessoa.Nome,
            Idade = pessoa.Idade
        };

        return Result<PessoaDto>.Success(pessoaDto);
    }

    public async Task<Result<List<PessoaDto>>> GetPessoas()
    {
        var pessoas = await _pessoaRepository.GetPessoas();
        if (pessoas == null)
            return Result<List<PessoaDto>>.Failure("Nenhuma pessoa encontrada.");

        var pessoasDto = pessoas.Select(pessoa => new PessoaDto
        {
            Id = pessoa.Id,
            Nome = pessoa.Nome,
            Idade = pessoa.Idade
        }).ToList();

        return Result<List<PessoaDto>>.Success(pessoasDto);
    }
}