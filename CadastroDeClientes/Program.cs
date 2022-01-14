using CadastroDeClientes.Domain;
using LinqToDB.Data;
using System;
using System.Windows.Forms;

namespace CadastroDeClientes.UI
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            NinjectRepos.ComponenteModulo(new ModuloAplicacao());
            DataConnection.DefaultSettings = new MySettings();
            Application.Run(NinjectRepos.Resolver<FormMain>());
        }
    }
}
