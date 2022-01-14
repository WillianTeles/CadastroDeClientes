using FluentMigrator;

namespace CadastroDeClientes.WebApp
{
    [Migration(20211203163201)]
    public class AddClienteTable : Migration
    {
        public override void Up()
        {
            Create.Table("Cliente")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Nome").AsString(255).NotNullable()
                .WithColumn("CPF").AsString(11).NotNullable()
                .WithColumn("DataNascimento").AsDateTime().NotNullable()
                .WithColumn("Telefone").AsString(11).NotNullable();
        }

        public override void Down()
        {
            Delete.Table("Cliente");
        }
    }
}