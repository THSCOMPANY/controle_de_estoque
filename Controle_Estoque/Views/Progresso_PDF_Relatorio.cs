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
    public partial class Progresso_PDF_Relatorio : Form
    {
        public Progresso_PDF_Relatorio()
        {
            InitializeComponent();
        }

        private void progressBarTimer_Tick(object sender, EventArgs e)
        {
            if(progressBarPanel.Width != topPanel.Width)
            {
                progressBarPanel.Width = progressBarPanel.Width + 2; // 2 * 10 == 200 a quantidade total do contador 10 é o valor do intervalo

                int contador = progressBarPanel.Width;

                if(progressBarPanel.Width == 470)//Tive que dobrar o valor, para acompanhar o processo da CPU que é de execução rápida
                {
                    messageLabel.Visible = true;
                }
            }
            else
            {
                //messageLabel.Visible = true;

                Relatorio_Estoque relatorio = new Relatorio_Estoque();
                relatorio.Close();
                
                //System.Threading.Thread.Sleep(50000);
                //System.Threading.Thread.Sleep((int)System.TimeSpan.FromSeconds(3).TotalMilliseconds);
                progressBarTimer.Stop();

                Hide();
            }
        }
        
        private void messageLabel_Click(object sender, EventArgs e)
        {

        }

        private void Progresso_PDF_Relatorio_Load(object sender, EventArgs e)
        {
            progressBarTimer.Start(); //Inicia  o contador
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
