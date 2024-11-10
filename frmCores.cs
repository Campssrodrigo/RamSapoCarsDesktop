using RamSapoCarsDesktop.Comum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RamSapoCarsDesktop
{
    public partial class frmCores : Form
    {
        public frmCores()
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

        private void frmCores_Load(object sender, EventArgs e)
        {
            Util.configurarBotoesTela(Util.EstadoTela.Nova, btnCadastrar, btnAlterar, btnExcluir);
        }

        private void grdCores_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        #endregion

        #region Métodos privados

        private void limparCampos()
        {
            txtCores.Clear();
            chkOffLine.Checked = false;
            txtCores.Focus();
        }

        private bool validarCampos()
        {
            bool flag = true;
            string campos = string.Empty;

            if(txtCores.Text.Trim() == string.Empty)
            {
                flag = false;
                campos = "\n- Cor";
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
