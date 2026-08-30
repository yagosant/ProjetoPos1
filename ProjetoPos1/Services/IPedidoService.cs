using ProjetoPos1.Models;

namespace ProjetoPos1.Services
{
    public interface IPedidoService
    {
        Task<IEnumerable<Pedido>> GetAllAsync();
        Task<Pedido?> GetByIdAsync(int id);
        Task<IEnumerable<Pedido>> GetByNomeAsync(string nome);
        Task<int> CountAsync();
        Task<Pedido> CreateAsync(Pedido pedido);
        Task<Pedido?> UpdateAsync(int id, Pedido pedido);
        Task<bool> DeleteAsync(int id);
    }
}
