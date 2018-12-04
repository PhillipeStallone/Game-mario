using System;
using System.Collections.Generic;
using System.Text;

namespace MarioLikeGame.Model
{
    public class Placar
    {
        private int id_Jogador;
        private string nome_Jogador;
        private int score_Jogador;
        private DateTime data_Score_Jogador;
        private string tempo_Jogador;

        public Placar()
        {

        }

        public Placar(int id_Jogador, string nome_Jogador, int score_Jogador, DateTime data_Score_Jogador, string tempo_Jogador)
        {
            this.Id_Jogador = id_Jogador;
            this.NomeJogador = nome_Jogador;
            this.ScoreJogador = score_Jogador;
            this.DataScore_Jogador = data_Score_Jogador;
            this.TempoJogador = tempo_Jogador;
        }

        //getters and setters 

        public int Id_Jogador { get => id_Jogador; set => id_Jogador = value; }
        public string NomeJogador { get => nome_Jogador; set => nome_Jogador = value; }
        public int ScoreJogador { get => score_Jogador; set => score_Jogador = value; }
        public DateTime DataScore_Jogador { get => data_Score_Jogador; set => data_Score_Jogador = value; }
       public string TempoJogador { get => tempo_Jogador; set => tempo_Jogador = value; }
    }
}
