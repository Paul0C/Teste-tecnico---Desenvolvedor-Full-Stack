using Domain.Enums;

namespace backend.Domain.Entities;

public class Transacao
{
    public Guid Id { get; set; }
    public string Descricao { get; set; }
    public decimal Valor { get; set; }
    public FinalidadeCategoria Finalidade { get; set; }
    public Categoria Categoria { get; set; }
    public Guid CategoriaId { get; set; }
    public Pessoa Pessoa { get; set; }
    public Guid PessoaId { get; set; }
}