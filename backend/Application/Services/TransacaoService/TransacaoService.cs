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

        if(pessoa.Idade < 18 && Enum.Parse<FinalidadeCategoria>(transacaoDto.Finalidade) == FinalidadeCategoria.Receita)
            return Result<TransacaoDto>.Failure("Pessoa menor de idade não pode realizar receitas.");

        var categoria = await _categoriaRepository.GetCategoriaById(transacaoDto.CategoriaId);
        if(categoria == null)
            return Result<TransacaoDto>.Failure("Categoria não encontrada");

        if(categoria.Finalidade != FinalidadeCategoria.Ambas && categoria.Finalidade != Enum.Parse<FinalidadeCategoria>(transacaoDto.Finalidade))
            return Result<TransacaoDto>.Failure("Categoria não compatível com a finalidade da transação.");

        var transacao = new Transacao
        {
            Valor = transacaoDto.Valor,
            CategoriaId = transacaoDto.CategoriaId,
            PessoaId = transacaoDto.PessoaId,
            Descricao = transacaoDto.Descricao,
            Finalidade = Enum.Parse<FinalidadeCategoria>(transacaoDto.Finalidade)
        };
        _transacaoRepository.Add(transacao);
        await _unitOfWork.SaveChangesAsync();
        return Result<TransacaoDto>.Success(transacaoDto);
    }

    public async Task<Result<TransacaoDto>> GetTransacaoById(Guid id)
    {
        var transacao = await _transacaoRepository.GetTransacaoById(id);
        if (transacao == null)
            return Result<TransacaoDto>.Failure("Transação não encontrada.");

        var transacaoDto = new TransacaoDto
        {
            Id = transacao.Id,
            Valor = transacao.Valor,
            CategoriaId = transacao.CategoriaId,
            DescricaoCategoria = transacao.Categoria.Descricao,
            PessoaId = transacao.PessoaId,
            PessoaNome = transacao.Pessoa.Nome,
            Descricao = transacao.Descricao,
            Finalidade = transacao.Finalidade.ToString()
        };

        return Result<TransacaoDto>.Success(transacaoDto);
    }

    public async Task<Result<List<TransacaoDto>>> GetTransacoes()
    {
        var transacoes = await _transacaoRepository.GetTransacoes();
        if (transacoes == null)
            return Result<List<TransacaoDto>>.Failure("Nenhuma transação encontrada.");

        var transacoesDto = transacoes.Select(transacao => new TransacaoDto
        {
            Id = transacao.Id,
            Valor = transacao.Valor,
            CategoriaId = transacao.CategoriaId,
            DescricaoCategoria = transacao.Categoria.Descricao,
            PessoaId = transacao.PessoaId,
            PessoaNome = transacao.Pessoa.Nome,
            Descricao = transacao.Descricao,
            Finalidade = transacao.Finalidade.ToString()
        }).ToList();

        return Result<List<TransacaoDto>>.Success(transacoesDto);
    }
}