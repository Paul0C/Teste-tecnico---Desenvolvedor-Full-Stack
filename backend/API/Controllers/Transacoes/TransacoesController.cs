using backend.Application.Services.CategoriaService.Dto;
using backend.Application.Services.CategoriaService.Interface;
using backend.Application.Services.TransacaoService.Dto;
using backend.Application.Services.TransacaoService.Interface;
using Microsoft.AspNetCore.Mvc;

namespace backend.API.Controllers.Categorias; 

[ApiController]
[Route("api/[controller]")]
public class TransacoesController(ITransacaoService transacaoService) : ControllerBase
{
    private readonly ITransacaoService _transacaoService = transacaoService;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TransacaoDto transacaoDto)
    {
        var result = await _transacaoService.CreateTransacao(transacaoDto);
        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpGet()]
    public async Task<IActionResult> Get()
    {
        var result = await _transacaoService.GetTransacoes();
        if (result.IsFailure)
            return NotFound(result.Error);

        return Ok(result.Value);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _transacaoService.GetTransacaoById(id);
        if (result.IsFailure)
            return NotFound(result.Error);

        return Ok(result.Value);
    }
}