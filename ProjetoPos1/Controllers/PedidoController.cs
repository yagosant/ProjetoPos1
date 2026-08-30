using Microsoft.AspNetCore.Mvc;
using ProjetoPos1.Models;
using ProjetoPos1.Services;

namespace ProjetoPos1.Controllers
{
    public class PedidoController: ControllerBase
    {
        private readonly IPedidoService _service;

        public PedidoController(IPedidoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetAll()
        {
            var pedidos = await _service.GetAllAsync();
            return Ok(pedidos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> GetById(int id)
        {
            var pedido = await _service.GetByIdAsync(id);
            if (pedido == null) return NotFound();
            return Ok(pedido);
        }

        [HttpGet("nome/{nome}")]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetByNome(string nome)
        {
            var pedidos = await _service.GetByNomeAsync(nome);
            return Ok(pedidos);
        }

        [HttpGet("contar")]
        public async Task<ActionResult<int>> Count()
        {
            var total = await _service.CountAsync();
            return Ok(total);
        }

        [HttpPost]
        public async Task<ActionResult<Pedido>> Create(Pedido pedido)
        {
            try
            {
                var novoPedido = await _service.CreateAsync(pedido);
                return CreatedAtAction(nameof(GetById), new { id = novoPedido.Id }, novoPedido);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Pedido>> Update(int id, Pedido pedido)
        {
            var atualizado = await _service.UpdateAsync(id, pedido);
            if (atualizado == null) return NotFound();
            return Ok(atualizado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var sucesso = await _service.DeleteAsync(id);
            if (!sucesso) return NotFound();
            return NoContent();
        }
    }
}
