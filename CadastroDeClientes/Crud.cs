using CadastroDeClientes.Domain;
using System;
using System.Windows.Forms;

namespace CadastroDeClientes
{
    public partial class Crud : Form
    {

        public Cliente Cliente = null;
        public IClienteRepositorio _repositorioCliente;

        public Crud(IClienteRepositorio _repositorioCliente)
        {
            InitializeComponent();
            this._repositorioCliente = _repositorioCliente;
        }

        private bool PreencheuTodosOsCampos()
        {
            if (String.IsNullOrWhiteSpace(txtNome.Text)) return false;
            if (String.IsNullOrWhiteSpace(mtbCPF.Text)) return false;
            if (dtpDataNascimento.Value.Date == DateTime.Now.Date) return false;
            if (String.IsNullOrWhiteSpace(mtbTelefone.Text)) return false;

            return true;
        }

        private bool PossuiValoresNaoSalvos()
        {
            if (!String.IsNullOrWhiteSpace(txtNome.Text)) return true;
            if (!String.IsNullOrWhiteSpace(mtbCPF.Text)) return true;
            if (dtpDataNascimento.Value.Date != DateTime.Now.Date) return true;
            if (!String.IsNullOrWhiteSpace(mtbTelefone.Text)) return true;

            return false;
        }

        private bool PossuiAlteracao()
        {
            if (txtNome.Text.Trim() != Cliente.Nome) return true;
            if (mtbCPF.Text != Cliente.CPF.ToString()) return true;
            if (dtpDataNascimento.Value.Date != Cliente.DataNascimento) return true;
            if (mtbTelefone.Text != Cliente.Telefone.ToString()) return true;

            return false;
        }

        private void Crud_Load(object sender, EventArgs e)
        {
            txtCodigo.Text = Cliente.Id.ToString();
            txtNome.Text = Cliente.Nome;
            mtbCPF.Text = Cliente.CPF;
            dtpDataNascimento.Value = (DateTime)Cliente.DataNascimento;
            mtbTelefone.Text = Cliente.Telefone;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (PreencheuTodosOsCampos())
            {
                Cliente.Nome = txtNome.Text;
                Cliente.CPF = mtbCPF.Text;
                Cliente.DataNascimento = dtpDataNascimento.Value.Date;
                Cliente.Telefone = mtbTelefone.Text;

                if (Cliente.Id == 0)
                {                    
                    _repositorioCliente.Inserir(Cliente);
                    MessageBox.Show("Cliente cadastrado com sucesso!", "Informação!", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    _repositorioCliente.Atualizar(Cliente);
                    MessageBox.Show("Alterações realizadas com sucesso!", "Informação!", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                }
                Close();
            }
            else
            {
                MessageBox.Show("Só é possível salvar se todos os campos forem preenchidos.",
                    "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            if (PossuiAlteracao())
            {
                if (PossuiValoresNaoSalvos())
                {
                    if
                        (MessageBox.Show("Há alterações não salvas. Deseja realmente cancelar?", "Informação!",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Close();
                    }
                }
                else
                {
                    MessageBox.Show("Nenhum alteração foi feita!", "Sem alterações!",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
            }
            else
            {
                MessageBox.Show("Nenhum alteração foi feita!", "Sem alterações!",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
        }
    }
}