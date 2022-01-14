using CadastroDeClientes.Domain;
using CadastroDeClientes.UI;
using System;
using System.Windows.Forms;

namespace CadastroDeClientes
{
    public partial class FormMain : Form
    {
        Cliente clienteSelecionado = null;
        private readonly IClienteRepositorio _repositorioCliente;

        public FormMain(IClienteRepositorio _repositorioCliente)
        {
            InitializeComponent();
            this._repositorioCliente = _repositorioCliente;
            dgvClientes.DataSource = _repositorioCliente.ObterTodos();
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            var crud = NinjectRepos.Resolver<Crud>();
            crud.Cliente = new Cliente
            {
                Id = 0,
                DataNascimento = DateTime.Today,
            };
            crud.ShowDialog(this);
            crud.Dispose();
            dgvClientes.DataSource = null;
            dgvClientes.DataSource = _repositorioCliente.ObterTodos();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = MessageBox.Show("Deseja realmente sair?", "Confirmação", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.No;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            DataGridViewRow linhaSelecionada = null;
            if (dgvClientes.SelectedRows.Count > 0)
            {
                linhaSelecionada = dgvClientes.SelectedRows[0];
                clienteSelecionado = linhaSelecionada.DataBoundItem as Cliente;
            }

            if (clienteSelecionado != null)
            {
                var crud = NinjectRepos.Resolver<Crud>();
                crud.Cliente = clienteSelecionado;
                crud.ShowDialog(this);
                clienteSelecionado = null;
                crud.Dispose();
                dgvClientes.DataSource = null;
                dgvClientes.DataSource = _repositorioCliente.ObterTodos();
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            DataGridViewRow linhaSelecionada = null;
            if (dgvClientes.SelectedRows.Count > 0)
            {
                linhaSelecionada = dgvClientes.SelectedRows[0];
                clienteSelecionado = linhaSelecionada.DataBoundItem as Cliente;
            }

            if (clienteSelecionado != null)
            {
                if (MessageBox.Show("Deseja realmente excluir este cliente?", "Confirmação",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _repositorioCliente.Delete(clienteSelecionado);
                }
            }
            dgvClientes.DataSource = null;
            dgvClientes.DataSource = _repositorioCliente.ObterTodos();

            if (dgvClientes.RowCount == 0)
            {
                btnEditar.Enabled = false;
            }
            else
            {
                btnEditar.Enabled = true;
            }

            if (dgvClientes.RowCount == 0)
            {
                btnExcluir.Enabled = false;
            }
            else
            {
                btnExcluir.Enabled = true;
            }
        }

        private void dgvClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvClientes.SelectedRows.Count < 0)
            {
                btnEditar.Enabled = false;
            }
            else
            {
                btnEditar.Enabled = true;
            }

            if (dgvClientes.SelectedRows.Count < 0)
            {
                btnExcluir.Enabled = false;
            }
            else
            {
                btnExcluir.Enabled = true;
            }
        }
    }
}