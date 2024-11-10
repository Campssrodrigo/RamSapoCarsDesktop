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
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void coresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmCores().ShowDialog();
        }

        private void marcasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmMarcas().ShowDialog();
        }

        private void modelosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmModelos().ShowDialog();
        }

        private void vendedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmVendedores().ShowDialog();
        }

        private void veículosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmVeiculos().ShowDialog();

        }

        private void vendedoresToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new frmConsultarVendasVendedores().ShowDialog();
        }

        private void marcaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmConsultarVendasMarcaModelo().ShowDialog();
        }

        private void subirOnlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmSubirBanco().ShowDialog();
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
