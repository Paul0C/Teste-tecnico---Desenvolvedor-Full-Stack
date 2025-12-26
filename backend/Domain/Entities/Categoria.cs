using Domain.Enums;

namespace backend.Domain.Entities;

public class Categoria
{
    public Guid Id { get; set; }
    public string Descricao { get; set; }
    public FinalidadeCategoria Finalidade { get; set; }
    public List<Transacao> Transacoes { get; set; } = [];
}