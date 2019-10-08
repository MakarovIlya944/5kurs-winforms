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
        private int maxBind = 1;
        public void ShowGraphic() {
            chart1.DataSource = null;
            chart1.Series[0].ChartType = currentType;
            chart1.DataSource = currentBinding;
        }

        private Dictionary<int, BindingSource> bindings;
        public BindingSource currentBinding;

        public Form1() {
            bindings = new Dictionary<int, BindingSource>();
            currentBinding = new BindingSource();
            currentBinding.ListChanged += new ListChangedEventHandler(bindingSource1_ListChanged);
            bindings.Add(1, currentBinding);

            InitializeComponent();

            currentBinding.Add(new Data() { X = 0, Y = 0 });
            currentBinding.Add(new Data() { X = 1, Y = 1 });
            currentBinding.Add(new Data() { X = 2, Y = 4 });
            dataGridView1.DataSource = currentBinding;
            comboBoxTable.Items.Add(1);
        }

        private void AddButton_Click(object sender, EventArgs e) {
            currentBinding.Add(new Data() { X = 4, Y = 16 });
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
            currentBinding = new BindingSource();
            tmp.ForEach(x => currentBinding.Add(new Data() {
                X = Convert.ToDouble(x.Split(' ')[0]),
                Y = Convert.ToDouble(x.Split(' ')[1])
            }));
            maxBind++;
            bindings.Add(maxBind, currentBinding);
            comboBoxTable.Items.Add(maxBind);
            label1.Text = "Table " + maxBind.ToString();
            comboBoxTable.SelectedItem = maxBind;
            dataGridView1.DataSource = currentBinding;
            ShowGraphic();
            MessageBox.Show("Файл загружен");
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e) {
            if(saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            string filename = saveFileDialog1.FileName;
            // сохраняем текст в файл
            string Text = "";
            foreach(var a in currentBinding.List) {
                Text += a.ToString() + '\n';
            }
            File.WriteAllText(filename, Text);
            MessageBox.Show("Файл сохранен");
        }

        private void removeCurrentToolStripMenuItem_Click(object sender, EventArgs e) {
            if(bindings.Count == 1) {
                MessageBox.Show("Нельзя удалить последний");
                return;
            }
            bindings.Remove((int)comboBoxTable.SelectedItem);
            int i = comboBoxTable.Items.IndexOf((int)comboBoxTable.SelectedItem);
            comboBoxTable.Items.RemoveAt(i);
            var tmp = bindings.LastOrDefault();
            currentBinding = tmp.Value;
            label1.Text = "Table " + tmp.Key.ToString();
            comboBoxTable.SelectedItem = tmp.Key;
            dataGridView1.DataSource = currentBinding;
            ShowGraphic();
        }

        private void comboBoxTable_SelectedIndexChanged(object sender, EventArgs e) {
            currentBinding = bindings[(int)comboBoxTable.SelectedItem];
            label1.Text = "Table " + comboBoxTable.SelectedItem.ToString();
            dataGridView1.DataSource = currentBinding;
            ShowGraphic();
        }
    }
    public class Data
    {
        public double X { get; set; }
        public double Y { get; set; }
        public override string ToString() { return $"{X} {Y}"; }
    }
}