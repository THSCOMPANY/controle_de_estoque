using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Controle_Estoque
{
    public partial class Menu : Form
    {
        
        public Menu()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cadastrar formCadastro = new Cadastrar();//Cria uma referência para o Form
            formCadastro.StartPosition = FormStartPosition.CenterScreen;//Obriga o Form a inicializar no centro da tela
            formCadastro.Show();//mostra o form
            //cad.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Hide();
            Application.Exit(); //Exit process the Form
        }

        private void groupBox1_Enter(object sender, EventArgs e) { }

        private void Menu_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false; //impede a maximização do form
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NF_Entrada entrada = new NF_Entrada();
            entrada.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //System.Windows.Forms.MessageBox.Show("Em Desenvolvimento!!");
            Relatorio_Estoque estoque = new Relatorio_Estoque();
            estoque.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //System.Windows.Forms.MessageBox.Show("Em Desenvolvimento!!");  
            NF_Saida saida = new NF_Saida();
            saida.ShowDialog();
        }
    }
}
