using backend.Application.Services.CategoriaService.Dto;
using backend.Application.Services.CategoriaService.Interface;
using Microsoft.AspNetCore.Mvc;

namespace backend.API.Controllers.Categorias; 

[ApiController]
[Route("api/[controller]")]
public class CategoriasController(ICategoriaService categoriaservice) : ControllerBase
{
    private readonly ICategoriaService _categoriaservice = categoriaservice;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CategoriaDto categoriaDto)
    {
        var result = await _categoriaservice.CreateCategoria(categoriaDto);
        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpGet()]
    public async Task<IActionResult> Get()
    {
        var result = await _categoriaservice.GetCategorias();
        if (result.IsFailure)
            return NotFound(result.Error);

        return Ok(result.Value);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _categoriaservice.GetCategoriaById(id);
        if (result.IsFailure)
            return NotFound(result.Error);

        return Ok(result.Value);
    }

    [HttpGet("total")]
    public async Task<IActionResult> GetTotalByCategorias()
    {
        var result = await _categoriaservice.GetTotalByCategorias();
        if (result.IsFailure)
            return NotFound(result.Error);

        return Ok(result.Value);
    }
}