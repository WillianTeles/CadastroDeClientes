using LinqToDB.Mapping;
using System;
using System.ComponentModel.DataAnnotations;

namespace CadastroDeClientes.Domain
{
    public class Cliente : ICliente
    {
        [PrimaryKey, Identity]
        public int Id { get; set; }

        [Display(Name = "Nome Completo")]
        [Required(ErrorMessage = "Preencher Nome Completo")]
        public string Nome { get; set; }

        [RegularExpression(@"^[0-9]{3}.[0-9]{3}.[0-9]{3}-[0-9]{2}$", ErrorMessage = "CPF Inválido")]
        [Required(ErrorMessage = "Preencher CPF")]
        public string CPF { get; set; }

        [System.ComponentModel.DataAnnotations.DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Data de Nascimento")]
        [Required(ErrorMessage = "Preencher Data de Nascimento")]
        public DateTime? DataNascimento { get; set; }

        [RegularExpression(@"^\([1-9]{2}\)[0-9]{5}-[0-9]{4}$", ErrorMessage = "Telefone Inválido")]
        [Required(ErrorMessage = "Preencher Telefone")]
        public string Telefone { get; set; }
    }
}