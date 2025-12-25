using Domain.Enums;

namespace backend.Application.Services.CategoriaService.Dto;

public class CategoriaDto
{
    public Guid Id { get; set; }
    public string Descricao { get; set; }
    public FinalidadeCategoria Finalidade { get; set; }
}