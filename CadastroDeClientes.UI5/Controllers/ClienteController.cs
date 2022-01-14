using CadastroDeClientes.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CadastroDeClientes.UI5.Controllers
{
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepositorio _clienteRepositorio;

        public ClienteController(IClienteRepositorio clienteRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var clientes = _clienteRepositorio.ObterTodos();
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public IActionResult GetClienteId(int id)
        {
            Cliente cliente = _clienteRepositorio.ObterPorId(id);
            return Ok(cliente);
        }

        [HttpPost]
        public IActionResult InserirCliente([FromBody] Cliente cliente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    cliente.CPF = cliente.CPF.ClearMask();
                    cliente.Telefone = cliente.Telefone.ClearMask();
                    _clienteRepositorio.Inserir(cliente);
                    return Ok();
                }
                throw new Exception("Preencha todos os campos!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Conflict(new {message = "Erro ao cadastrar no servidor! \n" + e.Message});
            }
        }

        [HttpPut]
        public IActionResult AlterarCliente([FromBody] Cliente cliente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    cliente.CPF = cliente.CPF.ClearMask();
                    cliente.Telefone = cliente.Telefone.ClearMask();
                    _clienteRepositorio.Atualizar(cliente);
                    return Ok();
                }
                throw new Exception("Preencha todos os campos!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Conflict(new { message = "Erro ao alterar no servidor! \n" + e.Message });
            }
        }

        [HttpDelete("{id}")]
        public Exception DeletarCliente(int id)
        {
            List<ICliente> lista = new();
            var cliente = _clienteRepositorio.ObterPorId(id);
            lista.Add(cliente);
            _clienteRepositorio.Delete(cliente);
            return new Exception("Cliente deletado com sucesso!");
        }
    }
}