using DAO;
using RamSapoCarsDesktop.Comum;
using RamSapoCarsDesktop.VO;
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
    public partial class frmModelos : Form
    {
        public frmModelos()
        {
            InitializeComponent();
        }

        #region Variáveis Globais
        int idModelo = 0;
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
            if (Util.MostarMensagem(Util.TipoMensagem.Confirmacao, $"\n{txtNomeModelo.Text}"))
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

        private void frmModelos_Load(object sender, EventArgs e)
        {
            Util.configurarBotoesTela(Util.EstadoTela.Nova, btnCadastrar, btnAlterar, btnExcluir);
            Util.ConfigurarGrid(grdModelos);
            CarregarMarcas();
            Consultar();
            limparCampos();
        }

        private void grdModelos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (grdModelos.RowCount > 0)
            {
                ModeloVO objLinhaClickada = (ModeloVO)grdModelos.CurrentRow.DataBoundItem;
                txtNomeModelo.Text = objLinhaClickada.ObjEdicao.nome_modelo;
                cbMarca.SelectedValue = objLinhaClickada.ObjEdicao.id_marca;
                idModelo = objLinhaClickada.ObjEdicao.id_modelo;
                Util.configurarBotoesTela(Util.EstadoTela.Edicao, btnCadastrar, btnAlterar, btnExcluir);
            }
        }
        #endregion

        #region Métodos
        private void Cadastrar()
        {
            new ModeloDAO().Cadastrar(new tb_modelo
            {
                id_garagem = Util.CodigoGaragemLogada,
                id_marca = (int)cbMarca.SelectedValue,
                nome_modelo = txtNomeModelo.Text
            });
            Util.MostarMensagem(Util.TipoMensagem.Sucesso);
        }
        private void Alterar()
        {
            new ModeloDAO().Alterar(new tb_modelo
            {
                id_modelo = idModelo,
                id_marca = (int)cbMarca.SelectedValue,
                nome_modelo = txtNomeModelo.Text,
            });
            Util.MostarMensagem(Util.TipoMensagem.Sucesso);
        }
        private void Excluir()
        {
            new ModeloDAO().Excluir(idModelo);
            
            Util.MostarMensagem(Util.TipoMensagem.Sucesso);
        }
        private void Consultar()
        {
            grdModelos.DataSource = new ModeloDAO().Consultar(Util.CodigoGaragemLogada);
            grdModelos.Columns["ObjEdicao"].Visible = false;
        }
        private void CarregarMarcas()
        {
            List<tb_marca> lstMarcas = new MarcaDAO().ConsultarMarcas(Util.CodigoGaragemLogada);
            cbMarca.DisplayMember = "nome_marca";
            cbMarca.ValueMember = "id_marca";
            cbMarca.DataSource = lstMarcas;
        }

        private void limparCampos()
        {
            idModelo = 0;
            txtNomeModelo.Clear();
            cbMarca.SelectedIndex = -1;
            txtNomeModelo.Focus();
            chkOffLine.Checked = false;
        }

        private bool validarCampos()
        {
            bool flag = true;
            string campos = string.Empty;

            if (txtNomeModelo.Text.Trim() == string.Empty)
            {
                flag = false;
                campos = "\n -Nome";
            }

            if (cbMarca.SelectedIndex == -1)
            {
                flag = false;
                campos += "\n -Marca";
            }

            if (!flag)
            {
                Util.MostarMensagem(Util.TipoMensagem.Obrigatorio, campos);
            }

            return flag;
        }

        private void GerarArquivoXML()
        {
            XmlDocument xml = new XmlDocument();

            if (!File.Exists(Util.RetornarCaminhoArquivo(Util.TelaCarregaOff.Modelo)))
            {
                XmlElement noModelo = xml.CreateElement("modelo");
                xml.AppendChild(noModelo);
            }
            else
            {
                xml.Load(Util.RetornarCaminhoArquivo(Util.TelaCarregaOff.Modelo));
            }

            XmlElement noItem = xml.CreateElement("item");

            XmlElement noIdMarca = xml.CreateElement("idmarca");
            noIdMarca.InnerText = cbMarca.SelectedValue.ToString();
            noItem.AppendChild(noIdMarca);

            XmlElement noMarca = xml.CreateElement("marca");
            noMarca.InnerText = cbMarca.Text;
            noItem.AppendChild(noMarca);

            XmlElement noNome = xml.CreateElement("nome");
            noNome.InnerText = txtNomeModelo.Text.Trim();
            noItem.AppendChild(noNome);


            XmlNode xmlPai = xml.SelectSingleNode("modelo");
            xmlPai.AppendChild(noItem);

            xml.Save(Util.RetornarCaminhoArquivo(Util.TelaCarregaOff.Modelo));

            Util.MostarMensagem(Util.TipoMensagem.Sucesso);
        }
        #endregion
    }
}
