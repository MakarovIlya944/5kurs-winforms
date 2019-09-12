using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WinForms
{
    public partial class Form1 : Form
    {
        public BindingSource DataCoord { get; set; }
        private SeriesChartType currentType = SeriesChartType.Line;
        private void ShowGraphic() {
            chart1.DataSource = null;
            chart1.Series[0].ChartType = currentType;
            chart1.DataSource = DataCoord;
        }

        public Form1() {
            bindingSource1 = new BindingSource();
            DataCoord = bindingSource1;
            DataCoord.Add(new Data() { X = 0, Y = 0 });
            DataCoord.Add(new Data() { X = 1, Y = 1 });
            DataCoord.Add(new Data() { X = 2, Y = 4 });
            InitializeComponent();
            dataGridView1.DataSource = DataCoord;
        }

        private void AddButton_Click(object sender, EventArgs e) {
            DataCoord.Add(new Data() { X = 4, Y = 16 });
        }

        private void DrawAsLines_Click(object sender, EventArgs e) {
            currentType = SeriesChartType.Line;
        }

        private void DrawAsSpline_Click(object sender, EventArgs e) {
            currentType = SeriesChartType.Spline;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {
            currentType = (SeriesChartType)(comboBox1.SelectedIndex + 3);
        }
        
        private void bindingSource1_DataSourceChanged(object sender, EventArgs e) {
            ShowGraphic();
        }
    }
    public class Data
    {
        public double X { get; set; }
        public double Y { get; set; }
    }
}