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
    public partial class frmConsultarVendasVendedores : Form
    {
        public frmConsultarVendasVendedores()
        {
            InitializeComponent();
        }
        #region Eventos
        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (validarCampos())
            {

            }
        }

        private void grdResultado_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmConsultarVendasVendedores_Load(object sender, EventArgs e)
        {

        }

        #endregion

        #region Métodos Privados
        private bool validarCampos()
        {
            bool flag = true;
            string campos = string.Empty;
            DateTime dataInicial = Convert.ToDateTime(dtpInicial.Text);
            DateTime dataFinal = Convert.ToDateTime(dtpFinal.Text);

            if (cbVendedor.SelectedIndex == -1)
            {
                flag = false;
                campos = "\n -Vendedor";
            }
            if (dtpInicial.Value.ToString() == string.Empty)
            {
                flag = false;
                campos += "\n -Data Inicial";
            }
            else if (dataInicial > dataFinal)
            {
                flag = false;
                campos += "\n -As datas estão com a ordem cronológica incorreta.";
            }
            
            if(dtpFinal.Value.ToString() == string.Empty)
            {
                flag = false;
                campos += "\n -Data Final";
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
