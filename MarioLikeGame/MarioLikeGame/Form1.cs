using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MarioLike.DAL;
using MarioLikeGame.Model;

namespace MarioLikeGame
{

    public partial class frmTelaJogo : Form
    {
        //declarando a DAL
        GamerDAL gamerDAL;

        //criar um atributo para pegar o nome do jogador 
        public string nomeGamer { get; set; }

       
        
        //Atributos para controle da movimentação do personagem
        private bool paraCima;
        private bool paraBaixo;
        private bool paraDireita;
        private bool paraEsquerda;

        //Variável para condições de vitoria/derrota
        private bool vitoria = false;

        //Variável para pontuação
        private int pontos = 0;

        //Variáveis para controlar o cronômetro do jogo
        int segundos = 0;
        int minutos = 0;

        //Atributo responsável pela velocidade de locomoção do personagem
        private int velocidade = 10;

        //biblioteca do windows media player
        //WMPLib.WindowsMediaPlayer Tocador = new WMPLib.WindowsMediaPlayer();
        List<System.Windows.Media.MediaPlayer> sounds = new List<System.Windows.Media.MediaPlayer>();

        public frmTelaJogo()
        {
            InitializeComponent();
            ptxGameOver.Visible = false;
            ptxYouWin.Visible = false;
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void frmTelaJogo_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void frmTelaJogo_Load(object sender, EventArgs e)
        {
            //Audio("01 overworld bgm.mp3", "Play");
            playsound("01 overworld bgm.mp3");
        }

        //Movimentar o personagem quando pressiono a tecla

        private void KeyisDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                paraEsquerda = true;
            }

            if (e.KeyCode == Keys.Right)
            {
                paraDireita = true;
            }

            if (e.KeyCode == Keys.Up)
            {
                paraCima = true;
            }

            if (e.KeyCode == Keys.Down)
            {
                paraBaixo = true;
            }
        }

        //Parar o movimento do personagem quando soltar a tecla
        private void KeyisUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    paraEsquerda = false;
                    break;
                case Keys.Right:
                    paraDireita = false;
                    break;
                case Keys.Up:
                    paraCima = false;
                    break;
                case Keys.Down:
                    paraBaixo = false;
                    break;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           
            lblPontos.Text = "Pontos: " + pontos;
            if (paraEsquerda)
            {
                //Movimenta o personagem para esquerda
                personagem.Left -= velocidade;
            }

            if (paraDireita)
            {
                //Movimenta o personagem para Direita
                personagem.Left += velocidade;
            }

            //Movimenta o personagem para cinma
            if (paraCima)
            {
                personagem.Top -= velocidade;
            }

            //Movimenta o personagem para baixo
            if (paraBaixo)
            {
                personagem.Top += velocidade;
            }

            //Posicionamento do personagem dentro da área do formulário (tela)
            if (personagem.Left < 0)
            {
                personagem.Left = 0;
            }

            if (personagem.Left > 1810)
            {
                personagem.Left = 1810;
            }

            if (personagem.Top < 70)
            {
                personagem.Top = 70;
            }

            if (personagem.Top > 900)
            {
                personagem.Top = 900;
            }

            //Loop para checar todos os controles inseridos no form
            foreach (Control item in this.Controls)
            {
                //Verifica se o jogador colidiu com o inimigo, caso positivo GameOver
                if (item is PictureBox && (string)item.Tag == "inimigo")
                {
                    //Checa colisão com as PictureBox
                    if (((PictureBox)item).Bounds.IntersectsWith(personagem.Bounds))
                    {
                        vitoria = false;
                        stopsound();
                        playsound("smb_gameover.wav");
                        GameOver(vitoria);
                        RemovePictureBox();
                    }           
                }

                //Verifica se o jogador colidiu com o item, caso positivo o destrua
                if (item is PictureBox && (string)item.Tag == "coletaveis" )
                {
                        //Checa colisão com as PictureBox
                        if (((PictureBox)item).Bounds.IntersectsWith(personagem.Bounds))
                    {
                        //toca a música
                        //Audio("smb_coin.wav", "Play");
                        playsound("smb_coin.wav");
                        //Remove o item coletável
                        this.Controls.Remove(item);

                        //Incrementar a variável pontos
                        pontos++;

                        //Condição de Vitória
                        if (pontos == 15)
                        {
                            vitoria = true;
                            stopsound();
                            playsound("smb_stage_clear.wav");
                            GameOver(vitoria);
                            RemovePictureBox();
                        }
                    }
                }
                if (item is PictureBox && (string)item.Tag == "coletaveisaumentocc" || (string)item.Tag == "coletaveisaumentoss")
                {
                    //Checa colisão com as PictureBox
                    if (((PictureBox)item).Bounds.IntersectsWith(personagem.Bounds))
                    {
                        //toca a música
                        //Audio("smb_coin.wav", "Play");
                        playsound("smb_powerup.wav");
                        //Remove o item coletável
                        this.Controls.Remove(item);

                        //Incrementar a variável pontos
                        pontos++;

                        //Condição de Vitória
                        if (pontos == 15)
                        {
                            vitoria = true;
                            stopsound();
                            playsound("smb_stage_clear.wav");
                            GameOver(vitoria);
                            RemovePictureBox();
                        }
                        if ((string)item.Tag == "coletaveisaumentocc")
                        {
                            personagem.Height += 10;
                            personagem.Width += 10;
                        }
                        if ((string)item.Tag == "coletaveisaumentoss")
                        {
                            velocidade += 5;
                        }
                    }

                }
            }
        }

        public void GravaScore()
        {
            //instancia a DAL
            gamerDAL = new GamerDAL();

            Placar placar = new Placar();

            var frm = new frmTelaInicial();

            if (!this.nomeGamer.Equals(""))
            {
                placar.NomeJogador = this.nomeGamer;
            }
            else
            {
                placar.NomeJogador = "Bot1";
            }

            placar.ScoreJogador = pontos;
            placar.DataScore_Jogador = DateTime.Now;
            placar.TempoJogador = lblTempo.Text;

            //chama a função Inserir da DAL passando o bijeto populado como parâmetro
            if (!gamerDAL.Inserir(placar))
            {
                //deu pau D: exibe mensagem para o usuário
                MessageBox.Show("Erro ao inserir os dados: \n\n\n\n" +
                    gamerDAL.mensagemErro, "mario Like Game");
            }
        }

        private void RemovePictureBox()
        {
            foreach (Control item in this.Controls)
            {
                if (item is PictureBox && (string)item.Tag != "gameOver")
                {
                    ((PictureBox)item).Image = null;
                }
            }
        }

        private void GameOver(bool ganhou)
        {
            lblPontos.Text = "Pontos: " + pontos;
            personagem.Visible = false;
            btnRestart.Visible = true;
            btnRestart.Focus();

            GravaScore();

            if (ganhou)
            {
                ptxYouWin.Visible = true;
            }
            else
            {
                ptxGameOver.Visible = true;
                
            }
            timer1.Stop();
            timer2.Stop();
        }

        private void lblPontos_Click(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            segundos++;
            if (segundos == 60)
            {
                minutos++;
                //segundos = 0;
            }
            segundos = segundos % 60;
            lblTempo.Text = "Tempo: " + minutos.ToString("00:") + segundos.ToString("00");
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            //Application.Restart();
            stopsound();
            this.Close();
        }
        //private void Audio(string caminho, string estadoMP)
        //{
        //    Tocador.MediaError += new WMPLib._WMPOCXEvents_MediaErrorEventHandler(Tocador_MediaError);

        //    Tocador.URL = caminho;
        //    if (estadoMP.Equals("Play"))
        //    {
        //        Tocador.controls.play();
        //    }
        //    else if (estadoMP.Equals("Stop"))
        //    {
        //        Tocador.controls.stop();
        //    }
        //}

        //private void Tocador_MediaError(object pMediaObject)
        //{
        //    MessageBox.Show("Não é possível executar o arquivo de mídia");
        //    this.Close();
        //}

        private void playsound(string nome)
        {
            string url = Application.StartupPath + @"\" + nome;
            var sound = new System.Windows.Media.MediaPlayer();
            sound.Open(new Uri(url));
            sound.Play();
            sounds.Add(sound);
        }

        private void stopsound()
        {
            for (int i = sounds.Count -1; i >=0 ; i--)
            {
                sounds[i].Stop();
                sounds.RemoveAt(i);
            }
        }

        private void pictureBox23_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void ptxYouWin_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox31_Click(object sender, EventArgs e)
        {

        }
    }
}
