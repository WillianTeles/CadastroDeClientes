using CadastroDeClientes.Domain;
using CadastroDeClientes.Infra;
using Ninject.Modules;

namespace CadastroDeClientes.WebApp.Repositorio
{
    public class RepositorioModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IClienteRepositorio>().To<RepositorioClienteLinqToDb>().InSingletonScope();
        }
    }
}