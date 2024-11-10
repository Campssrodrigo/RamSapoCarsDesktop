using RamSapoCarsDesktop.Comum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RamSapoCarsDesktop
{
    public partial class frmVendedores : Form
    {
        public frmVendedores()
        {
            InitializeComponent();
        }
        #region Eventos
        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            if (validarCampos())
            {

            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limparCampos();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {

        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {

        }

        private void frmVendedores_Load(object sender, EventArgs e)
        {
            Util.configurarBotoesTela(Util.EstadoTela.Nova, btnCadastrar, btnAlterar, btnExcluir);
        }

        private void grdVendedores_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        #endregion

        #region Métodos privados

        private void limparCampos()
        {
            txtEmail.Clear();
            txtEndereco.Clear();
            txtNomeVendedor.Clear();
            txtTelefone.Clear();
            txtNomeVendedor.Focus();
        }
        private bool validarCampos()
        {
            bool flag = true;
            string campos = string.Empty;

            if(txtEmail.Text.Trim() == string.Empty)
            {
                flag = false;
                campos = "\n -Email";
            }
            if(txtEndereco.Text.Trim() == string.Empty)
            {
                flag = false;
                campos += "\n -Endereço";
            }
            if (txtNomeVendedor.Text.Trim() == string.Empty)
            {
                flag = false;
                campos += "\n -Nome";
            }
            if(txtTelefone.Text.Trim() == string.Empty)
            {
                flag = false;
                campos += "\n -Telefone";
            }

            if (!flag)
            {
                Util.MostarMensagem(Util.TipoMensagem.Obrigatorio, campos);
            }
            return flag;
        }
        #endregion
    }
}
