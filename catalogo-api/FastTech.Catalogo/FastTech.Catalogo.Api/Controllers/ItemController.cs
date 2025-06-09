using FastTech.Catalogo.Application.Interfaces;
using FastTech.Catalogo.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FastTech.Catalogo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetTodos()
        {
            var itens = await _itemService.ListarTodosAsync();
            return Ok(itens);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Item>> GetPorId(Guid id)
        {
            var item = await _itemService.ObterPorIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpGet("tipo/{tipoId:int}")]
        public async Task<ActionResult<IEnumerable<Item>>> GetPorTipo(int tipoId)
        {
            var itens = await _itemService.ListarPorTipoAsync(tipoId);
            return Ok(itens);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Item item)
        {
            await _itemService.AdicionarAsync(item);
            return CreatedAtAction(nameof(GetPorId), new { id = item.Id }, item);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] Item item)
        {
            if (id != item.Id) return BadRequest("ID não confere com o corpo da requisição");

            await _itemService.AtualizarAsync(item);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _itemService.RemoverAsync(id);
            return NoContent();
        }
    }
}
