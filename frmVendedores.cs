using DAO;
using DAO.Interfaces;
using RamSapoCarsDesktop.Comum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
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

        int idVendedor = 0;
        #region Eventos
        private void btnCadastrar_Click(object sender, EventArgs e)
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

        }

        private void frmVendedores_Load(object sender, EventArgs e)
        {
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
                txtEmail.Text = objVendedorClicado.email_vendedor;
                txtEndereco.Text = objVendedorClicado.endereco_vendedor;
                txtTelefone.Text = objVendedorClicado.tel_vendedor;
                chkStatus.Checked = objVendedorClicado.status_vendedor;

                idVendedor = objVendedorClicado.id_vendedor;

                Util.configurarBotoesTela(Util.EstadoTela.Edicao, btnCadastrar, btnAlterar, btnExcluir);
            }
        }
        private void txtNomePesquisa_TextChanged(object sender, EventArgs e)
        {

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

        #endregion

        #region Métodos privados 

        private bool VerificarEmailExistente() => new VendedorDAO().VerificarEmailExistente(txtEmail.Text);

        private void Cadastrar()
        {
            new VendedorDAO().CadastrarVendedor(new tb_vendedor
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
            new VendedorDAO().AlterarVendedor(new tb_vendedor
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
            new VendedorDAO().ExcluirVendedor(idVendedor);
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
