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
        public const int CodigoGaragemLogada = 1;

        public enum EstadoTela
        {
            Nova,
            Edicao
        }
        public enum TipoMensagem
        {
            Erro,
            Sucesso,
            Obrigatorio,
            Confirmacao,
            EmailDuplicado,
            EmailIncorreto
        }

        public static string CriarSenha(string palavra) 
            => palavra.Split('@')[0];
        
        public static string CriarSenha1(string palavra)
        {
            string senha = palavra.Split('@')[0];
            return senha;
        }

        public static void ConfigurarGrid(DataGridView grd)
        {
            grd.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grd.ReadOnly = true;
            grd.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grd.MultiSelect = false;
            grd.AllowUserToAddRows = false;
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

                    btnCadastrar.BackColor = Color.Gray;
                    btnAlterar.BackColor = Color.Blue;
                    btnExcluir.BackColor = Color.Red;
                    break;
            }
        }

        public static bool MostarMensagem(TipoMensagem tipo, string campos = "")
        {
            bool flag = true;
            switch (tipo)
            {
                case TipoMensagem.Erro:
                    MessageBox.Show(Mensagens.MSG_ERRO, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case TipoMensagem.Sucesso:
                    MessageBox.Show(Mensagens.MSG_SUCESSO, "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;

                case TipoMensagem.Obrigatorio:
                    MessageBox.Show(Mensagens.MSG_OBRIGATORIA + campos, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                case TipoMensagem.Confirmacao:
                    flag = MessageBox.Show(Mensagens.MSG_CONFIRMACAO + campos, "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        == DialogResult.Yes;
                    break;
                case TipoMensagem.EmailDuplicado:
                    MessageBox.Show(Mensagens.MSG_EMAIL_DUPLICADO , "Atenção" , MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                case TipoMensagem.EmailIncorreto:
                    MessageBox.Show(Mensagens.MSG_EMAIL_INVALIDO, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
            }

            return flag;
        }

        public static bool VerficarEmail(string email)
        {
            string strModelo = "^([0-9a-zA-Z]([-.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
            if (System.Text.RegularExpressions.Regex.IsMatch(email, strModelo))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
