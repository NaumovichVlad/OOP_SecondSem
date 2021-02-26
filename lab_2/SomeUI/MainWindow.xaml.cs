using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using StreamEditorLibrary;

namespace SomeUI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (mainTextBox.Text != string.Empty)
            {
                var efficiencys = WriteFile(mainTextBox.Text);
                MessageBox.Show(string.Format("Запись завершена\nКПД BufferedStream = {0}%\nКПД FileStream = {1}%\nКПД MemoryStream = {2}%",
                    efficiencys[0], efficiencys[1], efficiencys[2]), "Success");
            }
            else
                MessageBox.Show("Заполните текстовое поле!", "Warning!");
            
        }

        private double[] WriteFile(string line)
        {
            var efficiencys = new double[3];
            byte[] ByteLine = new UTF8Encoding(true).GetBytes(line);
            byte[] newByteLine;

            using (StreamDecorator msDecorator = new StreamDecorator(new MemoryStream(100)))
            {
                msDecorator.Write(ByteLine, 0, ByteLine.Length);
                newByteLine = new byte[msDecorator.Length];
                msDecorator.Seek(0, SeekOrigin.Begin);
                msDecorator.Read(newByteLine, 0, newByteLine.Length);
                efficiencys[2] = msDecorator.ComputeEfficiency();
                msDecorator.Close();
            }

            using (var bfStream = new StreamDecorator(new BufferedStream(File.OpenWrite("noteBS.bin"))))
            {
                bfStream.Write(newByteLine, 0, newByteLine.Length);
                efficiencys[0] = bfStream.ComputeEfficiency();
                bfStream.Close();
            }
            using (var fsStream = new StreamDecorator(new FileStream("noteFS.bin", (FileMode)FileAccess.Write)))
            {
                fsStream.Write(newByteLine, 0, newByteLine.Length);
                efficiencys[1] = fsStream.ComputeEfficiency();
                fsStream.Close();
            }
            return efficiencys;
        }
    }
}
