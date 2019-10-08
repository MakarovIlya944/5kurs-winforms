using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public void ShowGraphic() {
            chart1.DataSource = null;
            chart1.Series[0].ChartType = currentType;
            chart1.DataSource = bindingSource1;
        }

        public Form1() {
            InitializeComponent();
            bindingSource1.Add(new Data() { X = 0, Y = 0 });
            bindingSource1.Add(new Data() { X = 1, Y = 1 });
            bindingSource1.Add(new Data() { X = 2, Y = 4 });
            dataGridView1.DataSource = bindingSource1;
        }

        private void AddButton_Click(object sender, EventArgs e) {
            bindingSource1.Add(new Data() { X = 4, Y = 16 });
        }

        private void DrawAsLines_Click(object sender, EventArgs e) {
            currentType = SeriesChartType.Line;
            ShowGraphic();
        }

        private void DrawAsSpline_Click(object sender, EventArgs e) {
            currentType = SeriesChartType.Spline;
            ShowGraphic();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {
            currentType = (SeriesChartType)(comboBox1.SelectedIndex + 3);
            ShowGraphic();
        }

        private void bindingSource1_ListChanged(object sender, ListChangedEventArgs e) {
            ShowGraphic();
        }
    }
    public class Data
    {
        public double X { get; set; }
        public double Y { get; set; }
    }
}