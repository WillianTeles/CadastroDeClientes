using System;
using System.Collections.Generic;

namespace CadastroDeClientes.Domain
{
    public class ListagemDeClientes
    {
        public List<Cliente> Listagem { get; set; }

        private static readonly Lazy<ListagemDeClientes>
            lazy = new Lazy<ListagemDeClientes>(() => new ListagemDeClientes());

        public static ListagemDeClientes Instance { get { return lazy.Value; } }

        public ListagemDeClientes()
        {
            Listagem = new List<Cliente>();
        }
    }
}