using System.Text.Json;
using System;
using System.IO;
using System.Windows;

namespace AppForQED_Games
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            openFileButton.Click += ChooseFileButtonClick;
            computeButton.Click += ComputeButtonClick;
        }

        public class ParametersClass
        {
            public double A { get; set; }
            public double B { get; set; }
            public double C { get; set; }
        }

        private void ComputeButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                double a = double.Parse(aTextBox.Text);
                double b = double.Parse(bTextBox.Text);
                double c = double.Parse(cTextBox.Text);

                double delta = b * b - 4 * a * c;

                if (delta < 0)
                {
                    resultTextBlock.Text = "No real roots";
                }
                else
                {
                    double root1 = (-b + Math.Sqrt(delta)) / (2 * a);
                    double root2 = (-b - Math.Sqrt(delta)) / (2 * a);

                    resultTextBlock.Text = $"a = {a}, b = {b}, c = {c}; roots: {root1}, {root2}";
                }
            }
            catch (Exception ex)
            {
                resultTextBlock.Text = ex.Message;
            }
        }

        private void LoadParametersFromFile(string filePath)
        {
            try
            {
                string jsonString = File.ReadAllText(filePath);
                ParametersClass parameters = JsonSerializer.Deserialize<ParametersClass>(jsonString);

                aTextBox.Text = parameters.A.ToString();
                bTextBox.Text = parameters.B.ToString();
                cTextBox.Text = parameters.C.ToString();
            }
            catch (Exception ex)
            {
                resultTextBlock.Text = "Invalid file";
            }
        }

        private void ChooseFileButtonClick(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "JSON Files (*.json)|*.json";
            openFileDialog.DefaultExt = ".json";
            openFileDialog.Multiselect = false;
            bool? result = openFileDialog.ShowDialog();

            if (result == true)
            {
                string filePath = openFileDialog.FileName;
                LoadParametersFromFile(filePath);
            }
        }
    }
}
