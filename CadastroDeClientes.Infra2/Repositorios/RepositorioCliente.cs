using CadastroDeClientes.Domain;
using System.Collections.Generic;
using System.Linq;

namespace CadastroDeClientes.Infra
{
    public class RepositorioCliente : IClienteRepositorio
    {
        public RepositorioCliente repositorioCliente;
        private ListagemDeClientes _listagemDeClientes = ListagemDeClientes.Instance;

        public ListagemDeClientes Context

        {
            get;
            set;
        }

        public void Inserir(Cliente cliente)
        {
            var ultimoId = _listagemDeClientes.Listagem.Count == 0
                ? 0
                : _listagemDeClientes.Listagem.Max(x => x.Id);
            cliente.Id = ultimoId + 1;
            _listagemDeClientes.Listagem.Add(cliente);
        }

        public void Delete(Cliente cliente)
        {
            _listagemDeClientes.Listagem.Remove(cliente);
        }

        public List<Cliente> ObterTodos()
        {
            return _listagemDeClientes.Listagem;
        }

        public Cliente ObterPorId(int id)
        {
            return _listagemDeClientes.Listagem.FirstOrDefault(x => x.Id == id);
        }

        public void Atualizar(Cliente cliente)
        {
            var indice = _listagemDeClientes.Listagem.FindIndex(x => x.Id == cliente.Id);
            _listagemDeClientes.Listagem[indice] = cliente;
        }
    }
}