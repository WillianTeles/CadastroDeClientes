using System.Collections.Generic;

namespace CadastroDeClientes.Domain
{
    public interface IClienteRepositorio
    {
        public void Inserir(Cliente cliente);
        public void Delete(Cliente cliente);
        public List<Cliente> ObterTodos();
        public Cliente ObterPorId(int id);
        public void Atualizar(Cliente cliente);
    }
}
