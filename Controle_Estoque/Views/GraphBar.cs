using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//Include the required namespace of LiveCharts
using LiveCharts;
using LiveCharts.Wpf;

namespace Controle_Estoque
{
    public partial class GraphBar : Form
    {
        public GraphBar()
        {
            InitializeComponent();
        }
        // It's important to run the code inside the load event of the form, so the component will be drawn after this correctly
        private void GraphBar_Load(object sender, EventArgs e)
        {
            // Define the label that will appear over the piece of the chart
            // in this case we'll show the given value and the percentage e.g 123 (8%)
            Func<ChartPoint, string> labelPoint = ChartPoint => string.Format("{0}({1:P})", ChartPoint.Y, ChartPoint.Participation);

            //Define a collection of items to display in the chart
            SeriesCollection piechartData = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "ACIONADOR",
                    Values = new ChartValues<double> { 10 },
                    DataLabels = true,
                    LabelPoint = labelPoint
                },

                new PieSeries
                {
                    Title = "PNEU",
                    Values = new ChartValues<double> { 25 },
                    DataLabels = true,
                    LabelPoint = labelPoint
                },

                new PieSeries
                {
                    Title = "PARAFUSO",
                    Values = new ChartValues<double> { 45 },
                    DataLabels = true,
                    LabelPoint = labelPoint
                }
        };

            //You can add a new item dinamically with the add method of the collection
            //Useful when you define the data dinamically and not statically
            piechartData.Add(
                new PieSeries
                {
                    Title = "ARRUELA",
                    Values = new ChartValues<double> { 65 },
                    DataLabels = true,
                    LabelPoint = labelPoint,
                    Fill = System.Windows.Media.Brushes.Gray
                }
              );

            //Define the collection of values to display in the Pie Chart
            pieChart1.Series = piechartData;

            //Set the legend location to appear in the Right side of the chart
            pieChart1.LegendLocation = LegendLocation.Right;

        }
    }
}
