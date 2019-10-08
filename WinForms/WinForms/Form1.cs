using System;
using System.IO;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Linq;
using System.Collections.Generic;

namespace WinForms
{
    public partial class Form1 : Form
    {
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

        private void openToolStripMenuItem_Click(object sender, EventArgs e) {
            if(openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            string filename = openFileDialog1.FileName;
            // читаем файл в строку
            string fileText = File.ReadAllText(filename);
            List<string> tmp = fileText.Split('\n').ToList(); tmp.RemoveAt(tmp.Count - 1);
            bindingSource1.Clear();
            tmp.ForEach(x => bindingSource1.Add(new Data() {
                X = Convert.ToDouble(x.Split(' ')[0]),
                Y = Convert.ToDouble(x.Split(' ')[1])
            }));
            MessageBox.Show("Файл загружен");
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e) {
            if(saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            string filename = saveFileDialog1.FileName;
            // сохраняем текст в файл
            string Text = "";
            foreach(var a in bindingSource1.List) {
                Text += a.ToString() + '\n';
            }
            File.WriteAllText(filename, Text);
            MessageBox.Show("Файл сохранен");
        }
    }
    public class Data
    {
        public double X { get; set; }
        public double Y { get; set; }
        public override string ToString() { return $"{X} {Y}"; }
    }
}