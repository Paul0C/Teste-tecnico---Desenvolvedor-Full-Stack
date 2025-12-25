using backend.Application.Services.CategoriaService.Dto;
using backend.Application.Services.CategoriaService.Interface;
using backend.Application.Shared.Results;
using backend.Infrastructure.EntityFramework.Repository.Interface;
using Domain.Entities;

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
            Finalidade = categoriaDto.Finalidade
        };
        _categoriaRepository.Add(categoria);
        return await _unitOfWork.SaveChangesAsync() ? 
        Result<CategoriaDto>.Success(new CategoriaDto
        {
            Id = categoria.Id,
            Descricao = categoria.Descricao,
            Finalidade = categoria.Finalidade
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
            Finalidade = categoria.Finalidade
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
            Finalidade = categoria.Finalidade
        }).ToList();

        return Result<List<CategoriaDto>>.Success(categoriasDto);
    }
}