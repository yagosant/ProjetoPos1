using ProjetoPos1.Models;

namespace ProjetoPos1.Repositories
{
    public interface IPedidoRepository
    {
            Task<IEnumerable<Pedido>> GetAllAsync();
            Task<Pedido?> GetByIdAsync(int id);
            Task<IEnumerable<Pedido>> GetByNomeAsync(string nome);
            Task<int> CountAsync();
            Task<Pedido> AddAsync(Pedido pedido);
            Task<Pedido?> UpdateAsync(Pedido pedido);
            Task<bool> DeleteAsync(int id);
    }
}
