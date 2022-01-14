using CadastroDeClientes.Domain;
using CadastroDeClientes.UI5.Controllers;
using System.Data;
using System.Web.Mvc;

namespace CadastroDeClientes.WebApp.Controllers
{
    public class ClientesController : Controller
    {
        private readonly IClienteRepositorio _clienteRepositorio;

        public ClientesController(IClienteRepositorio clienteRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
        }

        public ActionResult Index()
        {
            {
                var clientes = _clienteRepositorio.ObterTodos();
                return View(clientes);
            }
        }

        public ViewResult Details(int id)
        {
            Cliente cliente = _clienteRepositorio.ObterPorId(id);
            return View(cliente);
        }

        public ActionResult Create()
        {
            return View(new Cliente());
        }

        [HttpPost]
        public ActionResult Create(Cliente cliente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    cliente.CPF = cliente.CPF.ClearMask();
                    cliente.Telefone = cliente.Telefone.ClearMask();
                    _clienteRepositorio.Inserir(cliente);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(cliente);
        }

        public ActionResult Edit(int id)
        {
            Cliente cliente = _clienteRepositorio.ObterPorId(id);
            return View(cliente);
        }

        [HttpPost]
        public ActionResult Edit(Cliente cliente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    cliente.CPF = cliente.CPF.ClearMask();
                    cliente.Telefone = cliente.Telefone.ClearMask();
                    _clienteRepositorio.Atualizar(cliente);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(cliente);
        }

        public ActionResult Delete(int id, bool? saveChangesError)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Unable to save changes. Try again, and if the problem persists see your system administrator.";
            }
            Cliente cliente = _clienteRepositorio.ObterPorId(id);
            return View(cliente);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Cliente cliente = _clienteRepositorio.ObterPorId(id);
                _clienteRepositorio.Delete(cliente);
                return RedirectToAction("Index");
            }
            catch (DataException)
            {
                return RedirectToAction(
                    "Delete",
                    new System.Web.Routing.RouteValueDictionary {
                        { "id", id },
                        { "saveChangesError", true }
                   }
                );
            }
        }
    }
}