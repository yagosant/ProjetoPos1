using ProjetoPos1.Models;
using ProjetoPos1.Repositories;

namespace ProjetoPos1.Services
{
    public class PedidoService: IPedidoService
    {
        private readonly IPedidoRepository _repository;
        public PedidoService(IPedidoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Pedido>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Pedido?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Pedido>> GetByNomeAsync(string nome)
        {
            return await _repository.GetByNomeAsync(nome);
        }

        public async Task<int> CountAsync()
        {
            return await _repository.CountAsync();
        }

        public async Task<Pedido> CreateAsync(Pedido pedido)
        {
            // Aqui entram regras de negócio, ex.: validar se tem ao menos 1 produto
            if (pedido.Produtos == null || !pedido.Produtos.Any())
                throw new ArgumentException("O pedido deve ter ao menos um produto.");

            pedido.DataPedido = DateTime.UtcNow;
            pedido.Status = StatusPedido.Pendente;

            return await _repository.AddAsync(pedido);
        }

        public async Task<Pedido?> UpdateAsync(int id, Pedido pedido)
        {
            pedido.Id = id;
            return await _repository.UpdateAsync(pedido);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
