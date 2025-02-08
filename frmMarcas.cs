using DAO;
using RamSapoCarsDesktop.Comum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace RamSapoCarsDesktop
{
    public partial class frmMarcas : Form
    {
        public frmMarcas()
        {
            InitializeComponent();
        }
        #region Variáveis Globais
        int idMarca = 0;
        #endregion

        #region Eventos
        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            if (validarCampos())
            {
                if (chkOffLine.Checked == false)
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

                else
                {
                    GerarArquivoXML();
                    limparCampos();
                    Util.configurarBotoesTela(Util.EstadoTela.Nova, btnCadastrar, btnAlterar, btnExcluir);
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
            if (Util.MostarMensagem(Util.TipoMensagem.Confirmacao, $"\n{txtMarca.Text}"))
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

        private void frmMarcas_Load(object sender, EventArgs e)
        {
            Util.configurarBotoesTela(Util.EstadoTela.Nova, btnCadastrar, btnAlterar, btnExcluir);
            Util.ConfigurarGrid(grdMarcas);
            Consultar();
        }

        private void grdMarcas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (grdMarcas.RowCount > 0)
            {
                //Recuperar a informação clicada
                tb_marca objMarcaClicada = (tb_marca)grdMarcas.CurrentRow.DataBoundItem;

                //Popula os campos pra editar ou excluir
                txtMarca.Text = objMarcaClicada.nome_marca;
                idMarca = objMarcaClicada.id_marca;

                Util.configurarBotoesTela(Util.EstadoTela.Edicao, btnCadastrar, btnAlterar, btnExcluir);
            }

        }
        #endregion

        #region Métodos

        private void Excluir()
        {
            new MarcaDAO().ExcluirMarca(idMarca);
            Util.MostarMensagem(Util.TipoMensagem.Sucesso);
        }
        private void Alterar()
        {
            new MarcaDAO().AlterarMarca(new tb_marca
            {
                nome_marca = txtMarca.Text,
                id_marca = idMarca
            });

            Util.MostarMensagem(Util.TipoMensagem.Sucesso);
        }
        private void Cadastrar()
        {
            new MarcaDAO().CadastrarMarca(new tb_marca
            {
                nome_marca = txtMarca.Text,
                id_garagem = Util.CodigoGaragemLogada
            });
            Util.MostarMensagem(Util.TipoMensagem.Sucesso);

        }
        //private void Cadastrarr() 
        //    => new MarcaDAO()
        //    .CadastrarMarca(new tb_marca{nome_marca = txtMarca.Text, id_garagem = Util.CodigoGaragemLogada});
        private void Consultar()
        {
            grdMarcas.DataSource = new MarcaDAO().ConsultarMarcas(Util.CodigoGaragemLogada);
            //Ocultar as colunas indesejadas
            grdMarcas.Columns["id_marca"].Visible = false;
            grdMarcas.Columns["id_garagem"].Visible = false;
            grdMarcas.Columns["tb_garagem"].Visible = false;
            grdMarcas.Columns["tb_modelo"].Visible = false;


            //Mudar o texto do cabeçalho
            grdMarcas.Columns["nome_marca"].HeaderText = "Marca";

        }
        private void limparCampos()
        {
            idMarca = 0;
            txtMarca.Clear();
            txtMarca.Focus();
        }
        private bool validarCampos()
        {
            bool flag = true;
            string campos = string.Empty;

            if (txtMarca.Text.Trim() == string.Empty)
            {
                flag = false;
                campos = "\n - Marca";
            }

            if (!flag)
            {
                Util.MostarMensagem(Util.TipoMensagem.Obrigatorio, campos);
                txtMarca.Focus();
            }
            return flag;
        }

        private void GerarArquivoXML()
        {
            XmlDocument xml = new XmlDocument();

            if (!File.Exists(Util.RetornarCaminhoArquivo(Util.TelaCarregaOff.Marca)))
            {
                XmlElement noMarca = xml.CreateElement("marca");
                xml.AppendChild(noMarca);
            }
            else
            {
                xml.Load(Util.RetornarCaminhoArquivo(Util.TelaCarregaOff.Marca));
            }

            XmlElement noItem = xml.CreateElement("item");

            XmlElement noNome = xml.CreateElement("nome");
            noNome.InnerText = txtMarca.Text.Trim();
            noItem.AppendChild(noNome);


            XmlNode xmlPai = xml.SelectSingleNode("marca");
            xmlPai.AppendChild(noItem);

            xml.Save(Util.RetornarCaminhoArquivo(Util.TelaCarregaOff.Marca));

            Util.MostarMensagem(Util.TipoMensagem.Sucesso);
        }
        #endregion
    }
}
