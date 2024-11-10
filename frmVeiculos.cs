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
    public partial class frmVeiculos : Form
    {
        public frmVeiculos()
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

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            new frmConsultarVeiculos().ShowDialog();
        }

        private void frmVeiculos_Load(object sender, EventArgs e)
        {

        }
        #endregion

        #region Métodos privados
        private bool validarCampos()
        {
            bool flag = true;
            string campos = string.Empty;

            if(cbMarca.SelectedIndex == -1)
            {
                flag = false;
                campos = "\n -Marca";
            }
            if(cbCor.SelectedIndex == -1)
            {
                flag = false;
                campos += "\n -Cor";
            }
            if(txtMotor.Text.Trim() == string.Empty)
            {
                flag = false;
                campos += "\n -Motor";
            }
            if(cbCambio.SelectedIndex == -1)
            {
                flag = false;
                campos += "\n -Cambio";
            }
            if(txtKm.Text.Trim() == string.Empty)
            {
                flag = false;
                campos += "\n -KM";
            }
            if(txtValorCompra.Text.Trim() == string.Empty)
            {
                flag = false;
                campos += "\n -Valor da Compra";
            }
            if(cbSituacao.SelectedIndex == -1)
            {
                flag = false;
                campos += "\n -Situação";
            }
            if(cbModelo.SelectedIndex == -1)
            {
                flag = false;
                campos += "\n -Modelo";
            }
            if(txtPlaca.Text.Trim() == string.Empty)
            {
                flag = false;
                campos += "\n -Placa";
            }
            if (cbDirecao.SelectedIndex == -1)
            {
                flag = false;
                campos += "\n -Direção";
            }
            if (cbCombustivel.SelectedIndex == -1)
            {
                flag = false;
                campos += "\n -Combustível";
            }
            if (txtAnoModelo.Text.Trim() == string.Empty)
            {
                flag = false;
                campos += "\n -Ano/Modelo";
            }
            if (txtValorVenda.Text.Trim() == string.Empty)
            {
                flag = false;
                campos += "\n -Valor de Venda";
            }

            if (!flag)
            {
                Util.MostarMensagem(Util.TipoMensagem.Obrigatorio, campos);
            }
            return flag;
        }

        private void limparCampos()
        {
            txtAnoModelo.Clear();
            txtKm.Clear();
            txtMotor.Clear();
            txtObs.Clear();
            txtPlaca.Clear();
            txtValorCompra.Clear();
            txtValorVenda.Clear();
            cbCambio.SelectedIndex = -1;
            cbCombustivel.SelectedIndex = -1;
            cbCor.SelectedIndex = -1;
            cbDirecao.SelectedIndex = -1;
            cbMarca.SelectedIndex = -1;
            cbModelo.SelectedIndex = -1;
            cbSituacao.SelectedIndex = -1;
            cbMarca.Focus();
        }


        #endregion


    }
}
