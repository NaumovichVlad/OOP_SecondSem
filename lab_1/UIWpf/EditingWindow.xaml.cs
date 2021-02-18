using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MotorVehiclesLibrary;

namespace UIWpf
{
    /// <summary>
    /// Логика взаимодействия для EditingWindow.xaml
    /// </summary>
    public partial class EditingWindow : Window
    {
        MotoVehiclesParser vehicles;
        public EditingWindow(MotoVehiclesParser vehicles)
        {
            InitializeComponent();
            this.vehicles = vehicles;
            ResizeMode = 0;

        }

        private void CloseButton(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] properties = new string[6];
                properties[0] = textBox0.Text;
                properties[1] = textBox1.Text;
                properties[2] = textBox2.Text;
                properties[3] = textBox3.Text;
                properties[4] = textBox4.Text;
                properties[5] = textBox5.Text;
                vehicles.CreateVehicle(properties);
                Close();
            }
            catch(FormatException)
            {
                ErrorMessage("Wrong input!!!");
            }
        }

        private void ErrorMessage(string textMessage)
        {
            MessageBox.Show(textMessage, "ERROR!!!", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
