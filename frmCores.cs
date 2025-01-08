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
using DAO;

namespace RamSapoCarsDesktop
{
    public partial class frmCores : Form
    {

        public frmCores()
        {
            InitializeComponent();
        }
        int idCor = 0;

        #region Eventos


        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            if (validarCampos())
            {
                try
                {
                    Cadastrar();
                    limparCampos();
                    Consultar();
                    Util.configurarBotoesTela(Util.EstadoTela.Nova, btnCadastrar, btnAlterar, btnExcluir);
                }
                catch
                {
                    Util.MostarMensagem(Util.TipoMensagem.Erro);
                }
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limparCampos();
            Util.configurarBotoesTela(Util.EstadoTela.Nova, btnCadastrar, btnAlterar, btnExcluir);

        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (validarCampos())
            {
                try
                {
                    Alterar();
                    limparCampos();
                    Consultar();
                    Util.configurarBotoesTela(Util.EstadoTela.Nova, btnCadastrar, btnAlterar, btnExcluir);
                }
                catch
                {
                    Util.MostarMensagem(Util.TipoMensagem.Erro);
                }

            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (Util.MostarMensagem(Util.TipoMensagem.Confirmacao, $"\n{txtCores.Text}"))
            {
                try
                {
                    Excluir();
                    limparCampos();
                    Consultar();
                    Util.configurarBotoesTela(Util.EstadoTela.Nova, btnCadastrar, btnAlterar, btnExcluir);
                }
                catch
                {
                    Util.MostarMensagem(Util.TipoMensagem.Erro);
                }

            }
        }

        private void frmCores_Load(object sender, EventArgs e)
        {
            Util.configurarBotoesTela(Util.EstadoTela.Nova, btnCadastrar, btnAlterar, btnExcluir);
            Util.ConfigurarGrid(grdCores);
            Consultar();
        }

        private void grdCores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Verfica se tem a informação
            if (grdCores.RowCount > 0)
            {
                //Recuperar a informação clicada
                tb_cor objCorClicada = (tb_cor)grdCores.CurrentRow.DataBoundItem;

                //Popula os campos pra editar ou excluir
                txtCores.Text = objCorClicada.nome_cor;
                idCor = objCorClicada.id_cor;

                Util.configurarBotoesTela(Util.EstadoTela.Edicao, btnCadastrar, btnAlterar, btnExcluir);
            }
        }

        #endregion

        #region Métodos privados

        private void Alterar()
        {
            //Criar o obj para ser consumido so métodos
            CorDAO objdao = new CorDAO();
            //Criar o obj para preencher sua info(propriedades)
            tb_cor objCor = new tb_cor();

            objCor.nome_cor = txtCores.Text;
            objCor.id_cor = idCor;

            objdao.AlterarCor(objCor);
            Util.MostarMensagem(Util.TipoMensagem.Sucesso);
        }

        private void Cadastrar()
        {
            //Criar o obj para ser consumido so métodos
            CorDAO objdao = new CorDAO();
            //Criar o obj para preencher sua info(propriedades)
            tb_cor objCor = new tb_cor();

            //Popular as propriedades de acordo com a tela
            objCor.nome_cor = txtCores.Text;
            objCor.id_garagem = Util.CodigoGaragemLogada;


            objdao.CadastrarCor(objCor);
            Util.MostarMensagem(Util.TipoMensagem.Sucesso);
        }

        private void Consultar()
        {
            CorDAO objdao = new CorDAO();
            List<tb_cor> lstReceberCores = objdao.ConsultarCor(Util.CodigoGaragemLogada);
            grdCores.DataSource = lstReceberCores;

            //Ocultar as colunas indesejadas
            grdCores.Columns["id_cor"].Visible = false;
            grdCores.Columns["id_garagem"].Visible = false;
            grdCores.Columns["tb_garagem"].Visible = false;
            grdCores.Columns["tb_veiculo"].Visible = false;

            //Mudar o texto do cabeçalho
            grdCores.Columns["nome_cor"].HeaderText = "Cores";


        }

        private void Excluir()
        {
            //Criar o obj para ser consumido so métodos
            CorDAO objdao = new CorDAO();
            //Criar o obj para preencher sua info(propriedades)
            tb_cor objCor = new tb_cor();

            objCor.id_cor = idCor;

            objdao.ExcluirCor(idCor);

        }
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

            if (txtCores.Text.Trim() == string.Empty)
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
