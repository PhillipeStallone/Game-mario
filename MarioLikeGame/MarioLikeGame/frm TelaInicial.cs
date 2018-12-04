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

namespace MarioLikeGame
{
    public partial class frmTelaInicial : Form
    {
        public frmTelaInicial()
        {
            InitializeComponent();
            txtNome.MaxLength = 15;
        }

        private void PreencherGrid()
        {
            //declaração a DAL
            GamerDAL gamerDAL;
            //instanciando a dal na construção do formulário
            gamerDAL = new GamerDAL();

            //limpando o Datasource
            dgvListaRecorde.DataSource = null;

            //listando o dal
            dgvListaRecorde.DataSource = gamerDAL.Listar();

            //removendo a colua id_Jogador
            dgvListaRecorde.Columns.Remove("ID_Jogador");

            dgvListaRecorde.CurrentRow.DefaultCellStyle.BackColor = Color.Red;

        }



        private void lblJogador_Click(object sender, EventArgs e)
        {

        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("Por favor insira um nome!","Mario Like");
            }
            else
            {
                //não exibir a instância atual da classe 
                this.Visible = false;
                //criar uma nova instância do frmTelaJogo()
                var frm = new frmTelaJogo();
                frm.nomeGamer = txtNome.Text;
                frm.ShowDialog();
                PreencherGrid();
                this.Visible = true;
            }
           
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmTelaInicial_Load(object sender, EventArgs e)
        {
            //preencher o grid
            PreencherGrid();

            txtNome.Focus();
        }

        private void dgvListaRecorde_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pbMario2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void txtNome_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox27_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox35_Click(object sender, EventArgs e)
        {

        }
    }
}
