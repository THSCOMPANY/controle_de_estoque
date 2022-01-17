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
using System.Windows.Forms.DataVisualization.Charting;

namespace Controle_Estoque
{
    public partial class TesteBar : Form
    {
        String connectionString = "Server=127.0.0.1;Database=controle_estoque;Uid=root;pwd=Michaeldias03092009;";
        //System.String connectionString = "Server=127.0.0.1;Database=controle_estoque;Uid=root;";

        public TesteBar()
        {
            InitializeComponent();
        }

        private void TesteBar_Load(object sender, EventArgs e)
        {
            //fillChart();
            //fillChartDynamicallyDatabase();
            carregarGraficoMySQLServer();
        }

        //Carrega o gráfico "Chart" de forma estática
        private void fillChart()
        {
            //addXY value in chart1 in series named as salary
            chart1.Series["Produtos"].Points.AddXY("Maquita", "100");
            chart1.Series["Produtos"].Points.AddXY("Serra", "200");
            chart1.Series["Produtos"].Points.AddXY("Celular", "300");
            chart1.Series["Produtos"].Points.AddXY("Teste01", "10");
            chart1.Series["Produtos"].Points.AddXY("Teste02", "20");
            chart1.Series["Produtos"].Points.AddXY("Teste03", "30");
            chart1.Series["Produtos"].Points.AddXY("Teste04", "40");
            chart1.Series["Produtos"].Points.AddXY("Teste05", "50");
            chart1.Series["Produtos"].Points.AddXY("Teste06", "60");
            chart1.Titles.Add("Produtos em Estoque");
        }

        private void fillChartDynamicallyDatabase()
        {
            var series = new Series("QUANTIDADE");
            var series2 = new Series("PRODUTO");

            MySqlConnection connection = new MySqlConnection(connectionString);
            DataSet ds = new DataSet();
            connection.Open();

            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter("SELECT PRODUTO, QUANTIDADE FROM PRODUTOS", connection);
            mySqlDataAdapter.Fill(ds);
            chart1.DataSource = ds;

            chart1.Series.Add(series);
            chart1.Series.Add(series2);
            series.SmartLabelStyle.Enabled = false;
            series.LabelAngle = -90;
            series.Label = "teste";

            chart1.Series["QUANTIDADE"].XValueMember = "PRODUTO";

            chart1.Series["QUANTIDADE"].YValueMembers = "QUANTIDADE";
            chart1.Titles.Add(" PRODUTO x QUANTIDADE");
            
            connection.Close();

            AboutCompanyProductNameVersion();
        }

        public void carregarGraficoMySQLServer()
        {
            //Adiciona o cabeçalho
            
            this.chart1.Series.Add("PRODUTO").LegendText = "Produtos";
            //this.chart1.Series.Add("QUANTIDADE").LegendText = "Quantidade";

            try
            {
                MySqlConnection connection = new MySqlConnection(connectionString);
                //MySqlCommand command = new MySqlCommand("SELECT PRODUTO, QUANTIDADE FROM CONTROLE_ESTOQUE.PRODUTOS;", connection);
                MySqlCommand command = new MySqlCommand("SELECT * FROM CONTROLE_ESTOQUE.PRODUTOS;", connection);
                MySqlDataReader dataReader;
                connection.Open();

                dataReader = command.ExecuteReader();

                var c = chart1;

                c.ChartAreas.Add(new ChartArea());
                c.Width  = 850;
                c.Height = 750;
                Series mySeries = new Series();
                while(dataReader.Read())
                {
                    //this.chart1.Series["PRODUTO"].Points.AddY(dataReader.GetString("PRODUTO"));
                    this.chart1.Series["PRODUTO"].Points.AddXY(dataReader.GetString("PRODUTO"), Convert.ToInt32(dataReader.GetInt32("QUANTIDADE")));
                    c.ChartAreas[0].AxisX.LabelStyle.Angle = 45;
                }
                
            }
            catch(MySqlException ex)
            {

            }
        }

        public void carregarGraficoSQLServer()
        {
            //Fetch the Statistical data from database.
            string query = "SELECT ShipCity, COUNT(OrderId) [Total]";
            query += " FROM Orders WHERE ShipCountry = 'Brazil'";
            query += " GROUP BY ShipCity";
            DataTable dt = GetData(query);

            //Get the names of Cities.
            string[] x = (from p in dt.AsEnumerable()
                          orderby p.Field<string>("ShipCity") ascending
                          select p.Field<string>("ShipCity")).ToArray();

            //Get the Total of Orders for each City.
            int[] y = (from p in dt.AsEnumerable()
                       orderby p.Field<string>("ShipCity") ascending
                       select p.Field<int>("Total")).ToArray();

            chart1.Series[0].LegendText = "Brazil Order Statistics";
            chart1.Series[0].ChartType = SeriesChartType.Line;
            chart1.Series[0].IsValueShownAsLabel = true;
            chart1.Series[0].Points.DataBindXY(x, y);

        }

        private static DataTable GetData(string query)
        {
            String connectionString = "Server=127.0.0.1;Database=controle_estoque;Uid=root;pwd=Michaeldias03092009;";
            //System.String connectionString = "Server=127.0.0.1;Database=controle_estoque;Uid=root;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlDataAdapter sda = new MySqlDataAdapter(query, connection))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    return dt;
                }
            }
        }

        private void AboutCompanyProductNameVersion()
        {
            //this.label1.Text = this.CompanyName + " THS Company " + this.ProductName + " Version: " + this.ProductVersion;
            this.label1.Text = this.CompanyName + " THS Company technology " + " Version: " + this.ProductVersion;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void chart1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
