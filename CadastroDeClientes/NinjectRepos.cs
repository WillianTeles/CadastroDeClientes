using CadastroDeClientes.Domain;
using CadastroDeClientes.Infra;
using Ninject;
using Ninject.Modules;

namespace CadastroDeClientes.UI
{
    internal class NinjectRepos
    {
        public static IKernel _ninjectKernel;
        public static void ComponenteModulo(INinjectModule module)
        {
            _ninjectKernel = new StandardKernel(module);
        }
        public static T Resolver<T>()
        {
            return _ninjectKernel.Get<T>();
        }
    }

    public class ModuloAplicacao : NinjectModule
    {
        public override void Load()
        {
            Bind<IClienteRepositorio>().To<RepositorioClienteLinqToDb>();
        }
    }
}