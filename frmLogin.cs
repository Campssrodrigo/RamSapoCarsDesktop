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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        #region Eventos
        private void btnAcessar_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {

            }
        }
        #endregion

        #region Métodos
        private bool ValidarCampos()
        {
            bool flag = true;
            string campos = string.Empty;

            if (txtLogin.Text.Trim() == string.Empty)
            {
                flag = false;
                campos = "\n- Login";
            }
            if (txtSenha.Text.Trim() == string.Empty)
            {
                flag = false;
                campos += "\n- Senha";
            }

            if (!flag)
            {
                Util.MostarMensagem(Util.TipoMensagem.Obrigatorio, campos);
                if (txtLogin.Text.Trim() == string.Empty)
                {
                    txtLogin.Focus();
                }
                else if (txtSenha.Text.Trim() == string.Empty)
                {
                    txtSenha.Focus();
                }
                else
                {
                    txtLogin.Focus();
                }

            }
            return flag;
        }
        #endregion
    }
}
