using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using MarioLikeGame.Model;

namespace MarioLike.DAL
{
    public class GamerDAL
    {
        //declarar o objeto de conexão com o BD
        private SqlConnection conexao;

            //exibir as mensagems de erro
            public string mensagemErro { get; set; }

        public GamerDAL()
        {
            //criar o objeto para ler a confguração
            LeitorConfiguracao leitor = new LeitorConfiguracao();

            //instanciar a conexão
            conexao = new SqlConnection();
            conexao.ConnectionString = leitor.LerConexao();
        }
        public bool Inserir(Placar placar)
        {
            bool resultado = false;
            //limpa mensagem de erro
            mensagemErro = "";

            //declarar comando SQL
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexao;
            comando.CommandText = "INSERT INTO jogador (Nome_Jogador,Score_Jogador,Data_Score_jogador,Tempo_Jogador) VALUES (@Nome,@Score,@Data,@Tempo)";

            //criar os parâmetros 
            comando.Parameters.AddWithValue("@Nome",placar.NomeJogador);
            comando.Parameters.AddWithValue("@Score",placar.ScoreJogador);
            comando.Parameters.AddWithValue("@Data",placar.DataScore_Jogador);
            comando.Parameters.AddWithValue("@Tempo", placar.TempoJogador);

            //executar os comandos 
            try
            {
                //abrir a conxão 
                conexao.Open();
                //executar o comando
                comando.ExecuteNonQuery();
                //se chegou ate aqui,então funcionou :D
                resultado = true;
            }
            catch (Exception ex)
            {
                //se entrou aqui, deu pau D:
                mensagemErro = ex.Message;
            }
            finally
            {
                //finalizar fechando a conexão
                conexao.Close();
            }
            return resultado;
        }

        public List<Placar> Listar()
        {
            //instanciar a lista
            List<Placar> resultado = new List<Placar>();

            //declarar o comando
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexao;
            comando.CommandText = "SELECT TOP 10 Id_Jogador, Nome_Jogador, Score_Jogador, Data_Score_Jogador, TEMPO_JOGADOR " +
                " FROM jogador ORDER BY Score_Jogador DESC, TEMPO_JOGADOR, Data_Score_Jogador";

            //executar o comando
            try
            {
                //abrir a conexão
                conexao.Open();

                //executar o comando e receber o resultado
                SqlDataReader leitor = comando.ExecuteReader();

                //verificar se encontrou algo
                while (leitor.Read() == true)
                {
                    //instanciar o objeto
                    Placar placar = new Placar();
                    placar.Id_Jogador = Convert.ToInt32(leitor["Id_Jogador"]);
                    placar.NomeJogador = leitor["Nome_Jogador"].ToString();
                    placar.ScoreJogador = Convert.ToInt32(leitor["Score_Jogador"]);
                    placar.DataScore_Jogador = Convert.ToDateTime(leitor["Data_Score_Jogador"]);
                    placar.TempoJogador = leitor["TEMPO_JOGADOR"].ToString();

                    //adicionar na lista 
                    resultado.Add(placar);
                }

                //fechar leitor 
                leitor.Close();
            }
            catch (Exception ex)
            {
                string mensagem = ex.Message;

            }
            finally
            {
                //finalizar fechando a conexão
                conexao.Close();
            }
            return resultado;
        }
    }
}
