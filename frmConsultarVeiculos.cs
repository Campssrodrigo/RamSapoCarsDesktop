using DAO;
using DAO.VO;
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
    public partial class frmConsultarVeiculos : Form
    {
        public frmConsultarVeiculos(frmVeiculos f)
        {
            InitializeComponent();
            telaVeiculo = f;
        }

        frmVeiculos telaVeiculo;
        #region Eventos
        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (validarCampos())
            {
                Filtrar();
            }
        }

        private void frmConsultarVeiculos_Load(object sender, EventArgs e)
        {
            Util.ConfigurarGrid(grdVeiculos);
            Util.ConfigurarCombo(cbMarca, "nome_marca", "id_marca");
            Util.ConfigurarCombo(cbModelo, "nome_modelo", "id_modelo");
            CarregarMarca();
            LimparCampos();
        }
        private void grdVeiculos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(grdVeiculos.RowCount > 0)
            {
                VeiculoVO objLinhaClicada = (VeiculoVO)grdVeiculos.CurrentRow.DataBoundItem;

                telaVeiculo.CarregarCampos(objLinhaClicada.ObjEdicao);
                this.Close();
            }
        }
        private void cbMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            HabilitarModelo();
        }

        #endregion

        #region Métodos privados
        private void Filtrar()
        {
            List<VeiculoVO> lstRetorno = new VeiculoDAO().FiltrarVeiculo(Util.CodigoGaragemLogada, (int)cbModelo.SelectedValue);

            if (lstRetorno.Count > 0)
            {
                grdVeiculos.DataSource = lstRetorno;
                grdVeiculos.Columns["ObjEdicao"].Visible = false;
            }
            else
            {
                Util.MostarMensagem(Util.TipoMensagem.RegistroNaoEncontrado);
                grdVeiculos.DataSource = null;
            }
        }

        private bool validarCampos()
        {
            bool flag = true;
            string campos = string.Empty;

            if(cbMarca.SelectedIndex == -1 && cbModelo.SelectedIndex == -1)
            {
                flag = false;
                campos = "\n-Ao menos um dos filtros devem estar selecionados.";
            }

            if(cbModelo.SelectedIndex == -1)
            {
                flag = false;
                campos = "\n-Para a pesquisa é necessário a seleção do modelo.";
            }

            if (!flag)
            {
                Util.MostarMensagem(Util.TipoMensagem.Obrigatorio, campos);
            }

            return flag;
        }

        private void LimparCampos()
        {
            cbMarca.SelectedIndex = -1;
            cbModelo.SelectedIndex = -1;
        }

        private void CarregarMarca()
        {
            cbMarca.DataSource = new MarcaDAO().ConsultarMarcas(Util.CodigoGaragemLogada);
        }

        private void CarregarModelo()
        {
            cbModelo.DataSource = new ModeloDAO().FiltrarModelo(Util.CodigoGaragemLogada, (int)cbMarca.SelectedValue);
        }
        private void HabilitarModelo()
        {
            if (cbMarca.SelectedIndex >= 0)
            {
                cbModelo.Enabled = true;
                btnPesquisar.Enabled = true;
                CarregarModelo();
            }
            else
            {
                cbModelo.Enabled = false;
                btnPesquisar.Enabled = false;
            }
             
        }
        #endregion


    }
}
