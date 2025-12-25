using Domain.Enums;

namespace backend.Application.Services.TransacaoService.Dto;

public class TransacaoDto
{
    public Guid Id { get; set; }
    public Guid PessoaId { get; set; }
    public Guid CategoriaId { get; set; }
    public string Descricao { get; set; }
    public decimal Valor { get; set; }
    public FinalidadeCategoria Finalidade { get; set; }
}