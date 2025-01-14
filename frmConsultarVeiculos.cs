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
    public partial class frmConsultarVeiculos : Form
    {
        public frmConsultarVeiculos()
        {
            InitializeComponent();
        }
        #region Eventos
        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (validarCampos())
            {

            }
        }

        private void frmConsultarVeiculos_Load(object sender, EventArgs e)
        {

        }
        private void grdVeiculos_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void cbMarca_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        #endregion

        #region Métodos privados
        private bool validarCampos()
        {
            bool flag = true;
            string campos = string.Empty;

            if(cbMarca.SelectedIndex == -1 && cbModelo.SelectedIndex == -1)
            {
                flag = false;
                campos = "\n -Ao menos um dos filtros devem estar selecionados.";
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
