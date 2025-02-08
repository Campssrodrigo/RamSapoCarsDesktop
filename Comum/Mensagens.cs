using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamSapoCarsDesktop.Comum
{
    public static class Mensagens
    {
        //const e static são quase a mesma coisa, a diferença é que o const não deixar outros valores serem atribuidos
        public const string MSG_OBRIGATORIA = "Revise o(s) campo(s):";
        public const string MSG_SUCESSO = "Ação realizada com sucesso";
        public const string MSG_ERRO = "Ocorreu um erro na operação";
        public const string MSG_CONFIRMACAO = "Deseja excluir o registro?";
        public const string MSG_EMAIL_DUPLICADO = "E-mail já existente!";
        public const string MSG_EMAIL_INVALIDO = "E-mail inválido!";
        public const string MSG_NAO_ENCONTRADO = "Registro não encontrado!";
        public const string MSG_USUARIO_NAO_ENCONTRADO = "Usuário não encontrado!";
        public const string MSG_ACAO_NAO_AUTORIZADA = "Não é possivel fazer o cadastro com informações de dois seguimentos diferentes";
       
       
    }
}
