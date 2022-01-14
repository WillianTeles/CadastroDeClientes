namespace CadastroDeClientes.UI5.Controllers
{
    public static class StringExtensions
    {
        public static string ClearMask(this string value)
        {
            return value.Replace(".", string.Empty).Replace("-", string.Empty).Replace("/", string.Empty).Replace("(", string.Empty).Replace(")", string.Empty).Replace(" ", string.Empty);
        }
    }
}