using backend.Application.Services.PessoaService.Dto;
using backend.Application.Services.PessoaService.Interface;
using Microsoft.AspNetCore.Mvc;

namespace backend.API.Controllers.Pessoas; 

[ApiController]
[Route("api/[controller]")]
public class PessoasController(IPessoaService pessoaService) : ControllerBase
{
    private readonly IPessoaService _pessoaService = pessoaService;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PessoaDto pessoaDto)
    {
        var result = await _pessoaService.CreatePessoa(pessoaDto);
        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _pessoaService.DeletePessoa(id);
        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpGet()]
    public async Task<IActionResult> Get()
    {
        var result = await _pessoaService.GetPessoas();
        if (result.IsFailure)
            return NotFound(result.Error);

        return Ok(result.Value);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _pessoaService.GetPessoaById(id);
        if (result.IsFailure)
            return NotFound(result.Error);

        return Ok(result.Value);
    }

    [HttpGet("total")]
    public async Task<IActionResult> GetTotalByPessoas()
    {
        var result = await _pessoaService.GetTotalByPessoas();
        if (result.IsFailure)
            return NotFound(result.Error);

        return Ok(result.Value);
    }
}