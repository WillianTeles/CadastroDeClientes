using System;

namespace CadastroDeClientes.Domain
{
    public interface ICliente
    {
        int Id { get; set; }
        string Nome { get; set; }
        string CPF { get; set; }
        DateTime? DataNascimento { get; set; }
        string Telefone { get; set; }
    }
}