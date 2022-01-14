using CadastroDeClientes.Domain;
using CadastroDeClientes.Infra.DataModel;
using LinqToDB;
using System.Collections.Generic;
using System.Linq;

namespace CadastroDeClientes.Infra
{
    public class RepositorioClienteLinqToDb : IClienteRepositorio
    {
        public void Inserir(Cliente cliente)
        {

            using (var db = new DbClientesDB())
            {
                db.Insert(cliente);
            }
        }

        public void Delete(Cliente cliente)
        {
            using (var db = new DbClientesDB())
            {
                db
                    .GetTable<Cliente>()
                    .Where(t => t.Id == cliente.Id)
                    .Delete();
            }
        }

        public List<Cliente> ObterTodos()
        {
            using (var db = new DbClientesDB())
            {
                IQueryable<Cliente> q =
                    from c in db.Clientes
                    select c;

                return q.ToList();
            }
        }

        public Cliente ObterPorId(int id)
        {
            using (var db = new DbClientesDB())
            {
                var cliente = db
                    .GetTable<Cliente>()
                    .FirstOrDefault(t => t.Id == id);

                return cliente;
            }
        }

        public void Atualizar(Cliente cliente)
        {
            using (var db = new DbClientesDB())
            {
                db
                .GetTable<Cliente>()
                .Where(t => t.Id == cliente.Id)
                .Update(t => new Cliente
                {
                    Nome = cliente.Nome,
                    CPF = cliente.CPF,
                    DataNascimento = cliente.DataNascimento,
                    Telefone = cliente.Telefone
                });
            }
        }
    }
}