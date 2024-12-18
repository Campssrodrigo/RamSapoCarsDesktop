﻿using RamSapoCarsDesktop.Comum;
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
    public partial class frmModelos : Form
    {
        public frmModelos()
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
            LimparCampos();
            Util.configurarBotoesTela(Util.EstadoTela.Nova, btnCadastrar, btnAlterar, btnExcluir);
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {

        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {

        }

        private void frmModelos_Load(object sender, EventArgs e)
        {
            Util.configurarBotoesTela(Util.EstadoTela.Nova, btnCadastrar, btnAlterar, btnExcluir);
        }

        private void grdModelos_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        #endregion

        #region Métodos

        private void LimparCampos()
        {
            txtNomeModelo.Clear();
            cbMarca.SelectedIndex = -1;
            txtNomeModelo.Focus();
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
        #endregion
    }
}
