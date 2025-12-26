using backend.Application.Services.CategoriaService.Dto;
using backend.Application.Shared.Results;

namespace backend.Application.Services.CategoriaService.Interface;

public interface ICategoriaService
{
    Task<Result<CategoriaDto>> CreateCategoria(CategoriaDto categoriaDto);
    Task<Result<CategoriaDto>> GetCategoriaById(Guid id);
    Task<Result<List<CategoriaDto>>> GetCategorias();
    Task<Result<TotalCategoriasDto>> GetTotalByCategorias();
}