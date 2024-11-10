using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RamSapoCarsDesktop.Comum
{
    public static class Util
    {
        public enum EstadoTela
        {
            Nova,
            Edicao
        }
        public enum TipoMensagem
        {
            Erro,
            Sucesso,
            Obrigatorio
        }

        public static void configurarBotoesTela(EstadoTela estado, Button btnCadastrar, Button btnAlterar, Button btnExcluir)
        {
            switch (estado)
            {
                case EstadoTela.Nova:
                    btnCadastrar.Enabled = true;
                    btnAlterar.Enabled = false;
                    btnExcluir.Enabled = false;

                    btnCadastrar.BackColor = Color.DarkGreen;
                    btnAlterar.BackColor = Color.Gray;
                    btnExcluir.BackColor = Color.Gray;
                    break;

                case EstadoTela.Edicao:
                    btnCadastrar.Enabled = false;
                    btnExcluir.Enabled = true;
                    btnAlterar.Enabled = true;

                    btnCadastrar.BackColor = Color.DarkGreen;
                    btnAlterar.BackColor = Color.Blue;
                    btnExcluir.BackColor = Color.Red;
                    break;
            }
        }

        public static void MostarMensagem(TipoMensagem tipo, string campos = "")
        {
            switch (tipo)
            {
                case TipoMensagem.Erro:

                    break;

                case TipoMensagem.Sucesso:

                    break;

                case TipoMensagem.Obrigatorio:
                    MessageBox.Show(Mensagens.MSG_OBRIGATORIA + campos, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
            }
        }

    }
}
