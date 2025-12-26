using Domain.Enums;

namespace backend.Application.Services.CategoriaService.Dto;

public class TotalCategoriasDto
{
    public List<TotalCategoriaDto> CategoriasTotais { get; set; } = [];
    public decimal TotalReceita { get; set; }
    public decimal TotalDespesa { get; set; }
    public decimal Saldo { get { return TotalReceita - TotalDespesa; } }
}

public class TotalCategoriaDto
{
    public Guid Id { get; set; }
    public string Descricao { get; set; }
    public FinalidadeCategoria Finalidade { get; set; }
    public decimal TotalReceita { get; set; }
    public decimal TotalDespesa { get; set; }
    public decimal Saldo { get { return TotalReceita - TotalDespesa; } }
}