using DAO;
using DAO.Interfaces;
using RamSapoCarsDesktop.Comum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace RamSapoCarsDesktop
{
    public partial class frmVendedores : Form
    {
        public frmVendedores()
        {
            InitializeComponent();
        }

        #region Variáveis Globais
        int idVendedor = 0;
        #endregion

        #region Eventos
        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            if (chkOffLine.Checked)
            {
                GerarArquivoXML();
                limparCampos();
                Util.configurarBotoesTela(Util.EstadoTela.Nova, btnCadastrar, btnAlterar, btnExcluir);
            }
            else 
            {
                if (validarCampos())
                {
                    if (!VerificarEmailExistente())
                    {
                        Util.MostarMensagem(Util.TipoMensagem.EmailDuplicado);
                    }
                    else
                    {
                        try
                        {
                            Cadastrar();
                            limparCampos();
                            Filtrar();
                            Util.configurarBotoesTela(Util.EstadoTela.Nova, btnCadastrar, btnAlterar, btnExcluir);
                        }
                        catch
                        {
                            Util.MostarMensagem(Util.TipoMensagem.Erro);
                        }
                    }

                }
            }
           
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limparCampos();
            Util.configurarBotoesTela(Util.EstadoTela.Nova, btnCadastrar, btnAlterar, btnExcluir);
            Util.ConfigurarGrid(grdVendedores);
        }
        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (validarCampos())
            {
                    if (!VerificarEmailExistente())
                    {
                        Util.MostarMensagem(Util.TipoMensagem.EmailDuplicado);
                    }
                    else
                    {

                        try
                        {
                            Alterar();
                            limparCampos();
                            Filtrar();
                            Util.configurarBotoesTela(Util.EstadoTela.Nova, btnCadastrar, btnAlterar, btnExcluir);
                        }
                        catch
                        {
                            Util.MostarMensagem(Util.TipoMensagem.Erro);
                        }
                    }
            }
        }
        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (Util.MostarMensagem(Util.TipoMensagem.Confirmacao, $"\n{txtNomeVendedor.Text}"))
            {
                try
                {
                    Excluir();
                    limparCampos();
                    Filtrar();
                    Util.configurarBotoesTela(Util.EstadoTela.Nova, btnCadastrar, btnAlterar, btnExcluir);
                }
                catch
                {
                    Util.MostarMensagem(Util.TipoMensagem.Erro);
                }
            }
        }
        private void frmVendedores_Load(object sender, EventArgs e)
        {
            Util.configurarBotoesTela(Util.EstadoTela.Nova, btnCadastrar, btnAlterar, btnExcluir);
            Util.ConfigurarGrid(grdVendedores);
            Filtrar();
        }
        private void grdVendedores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (grdVendedores.RowCount > 0)
            {
                //Recuperar a informação clicada
                tb_vendedor objVendedorClicado = (tb_vendedor)grdVendedores.CurrentRow.DataBoundItem;

                //Popula os campos pra editar ou excluir
                txtNomeVendedor.Text = objVendedorClicado.nome_vendedor;
                idVendedor = objVendedorClicado.id_vendedor;
                txtEmail.Text = objVendedorClicado.email_vendedor;
                txtTelefone.Text = objVendedorClicado.tel_vendedor;
                chkStatus.Checked = objVendedorClicado.status_vendedor;
                txtEndereco.Text = objVendedorClicado.endereco_vendedor;

                Util.configurarBotoesTela(Util.EstadoTela.Edicao, btnCadastrar, btnAlterar, btnExcluir);
            }
        }
        private void txtEmail_Leave(object sender, EventArgs e)
        {
            if (!Util.VerficarEmail(txtEmail.Text))
            {
                Util.MostarMensagem(Util.TipoMensagem.EmailIncorreto);
                txtEmail.Clear();
                txtEmail.Focus();
            }
        }
        private void txtNomePesquisa_TextChanged(object sender, EventArgs e)
        {
            Filtrar();
        }

        #endregion

        #region Métodos privados 
        private bool VerificarEmailExistente()
            => new VendedorDAO().VerificarEmailExistente(txtEmail.Text, idVendedor);
        private void Cadastrar()
        {
            new VendedorDAO().Cadastrar(new tb_vendedor
            {
                email_vendedor = txtEmail.Text,
                endereco_vendedor = txtEndereco.Text,
                id_garagem = Util.CodigoGaragemLogada,
                nome_vendedor = txtNomeVendedor.Text,
                senha_vendedor = Util.CriarSenha(txtEmail.Text),
                status_vendedor = chkStatus.Checked,
                tel_vendedor = txtTelefone.Text
            });
            Util.MostarMensagem(Util.TipoMensagem.Sucesso);
        }
        private void Alterar()
        {
            new VendedorDAO().Alterar(new tb_vendedor
            {
                email_vendedor = txtEmail.Text,
                endereco_vendedor = txtEndereco.Text,
                id_vendedor = idVendedor,
                nome_vendedor = txtNomeVendedor.Text,
                status_vendedor = chkStatus.Checked,
                tel_vendedor = txtTelefone.Text
            });
            Util.MostarMensagem(Util.TipoMensagem.Sucesso);
        }
        private void Excluir()
        {
            new VendedorDAO().Excluir(idVendedor);
            Util.MostarMensagem(Util.TipoMensagem.Sucesso);
        }
        private void Filtrar()
        {
            grdVendedores.DataSource = new VendedorDAO().Filtrar(txtNomePesquisa.Text, Util.CodigoGaragemLogada);
            //Ocultar
            grdVendedores.Columns["id_vendedor"].Visible = false;

            grdVendedores.Columns["senha_vendedor"].Visible = false;
            grdVendedores.Columns["id_garagem"].Visible = false;
            grdVendedores.Columns["tb_cliente"].Visible = false;
            grdVendedores.Columns["tb_garagem"].Visible = false;
            grdVendedores.Columns["tb_venda"].Visible = false;


            grdVendedores.Columns["nome_vendedor"].HeaderText = "Vendedor";
            grdVendedores.Columns["email_vendedor"].HeaderText = "E-Mail";
            grdVendedores.Columns["endereco_vendedor"].HeaderText = "Endereço";
            grdVendedores.Columns["tel_vendedor"].HeaderText = "Telefone";
            grdVendedores.Columns["status_vendedor"].HeaderText = "Status";

        }
        private void limparCampos()
        {
            idVendedor = 0;
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

            if (txtEmail.Text.Trim() == string.Empty)
            {
                flag = false;
                campos = "\n -Email";
            }
            if (txtEndereco.Text.Trim() == string.Empty)
            {
                flag = false;
                campos += "\n -Endereço";
            }
            if (txtNomeVendedor.Text.Trim() == string.Empty)
            {
                flag = false;
                campos += "\n -Nome";
            }
            if (txtTelefone.Text.Trim() == string.Empty)
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

        private void GerarArquivoXML()
        {
            XmlDocument xml = new XmlDocument();

            if (!File.Exists(Util.RetornarCaminhoArquivo(Util.TelaCarregaOff.Vendedor)))
            {
                XmlElement noVendedor = xml.CreateElement("vendedor");
                xml.AppendChild(noVendedor);
            }
            else
            {
                xml.Load(Util.RetornarCaminhoArquivo(Util.TelaCarregaOff.Vendedor));
            }

            XmlElement noItem = xml.CreateElement("item");

            XmlElement noNome = xml.CreateElement("nome");
            noNome.InnerText = txtNomeVendedor.Text.Trim();
            noItem.AppendChild(noNome);

            XmlElement noEmail = xml.CreateElement("email");
            noEmail.InnerText = txtEmail.Text.Trim();
            noItem.AppendChild(noEmail);

            XmlElement noEndereco = xml.CreateElement("endereco");
            noEndereco.InnerText = txtEmail.Text.Trim();
            noItem.AppendChild(noEndereco);

            XmlElement noTelefone = xml.CreateElement("telefone");
            noTelefone.InnerText = txtTelefone.Text.Trim();
            noItem.AppendChild(noTelefone);

            XmlElement noStatus = xml.CreateElement("status");
            noStatus.InnerText = chkStatus.Checked.ToString();
            noItem.AppendChild(noStatus);

            XmlElement noSenha = xml.CreateElement("senha");
            noSenha.InnerText = Util.CriarSenha(txtEmail.Text);
            noItem.AppendChild(noSenha);

            XmlNode xmlPai = xml.SelectSingleNode("vendedor");
            xmlPai.AppendChild(noItem);

            xml.Save(Util.RetornarCaminhoArquivo(Util.TelaCarregaOff.Vendedor));

            Util.MostarMensagem(Util.TipoMensagem.Sucesso);
        }

        #endregion


    }
}
