using backend.Application.Services.CategoriaService.Dto;
using backend.Application.Services.CategoriaService.Interface;
using backend.Application.Shared.Results;
using backend.Domain.Entities;
using backend.Infrastructure.EntityFramework.Repository.Interface;
using Domain.Enums;

namespace backend.Application.Services.CategoriaService;

public class CategoriaService(ICategoriaRepository categoriaRepository, IUnitOfWork unitOfWork) : ICategoriaService
{
    private readonly ICategoriaRepository _categoriaRepository = categoriaRepository;
    public IUnitOfWork _unitOfWork { get; set; } = unitOfWork;
    public async Task<Result<CategoriaDto>> CreateCategoria(CategoriaDto categoriaDto)
    {
        var categoria = new Categoria
        {
            Descricao = categoriaDto.Descricao,
            Finalidade = Enum.Parse<FinalidadeCategoria>(categoriaDto.Finalidade)
        };
        _categoriaRepository.Add(categoria);
        return await _unitOfWork.SaveChangesAsync() ? 
        Result<CategoriaDto>.Success(new CategoriaDto
        {
            Id = categoria.Id,
            Descricao = categoria.Descricao,
            Finalidade = categoria.Finalidade.ToString()
        }) :
        Result<CategoriaDto>.Failure("Erro ao salvar a Categoria.");
    }

    public async Task<Result<CategoriaDto>> GetCategoriaById(Guid id)
    {
        var categoria = await _categoriaRepository.GetCategoriaById(id);
        if (categoria == null)
            return Result<CategoriaDto>.Failure("Categoria não encontrada.");

        var categoriaDto = new CategoriaDto
        {
            Id = categoria.Id,
            Descricao = categoria.Descricao,
            Finalidade = categoria.Finalidade.ToString()
        };

        return Result<CategoriaDto>.Success(categoriaDto);
    }

    public async Task<Result<List<CategoriaDto>>> GetCategorias()
    {
        var categorias = await _categoriaRepository.GetCategorias();
        if (categorias == null)
            return Result<List<CategoriaDto>>.Failure("Nenhuma categoria encontrada.");

        var categoriasDto = categorias.Select(categoria => new CategoriaDto
        {
            Id = categoria.Id,
            Descricao = categoria.Descricao,
            Finalidade = categoria.Finalidade.ToString()
        }).ToList();

        return Result<List<CategoriaDto>>.Success(categoriasDto);
    }

    public async Task<Result<TotalCategoriasDto>> GetTotalByCategorias()
    {
        var categorias = await _categoriaRepository.GetCategorias();
        if (categorias == null)
            return Result<TotalCategoriasDto>.Failure("Nenhuma categoria encontrada.");

        var categoriasTotaisDto = categorias.Select(categoria => new TotalCategoriaDto
        {
            Id = categoria.Id,
            Descricao = categoria.Descricao,
            Finalidade = categoria.Finalidade,
            TotalReceita = categoria.Transacoes.Where(t => t.Finalidade == FinalidadeCategoria.Receita).Sum(t => t.Valor),
            TotalDespesa = categoria.Transacoes.Where(t => t.Finalidade == FinalidadeCategoria.Despesa).Sum(t => t.Valor)
        }).ToList();

        var totalCategoriasDto = new TotalCategoriasDto
        {
            CategoriasTotais = categoriasTotaisDto,
            TotalReceita = categoriasTotaisDto.Sum(s => s.TotalReceita),
            TotalDespesa = categoriasTotaisDto.Sum(s => s.TotalDespesa)
        };

        return Result<TotalCategoriasDto>.Success(totalCategoriasDto);
    }
}