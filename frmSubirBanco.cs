using DAO;
using RamSapoCarsDesktop.Comum;
using RamSapoCarsDesktop.VO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace RamSapoCarsDesktop
{
    public partial class frmSubirBanco : Form
    {
        public frmSubirBanco()
        {
            InitializeComponent();
        }


        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                Cadastrar();
                limparCampos();
            }
            catch
            {
                Util.MostarMensagem(Util.TipoMensagem.AcaoNaoAutorizada);
            }


        }

        private void cbInfomacoes_SelectedIndexChanged(object sender, EventArgs e)
        {

            Util.ConfigurarGrid(grdDadosOff);
            CarregarXml();
        }

        private void CarregarXml()
        {

            if (cbInfomacoes.SelectedIndex != -1)
            {
                XmlDocument xml = new XmlDocument();

                switch (cbInfomacoes.SelectedIndex)
                {
                    case 0:
                        if (File.Exists(Util.RetornarCaminhoArquivo(Util.TelaCarregaOff.Cores)))
                        {
                            DataSet ds = new DataSet();
                            ds.ReadXml(Util.RetornarCaminhoArquivo(Util.TelaCarregaOff.Cores));

                            if (ds != null && ds.Tables.Count > 0)
                            {
                                grdDadosOff.DataSource = ds.Tables[0];
                            }
                        }
                        break;
                    case 1:
                        if (File.Exists(Util.RetornarCaminhoArquivo(Util.TelaCarregaOff.Vendedor)))
                        {
                            DataSet ds = new DataSet();
                            ds.ReadXml(Util.RetornarCaminhoArquivo(Util.TelaCarregaOff.Vendedor));

                            if (ds != null && ds.Tables.Count > 0)
                            {
                                grdDadosOff.DataSource = ds.Tables[0];
                            }
                        }
                        break;
                    case 2:
                        if (File.Exists(Util.RetornarCaminhoArquivo(Util.TelaCarregaOff.Marca)))
                        {
                            DataSet ds = new DataSet();
                            ds.ReadXml(Util.RetornarCaminhoArquivo(Util.TelaCarregaOff.Marca));

                            if (ds != null && ds.Tables.Count > 0)
                            {
                                grdDadosOff.DataSource = ds.Tables[0];
                            }
                        }
                        break;
                    case 3:
                        if (File.Exists(Util.RetornarCaminhoArquivo(Util.TelaCarregaOff.Modelo)))
                        {
                            DataSet ds = new DataSet();
                            ds.ReadXml(Util.RetornarCaminhoArquivo(Util.TelaCarregaOff.Modelo));

                            if (ds != null && ds.Tables.Count > 0)
                            {
                                grdDadosOff.DataSource = ds.Tables[0];
                                grdDadosOff.Columns["idmarca"].Visible = false;
                            }
                        }
                        break;

                    case 4:
                        if (File.Exists(Util.RetornarCaminhoArquivo(Util.TelaCarregaOff.Veiculo)))
                        {
                            DataSet ds = new DataSet();
                            ds.ReadXml(Util.RetornarCaminhoArquivo(Util.TelaCarregaOff.Veiculo));

                            if (ds != null && ds.Tables.Count > 0)
                            {
                                grdDadosOff.DataSource = ds.Tables[0];
                                grdDadosOff.Columns["idmarca"].Visible = false;
                                grdDadosOff.Columns["idcor"].Visible = false;
                                grdDadosOff.Columns["idmodelo"].Visible = false;
                                grdDadosOff.Columns["indexsituacao"].Visible = false;
                                grdDadosOff.Columns["indexdirecao"].Visible = false;
                                grdDadosOff.Columns["indexcombustivel"].Visible = false;
                                grdDadosOff.Columns["indexcambio"].Visible = false;

                            }
                        }
                        break;
                }

            }
        }

        private void Cadastrar()
        {

            if (cbInfomacoes.SelectedIndex != -1)
            {
                switch (cbInfomacoes.SelectedIndex)
                {
                    case 0:
                        //Cria o obj dao para chamar o método para cadastrar
                        CorDAO objDAOCores = new CorDAO();

                        foreach (DataGridViewRow row in grdDadosOff.Rows)
                        {
                            if (!row.IsNewRow)
                            {
                                //Cria o obj para preeencher (POPULAR) as informações para inserir
                                tb_cor objCor = new tb_cor();

                                //Popula as propriedades do obj
                                objCor.nome_cor = row.Cells["nome"].Value.ToString();
                                objCor.id_garagem = Util.CodigoGaragemLogada;


                                objDAOCores.CadastrarCor(objCor);
                                ExcluirItemXML(row.Cells["nome"].Value.ToString());

                            }
                        }
                        Util.MostarMensagem(Util.TipoMensagem.Sucesso);
                        break;

                    case 1:
                        //Cria o obj dao para chamar o método para cadastrar
                        VendedorDAO objDAOVendedor = new VendedorDAO();

                        foreach (DataGridViewRow row in grdDadosOff.Rows)
                        {
                            if (!row.IsNewRow)
                            {
                                //Cria o obj para preeencher (POPULAR) as informações para inserir
                                tb_vendedor objVendedor = new tb_vendedor();

                                //Popula as propriedades do obj
                                objVendedor.nome_vendedor = row.Cells["nome"].Value.ToString();
                                objVendedor.email_vendedor = row.Cells["email"].Value.ToString();
                                objVendedor.endereco_vendedor = row.Cells["endereco"].Value.ToString();
                                objVendedor.tel_vendedor = row.Cells["telefone"].Value.ToString();
                                objVendedor.status_vendedor = row.Cells["status"].Value.Equals("False") ? false : true;
                                objVendedor.senha_vendedor = row.Cells["senha"].Value.ToString();
                                objVendedor.id_garagem = Util.CodigoGaragemLogada;


                                objDAOVendedor.Cadastrar(objVendedor);
                                ExcluirItemXML(row.Cells["nome"].Value.ToString());
                            }
                        }
                        Util.MostarMensagem(Util.TipoMensagem.Sucesso);
                        break;
                    case 2:
                        //Cria o obj dao para chamar o método para cadastrar
                        MarcaDAO objDAOMarca = new MarcaDAO();

                        foreach (DataGridViewRow row in grdDadosOff.Rows)
                        {
                            if (!row.IsNewRow)
                            {
                                //Cria o obj para preeencher (POPULAR) as informações para inserir
                                tb_marca objMarca = new tb_marca();

                                //Popula as propriedades do obj
                                objMarca.nome_marca = row.Cells["nome"].Value.ToString();
                                objMarca.id_garagem = Util.CodigoGaragemLogada;


                                objDAOMarca.CadastrarMarca(objMarca);
                                ExcluirItemXML(row.Cells["nome"].Value.ToString());

                            }
                        }
                        Util.MostarMensagem(Util.TipoMensagem.Sucesso);
                        break;
                    case 3:
                        //Cria o obj dao para chamar o método para cadastrar
                        ModeloDAO objDAOModelo = new ModeloDAO();

                        foreach (DataGridViewRow row in grdDadosOff.Rows)
                        {
                            if (!row.IsNewRow)
                            {
                                //Cria o obj para preeencher (POPULAR) as informações para inserir
                                tb_modelo objModelo = new tb_modelo();

                                //Popula as propriedades do obj
                                objModelo.nome_modelo = row.Cells["nome"].Value.ToString();
                                objModelo.id_marca = Convert.ToInt32(row.Cells["idmarca"].Value.ToString());
                                objModelo.id_garagem = Util.CodigoGaragemLogada;


                                objDAOModelo.Cadastrar(objModelo);
                                ExcluirItemXML(row.Cells["nome"].Value.ToString());

                            }
                        }
                        Util.MostarMensagem(Util.TipoMensagem.Sucesso);
                        break;
                    case 4:
                        //Cria o obj dao para chamar o método para cadastrar
                        VeiculoDAO objDAOVeiculo = new VeiculoDAO();

                        foreach (DataGridViewRow row in grdDadosOff.Rows)
                        {
                            if (!row.IsNewRow)
                            {
                                //Cria o obj para preeencher (POPULAR) as informações para inserir
                                tb_veiculo objVeiculo = new tb_veiculo();
                                
                                

                                //Popula as propriedades do obj
                                objVeiculo.id_modelo = Convert.ToInt32(row.Cells["idmodelo"].Value.ToString());
                                objVeiculo.id_cor = Convert.ToInt32(row.Cells["idcor"].Value.ToString());
                                objVeiculo.placa_veiculo = row.Cells["placa"].Value.ToString();
                                objVeiculo.motor_veiculo = row.Cells["motor"].Value.ToString();
                                objVeiculo.km_veiculo = row.Cells["km"].Value.ToString();
                                objVeiculo.anomodelo_veiculo = row.Cells["anomodelo"].Value.ToString();
                                objVeiculo.obs_veiculo = row.Cells["obs"].Value.ToString();
                                objVeiculo.valorcompra_veiculo = Convert.ToDecimal(row.Cells["valorcompra"].Value);
                                objVeiculo.valorvenda_veiculo = Convert.ToDecimal(row.Cells["valorvenda"].Value);
                                objVeiculo.cambio_veiculo = Convert.ToByte(row.Cells["indexcambio"].Value);
                                objVeiculo.situacao_veiculo = Convert.ToByte(row.Cells["indexsituacao"].Value);
                                objVeiculo.direcao_veiculo = Convert.ToByte(row.Cells["indexdirecao"].Value);
                                objVeiculo.combustivel_veiculo = Convert.ToByte(row.Cells["indexcombustivel"].Value);

                                
                                objVeiculo.id_garagem = Util.CodigoGaragemLogada;


                                objDAOVeiculo.Cadastrar(objVeiculo);
                                ExcluirItemXML(row.Cells["idmodelo"].Value.ToString());

                            }
                        }
                        Util.MostarMensagem(Util.TipoMensagem.Sucesso);
                        break;
                }
            }
        }
        private void ExcluirItemXML(string nomeFiltro)
        {
            XmlDocument xml = new XmlDocument();

            switch (cbInfomacoes.SelectedIndex)
            {
                case 0:
                    if (File.Exists(Util.RetornarCaminhoArquivo(Util.TelaCarregaOff.Cores)))
                    {
                        xml.Load(Util.RetornarCaminhoArquivo(Util.TelaCarregaOff.Cores));

                        //Criar
                        XmlNode noEncontrado = xml.SelectSingleNode($"//item[nome='{nomeFiltro}']");

                        if (noEncontrado != null)
                        {
                            noEncontrado.ParentNode.RemoveChild(noEncontrado);
                            xml.Save(Util.RetornarCaminhoArquivo(Util.TelaCarregaOff.Cores));
                        }
                    }
                    break;

                case 1:
                    if (File.Exists(Util.RetornarCaminhoArquivo(Util.TelaCarregaOff.Vendedor)))
                    {
                        xml.Load(Util.RetornarCaminhoArquivo(Util.TelaCarregaOff.Vendedor));

                        //Criar
                        XmlNode noEncontrado = xml.SelectSingleNode($"//item[nome='{nomeFiltro}']");

                        if (noEncontrado != null)
                        {
                            noEncontrado.ParentNode.RemoveChild(noEncontrado);
                            xml.Save(Util.RetornarCaminhoArquivo(Util.TelaCarregaOff.Vendedor));
                        }
                    }
                    break;

                case 2:
                    if (File.Exists(Util.RetornarCaminhoArquivo(Util.TelaCarregaOff.Marca)))
                    {
                        xml.Load(Util.RetornarCaminhoArquivo(Util.TelaCarregaOff.Marca));

                        //Criar
                        XmlNode noEncontrado = xml.SelectSingleNode($"//item[nome='{nomeFiltro}']");

                        if (noEncontrado != null)
                        {
                            noEncontrado.ParentNode.RemoveChild(noEncontrado);
                            xml.Save(Util.RetornarCaminhoArquivo(Util.TelaCarregaOff.Marca));
                        }
                    }
                    break;
                case 3:
                    if (File.Exists(Util.RetornarCaminhoArquivo(Util.TelaCarregaOff.Modelo)))
                    {
                        xml.Load(Util.RetornarCaminhoArquivo(Util.TelaCarregaOff.Modelo));

                        //Criar
                        XmlNode noEncontrado = xml.SelectSingleNode($"//item[nome='{nomeFiltro}']");

                        if (noEncontrado != null)
                        {
                            noEncontrado.ParentNode.RemoveChild(noEncontrado);
                            xml.Save(Util.RetornarCaminhoArquivo(Util.TelaCarregaOff.Modelo));
                        }
                    }
                    break;
                case 4:
                    if (File.Exists(Util.RetornarCaminhoArquivo(Util.TelaCarregaOff.Veiculo)))
                    {
                        xml.Load(Util.RetornarCaminhoArquivo(Util.TelaCarregaOff.Veiculo));

                        //Criar
                        XmlNode noEncontrado = xml.SelectSingleNode($"//item[idmodelo='{nomeFiltro}']");

                        if (noEncontrado != null)
                        {
                            noEncontrado.ParentNode.RemoveChild(noEncontrado);
                            xml.Save(Util.RetornarCaminhoArquivo(Util.TelaCarregaOff.Veiculo));
                        }
                    }
                    break;
            }

        }

        private void limparCampos()
        {
            cbInfomacoes.SelectedIndex = -1;
            grdDadosOff.DataSource = null;
        }

    }
}
