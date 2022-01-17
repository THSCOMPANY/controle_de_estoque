using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Configuration;
using Microsoft.Office.Interop.Excel;

namespace Controle_Estoque
{
    public partial class Graficos : Form
    {
        String connectionString = "Server=127.0.0.1;Database=controle_estoque;Uid=root;pwd=Michaeldias03092009;";
        //String connectionString = "Server=127.0.0.1;Database=controle_estoque;Uid=root;";

        public LiveCharts.SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }

        //MySqlConnection conn = new
        //MySqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

        public Graficos()
        {
            InitializeComponent();
            //LoadData();
            LoadGrafico02();
        }

        private void Graficos_Load(object sender, EventArgs e)
        {
            produtosGraphicsBindingSource.DataSource = new List<ProdutosGraphics>();
            cartesianChart1.AxisX.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Meses",
                Labels = new[] { "Jan", "Fev", "Mar", "Abr", "Mai", "Jun", "Jul", "Ago", "Set", "Out", "Nov", "Dez" }
            });

            cartesianChart1.AxisY.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Produtos",
                LabelFormatter = valores => valores.ToString("C")
            });

            cartesianChart1.LegendLocation = LiveCharts.LegendLocation.Right;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*cartesianChart1.Series.Clear();
            SeriesCollection series = new SeriesCollection();

            var years = (from o in produtosGraphicsBindingSource.DataSource as List<ProdutosGraphics>
                         select new { Ano = o.ano }).Distinct();

            foreach (var ano in years)
            {
                List<double> values = new List<double>();
                for (int month = 1; month <= 12; month++)
                {
                    double value = 0;
                    var data = from o in produtosGraphicsBindingSource.DataSource as List<ProdutosGraphics>
                               where o.ano.Equals(ano.Ano) && o.mes.Equals(month)
                               orderby o.mes ascending
                               select new { o.valores, o.mes };
                    if (data.SingleOrDefault() != null)
                        value = data.SingleOrDefault().valores;
                    values.Add(value);
                }
                series.Add(new LineSeries() { Title = ano.Ano.ToString(), Values = new ChartValues<double>(values) });
            }

            cartesianChart1.Series = series;*/
        }

        /*private void LoadData()
        {
            List<String> allValues = new List<String>();

            MySqlConnection connection = new MySqlConnection(connectionString);

            string querie = "SELECT * FROM PRODUTOS";

            //connection.ConnectionString = ConfigurationManager.ConnectionStrings[""].ConnectionString;
            connection.Open();

            //cmd = new MySqlCommand("SELECT * FROM PRODUTOS", connection);
            MySqlCommand cmd = new MySqlCommand(querie, connection);
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                allValues.Add(Convert.ToString(dr["produtos"]));
            }

            SeriesCollection = new SeriesCollection
                {
                    new LineSeries
                    {
                        Values = new ChartValues<String>(allValues)
                    }
                };

            Labels = new[] { "Jan", "Fev", "Mar", "Abr", "Mai" };

            //DataContext = this;

        }*/

        /*private void LoadGrafico()
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM PRODUTOS;", connection);
            MySqlDataReader dataReader;

            try
            {
                connection.Open();

                dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    this.cartesianChart1.Series[].Points.AddXY(dataReader.GetString("Nome"), dataReader.GetInt32("Produto"));
                }
            } catch (MySqlException ex)
            {
                Console.WriteLine(ex);
            }
        }*/

        private void LoadGrafico02()
        {
            MySqlConnection connection = new MySqlConnection(connectionString);

            List<string> sensor1 = new List<string>();

            connection.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM PRODUTOS ORDER BY ID_PRODUTO DESC LIMIT 5", connection);
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                sensor1.Add(Convert.ToString(dr["PRODUTOS"]));
            }

            SeriesCollection = new LiveCharts.SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<string>(sensor1)
        }
    };

    Labels = new[] {"Jan", "Fev", "Mar", "Abr", "Mai", "Jun", "Ago"};

            //DataContext = this;
            connection.Close();
        }
    }
}
