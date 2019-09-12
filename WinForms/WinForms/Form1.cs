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
        public BindingSource Data { get; set; }
        private void ShowGraphic(SeriesChartType t) {
            chart1.DataSource = null;
            chart1.Series[0].ChartType = t;
            chart1.DataSource = Data;
        }

        public Form1() {
            Data = new BindingSource();
            Data.Add(new Data() { X = 0, Y = 0 });
            Data.Add(new Data() { X = 1, Y = 1 });
            Data.Add(new Data() { X = 2, Y = 4 });
            InitializeComponent();
            dataGridView1.DataSource = Data;
        }
        private void AddButton_Click(object sender, EventArgs e) {
            Data.Add(new Data() { X = 4, Y = 16 });
        }
        private void DrawAsLines_Click(object sender, EventArgs e) {
            ShowGraphic(SeriesChartType.Line);
        }
        private void DrawAsSpline_Click(object sender, EventArgs e) {
            ShowGraphic(SeriesChartType.Spline);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {
            ShowGraphic((SeriesChartType)(comboBox1.SelectedIndex + 3));
        }
    }
    public class Data
    {
        public double X { get; set; }
        public double Y { get; set; }
    }
}