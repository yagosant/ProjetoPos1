using Microsoft.EntityFrameworkCore;
using ProjetoPos1.Data;
using ProjetoPos1.Models;

namespace ProjetoPos1.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly AppDbContext _context;

        public PedidoRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Pedido>> GetAllAsync()
        {
            return await _context.Pedidos.ToListAsync();
        }

        public async Task<Pedido?> GetByIdAsync(int id)
        {
            return await _context.Pedidos.FindAsync(id);
        }

        public async Task<IEnumerable<Pedido>> GetByNomeAsync(string nome)
        {
            return await _context.Pedidos
                .Where(p => p.ClienteNome.Contains(nome))
                .ToListAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _context.Pedidos.CountAsync();
        }

        public async Task<Pedido> AddAsync(Pedido pedido)
        {
            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();
            return pedido;
        }

        public async Task<Pedido?> UpdateAsync(Pedido pedido)
        {
            var existente = await _context.Pedidos.FindAsync(pedido.Id);
            if (existente == null) return null;

            existente.ClienteNome = pedido.ClienteNome;
            existente.Produtos = pedido.Produtos;
            existente.DataPedido = pedido.DataPedido;
            existente.Status = pedido.Status;
            existente.ValorTotal = pedido.ValorTotal;

            await _context.SaveChangesAsync();
            return existente;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null) return false;

            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
