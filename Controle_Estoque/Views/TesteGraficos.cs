using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Threading;
using System.Windows.Threading;
using LiveCharts;
using LiveCharts.Wpf;
using static Controle_Estoque.DatabaseMySqlWorkbench;

namespace Controle_Estoque
{
    public partial class TesteGraficos : Form
    {
        String connectionString = "Server=127.0.0.1;Database=controle_estoque;Uid=root;pwd=Michaeldias03092009;";
        //System.String connectionString = "Server=127.0.0.1;Database=controle_estoque;Uid=root;";

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<string, string> YFormatter { get; set; }

        public TesteGraficos()
        {
            InitializeComponent();
        }

        private void TesteGraficos_Load(object sender, EventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);

            List<string> produto    = new List<string>();
            List<string> quantidade = new List<string>();

            connection.Open();

            MySqlCommand cmd = new MySqlCommand("SELECT * FROM PRODUTOS ORDER BY ID_PRODUTO DESC LIMIT 5", connection);

            MySqlDataReader dr = cmd.ExecuteReader();

            while(dr.Read())
            {
                produto.Add(Convert.ToString(dr["produto"]));
                quantidade.Add(Convert.ToString(dr["quantidade"]));
            }

            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Teste",
                },

                new LineSeries
                {
                    Title = "Nomes",
                    Values = new ChartValues<string>(produto)
                },
            
                new LineSeries
                {
                    Title = "Quantidade",
                    Values = new ChartValues<string>(quantidade)
                }
            };

            Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May" };

            connection.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void elementHost1_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }
    }
}
