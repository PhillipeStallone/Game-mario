using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace MarioLike.DAL
{
    class LeitorConfiguracao
    {
        public string LerConexao()
        {
            string resultado = "";

            //Ler a string de conexão

            resultado = ConfigurationManager.ConnectionStrings
                ["MarioLikeGame.Properties.Settings.Conexao"].ConnectionString;
            return resultado;
        }
    }
}
