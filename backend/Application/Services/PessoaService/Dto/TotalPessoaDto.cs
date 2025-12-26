namespace backend.Application.Services.PessoaService.Dto;

public class TotalPessoasDto
{
    public List<TotalPessoaDto> PessoasTotais { get; set; }
    public decimal TotalReceita { get; set; }
    public decimal TotalDespesa { get; set; }
    public decimal Saldo { get { return TotalReceita - TotalDespesa; } }
}

public class TotalPessoaDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public decimal TotalReceita { get; set; }
    public decimal TotalDespesa { get; set; }
    public decimal Saldo { get { return TotalReceita - TotalDespesa; } }
}