using Microsoft.AspNetCore.Mvc;
using ProjetoPos1.Models;
using ProjetoPos1.Services;

namespace ProjetoPos1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController: ControllerBase
    {
        private readonly IPedidoService _service;

        public PedidoController(IPedidoService service)
        {
            _service = service;
        }

        /// <summary>Lista todos os pedidos cadastrados.</summary>
        /// <response code="200">Lista de pedidos retornada com sucesso.</response>
        [HttpGet("ListarTodosPedidos")]
        [ProducesResponseType(typeof(IEnumerable<Pedido>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Pedido>>> ListarPedidos()
        {
            var pedidos = await _service.GetAllAsync();
            return Ok(pedidos);
        }

        /// <summary>Busca um pedido específico pelo ID.</summary>
        /// <response code="200">Pedido encontrado.</response>
        /// <response code="404">Nenhum pedido encontrado com esse ID.</response>
        [HttpGet("BuscarPedido/{id}")]
        [ProducesResponseType(typeof(Pedido), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Pedido>> BuscarPedidoPorId(int id)
        {
            var pedido = await _service.GetByIdAsync(id);
            if (pedido == null)
                return NotFound(new { mensagem = $"Pedido com Id {id} não encontrado." });

            return Ok(pedido);
        }

        /// <summary>Busca pedidos pelo nome do cliente (busca parcial).</summary>
        /// <response code="200">Lista de pedidos correspondentes (pode ser vazia).</response>
        [HttpGet("BuscaPedidoParcial/{nome}")]
        [ProducesResponseType(typeof(IEnumerable<Pedido>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Pedido>>> BuscarPedidosPorNome(string nome)
        {
            var pedidos = await _service.GetByNomeAsync(nome);
            return Ok(pedidos);
        }

        /// <summary>Retorna o total de pedidos cadastrados.</summary>
        /// <response code="200">Total retornado com sucesso.</response>
        [HttpGet("ContarPedidos")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<ActionResult<int>> ContarPedidos()
        {
            var total = await _service.CountAsync();
            return Ok(total);
        }

        /// <summary>Cria um novo pedido.</summary>
        /// <response code="201">Pedido criado com sucesso.</response>
        /// <response code="400">Dados inválidos (ex.: pedido sem produtos).</response>
        [HttpPost("CriarPedido")]
        [ProducesResponseType(typeof(Pedido), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pedido>> CriarPedido(Pedido pedido)
        {
            try
            {
                var novoPedido = await _service.CreateAsync(pedido);
                return CreatedAtAction(nameof(BuscarPedidoPorId), new { id = novoPedido.Id }, novoPedido);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        /// <summary>Atualiza um pedido existente.</summary>
        /// <response code="200">Pedido atualizado com sucesso.</response>
        /// <response code="404">Nenhum pedido encontrado com esse ID.</response>
        [HttpPut("AtualizarPedido/{id}")]
        [ProducesResponseType(typeof(Pedido), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Pedido>> AtualizarPedido(int id, Pedido pedido)
        {
            var atualizado = await _service.UpdateAsync(id, pedido);
            if (atualizado == null)
                return NotFound(new { mensagem = $"Pedido com Id {id} não encontrado." });

            return Ok(atualizado);
        }

        /// <summary>Remove um pedido pelo ID.</summary>
        /// <response code="204">Pedido removido com sucesso.</response>
        /// <response code="404">Nenhum pedido encontrado com esse ID.</response>
        [HttpDelete("RemoverPedido/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoverPedido(int id)
        {
            var sucesso = await _service.DeleteAsync(id);
            if (!sucesso)
                return NotFound(new { mensagem = $"Pedido com Id {id} não encontrado." });

            return NoContent();
        }
    }
}
