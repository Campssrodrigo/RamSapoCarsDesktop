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
    public partial class frmVeiculos : Form
    {
        public frmVeiculos()
        {
            InitializeComponent();
        }
        int idVeiculo = 0;
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
                        HabilitarModelo();
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
                    HabilitarModelo();
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
            if (Util.MostarMensagem(Util.TipoMensagem.Confirmacao))
            {
                try
                {
                    Excluir();
                    limparCampos();
                    Util.configurarBotoesTela(Util.EstadoTela.Nova, btnCadastrar, btnAlterar, btnExcluir);
                }
                catch
                {
                    Util.MostarMensagem(Util.TipoMensagem.Erro);
                }

            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            new frmConsultarVeiculos(this).ShowDialog();
        }
        private void cbMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            HabilitarModelo();
        }

        private void frmVeiculos_Load(object sender, EventArgs e)
        {
            Util.configurarBotoesTela(Util.EstadoTela.Nova, btnCadastrar, btnAlterar, btnExcluir);
            Util.ConfigurarCombo(cbCor, "nome_cor", "id_cor");
            Util.ConfigurarCombo(cbMarca, "nome_marca", "id_marca");
            Util.ConfigurarCombo(cbModelo, "nome_modelo", "id_modelo");
            CarregarCor();
            CarregarMarca();
            limparCampos();

        }

        #endregion

        #region Métodos privados
        private tb_veiculo RetornarObjVeiculo() => new tb_veiculo
        {
            id_veiculo = idVeiculo,
            id_garagem = Util.CodigoGaragemLogada,
            id_modelo = (int)cbMarca.SelectedValue,
            id_cor = (int)cbCor.SelectedValue,
            placa_veiculo = txtPlaca.Text.Trim(),
            motor_veiculo = txtMotor.Text.Trim(),
            direcao_veiculo = (short)cbDirecao.SelectedIndex,
            cambio_veiculo = (short)cbCambio.SelectedIndex,
            combustivel_veiculo = (short)cbCombustivel.SelectedIndex,
            km_veiculo = txtKm.Text.Trim(),
            anomodelo_veiculo = txtAnoModelo.Text,
            valorcompra_veiculo = Convert.ToDecimal(txtValorCompra.Text),
            valorvenda_veiculo = Convert.ToDecimal(txtValorVenda.Text),
            situacao_veiculo = (short)cbSituacao.SelectedIndex,
            obs_veiculo = txtObs.Text == string.Empty ? null : txtObs.Text.Trim()

        };
        private void Cadastrar()
        {
            new VeiculoDAO().Cadastrar(RetornarObjVeiculo());
            Util.MostarMensagem(Util.TipoMensagem.Sucesso);
        }
        private void Alterar()
        {
            new VeiculoDAO().Alterar(RetornarObjVeiculo());
            Util.MostarMensagem(Util.TipoMensagem.Sucesso);
        }
        private void Excluir()
        {
            new VeiculoDAO().Excluir(idVeiculo);
            Util.MostarMensagem(Util.TipoMensagem.Sucesso);
        }
        private bool validarCampos()
        {
            bool flag = true;
            string campos = string.Empty;

            if (cbMarca.SelectedIndex == -1)
            {
                flag = false;
                campos = "\n -Marca";
            }
            if (cbCor.SelectedIndex == -1)
            {
                flag = false;
                campos += "\n -Cor";
            }
            if (txtMotor.Text.Trim() == string.Empty)
            {
                flag = false;
                campos += "\n -Motor";
            }
            if (cbCambio.SelectedIndex == -1)
            {
                flag = false;
                campos += "\n -Cambio";
            }
            if (txtKm.Text.Trim() == string.Empty)
            {
                flag = false;
                campos += "\n -KM";
            }
            if (txtValorCompra.Text.Trim() == string.Empty)
            {
                flag = false;
                campos += "\n -Valor da Compra";
            }
            if (cbSituacao.SelectedIndex == -1)
            {
                flag = false;
                campos += "\n -Situação";
            }
            if (cbModelo.SelectedIndex == -1)
            {
                flag = false;
                campos += "\n -Modelo";
            }
            if (txtPlaca.Text.Trim() == string.Empty)
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
            chkOffLine.Checked = false;
            cbMarca.Focus();
        }

        private void CarregarCor()
        {
            cbCor.DataSource = new CorDAO().ConsultarCor(Util.CodigoGaragemLogada);
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
                CarregarModelo();
            }
            else
                cbModelo.Enabled = false;
        }

        private void GerarArquivoXML()
        {
            XmlDocument xml = new XmlDocument();

            if (!File.Exists(Util.RetornarCaminhoArquivo(Util.TelaCarregaOff.Veiculo)))
            {
                XmlElement noVeiculo = xml.CreateElement("veiculo");
                xml.AppendChild(noVeiculo);
            }
            else
            {
                xml.Load(Util.RetornarCaminhoArquivo(Util.TelaCarregaOff.Veiculo));
            }

            XmlElement noItem = xml.CreateElement("item");

            XmlElement noMarca = xml.CreateElement("marca");// Só para mostrar no carregamento off
            noMarca.InnerText = cbMarca.Text;
            noItem.AppendChild(noMarca);

            XmlElement noIdModelo = xml.CreateElement("idmodelo");
            noIdModelo.InnerText = cbModelo.SelectedValue.ToString();
            noItem.AppendChild(noIdModelo);

            XmlElement noModelo = xml.CreateElement("modelo");
            noModelo.InnerText = cbModelo.Text;
            noItem.AppendChild(noModelo);

            XmlElement noCor = xml.CreateElement("cor");
            noCor.InnerText = cbCor.Text.Trim();
            noItem.AppendChild(noCor);

            XmlElement noIdCor = xml.CreateElement("idcor");
            noIdCor.InnerText = cbCor.SelectedValue.ToString();
            noItem.AppendChild(noIdCor);

            XmlElement noMotor = xml.CreateElement("motor");
            noMotor.InnerText = txtMotor.Text.Trim();
            noItem.AppendChild(noMotor);

            XmlElement noCambio = xml.CreateElement("cambio");
            noCambio.InnerText = cbCambio.Text;
            noItem.AppendChild(noCambio);

            XmlElement noIndexCambio = xml.CreateElement("indexcambio");
            noIndexCambio.InnerText = cbCambio.SelectedIndex.ToString();
            noItem.AppendChild(noIndexCambio);

            XmlElement noKM = xml.CreateElement("km");
            noKM.InnerText = txtKm.Text;
            noItem.AppendChild(noKM);

            XmlElement noVlCompra = xml.CreateElement("valorcompra");
            noVlCompra.InnerText = txtValorCompra.Text;
            noItem.AppendChild(noVlCompra);

            XmlElement noSituacao = xml.CreateElement("situacao");
            noSituacao.InnerText = cbSituacao.Text;
            noItem.AppendChild(noSituacao);

            XmlElement noIndexSituacao = xml.CreateElement("indexsituacao");
            noIndexSituacao.InnerText = cbSituacao.SelectedIndex.ToString();
            noItem.AppendChild(noIndexSituacao);

            XmlElement noPlaca = xml.CreateElement("placa");
            noPlaca.InnerText = txtPlaca.Text;
            noItem.AppendChild(noPlaca);

            XmlElement noDirecao = xml.CreateElement("direcao");
            noDirecao.InnerText = cbDirecao.Text;
            noItem.AppendChild(noDirecao);

            XmlElement noIndexDirecao = xml.CreateElement("indexdirecao");
            noIndexDirecao.InnerText = cbDirecao.SelectedIndex.ToString();
            noItem.AppendChild(noIndexDirecao);

            XmlElement noCombustivel = xml.CreateElement("combustivel");
            noCombustivel.InnerText = cbCombustivel.Text;
            noItem.AppendChild(noCombustivel);

            XmlElement noIndexCombustivel = xml.CreateElement("indexcombustivel");
            noIndexCombustivel.InnerText = cbCombustivel.SelectedIndex.ToString();
            noItem.AppendChild(noIndexCombustivel);

            XmlElement noAnoModelo = xml.CreateElement("anomodelo");
            noAnoModelo.InnerText = txtAnoModelo.Text;
            noItem.AppendChild(noAnoModelo);

            XmlElement noVlVenda = xml.CreateElement("valorvenda");
            noVlVenda.InnerText = txtValorVenda.Text;
            noItem.AppendChild(noVlVenda);

            XmlElement noObs = xml.CreateElement("obs");
            noObs.InnerText = txtObs.Text.Trim() == string.Empty ? null : txtObs.Text.Trim();
            noItem.AppendChild(noObs);


            XmlNode xmlPai = xml.SelectSingleNode("veiculo");
            xmlPai.AppendChild(noItem);

            xml.Save(Util.RetornarCaminhoArquivo(Util.TelaCarregaOff.Veiculo));

            Util.MostarMensagem(Util.TipoMensagem.Sucesso);
        }

        #endregion

        #region Métodos publicos
        public void CarregarCampos(tb_veiculo objVeiculo)
        {
            idVeiculo = objVeiculo.id_veiculo;
            cbMarca.SelectedValue = objVeiculo.tb_modelo.id_marca;
            cbCor.SelectedValue = objVeiculo.id_cor;
            txtMotor.Text = objVeiculo.motor_veiculo;
            cbCambio.SelectedIndex = objVeiculo.cambio_veiculo;
            txtKm.Text = objVeiculo.km_veiculo;
            txtValorCompra.Text = objVeiculo.valorcompra_veiculo.ToString();
            cbSituacao.SelectedIndex = objVeiculo.situacao_veiculo;
            cbModelo.SelectedValue = objVeiculo.id_modelo;
            txtPlaca.Text = objVeiculo.placa_veiculo;
            cbDirecao.SelectedIndex = objVeiculo.direcao_veiculo;
            cbCombustivel.SelectedIndex = objVeiculo.combustivel_veiculo;
            txtAnoModelo.Text = objVeiculo.anomodelo_veiculo;
            txtValorVenda.Text = objVeiculo.valorvenda_veiculo.ToString();
            txtObs.Text = objVeiculo.obs_veiculo;

            Util.configurarBotoesTela(Util.EstadoTela.Edicao, btnCadastrar, btnAlterar, btnExcluir);
        }
        #endregion


    }
}
