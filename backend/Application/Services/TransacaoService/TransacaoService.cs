using backend.Application.Services.TransacaoService.Dto;
using backend.Application.Services.TransacaoService.Interface;
using backend.Application.Shared.Results;
using backend.Domain.Entities;
using backend.Infrastructure.EntityFramework.Repository.Interface;
using Domain.Enums;

namespace backend.Application.Services.TransacaoService;

public class TransacaoService (ITransacaoRepository transacaoRepository, IPessoaRepository pessoaRepository, ICategoriaRepository categoriaRepository, IUnitOfWork unitOfWork) : ITransacaoService
{
    private readonly ITransacaoRepository _transacaoRepository = transacaoRepository;
    private readonly IPessoaRepository _pessoaRepository = pessoaRepository;
    private readonly ICategoriaRepository _categoriaRepository = categoriaRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Result<TransacaoDto>> CreateTransacao(TransacaoDto transacaoDto)
    {
        var pessoa = await _pessoaRepository.GetPessoaById(transacaoDto.PessoaId);
        if(pessoa == null)
            return Result<TransacaoDto>.Failure("Pessoa não encontrada");

        if(pessoa.Idade < 18 && transacaoDto.Finalidade == FinalidadeCategoria.Receita)
            return Result<TransacaoDto>.Failure("Pessoa menor de idade não pode realizar receitas.");

        var categoria = await _categoriaRepository.GetCategoriaById(transacaoDto.CategoriaId);
        if(categoria == null)
            return Result<TransacaoDto>.Failure("Categoria não encontrada");

        var transacao = new Transacao
        {
            Valor = transacaoDto.Valor,
            CategoriaId = transacaoDto.CategoriaId,
            PessoaId = transacaoDto.PessoaId,
            Descricao = transacaoDto.Descricao,
            Finalidade = transacaoDto.Finalidade
        };
        _transacaoRepository.Add(transacao);
        await _unitOfWork.SaveChangesAsync();
        return Result<TransacaoDto>.Success(transacaoDto);
    }

    public Task<Result<TransacaoDto>> GetTransacaoById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Result<List<TransacaoDto>>> GetTransacoes()
    {
        throw new NotImplementedException();
    }
}