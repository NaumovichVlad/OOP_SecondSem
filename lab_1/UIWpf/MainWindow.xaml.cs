using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using MotorVehiclesLibrary;
using ExceptionLibrary;

namespace UIWpf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MotoVehiclesParser vehicles;
        public MainWindow()
        {
            ResizeMode = 0;
            InitializeComponent();
        }

        private void OpenFile()
        {
            var XMLSchemaPath = @"E:\Учёбка\GitHub\OOP_SecondSem\lab_1\data\XMLVehiiclesSchema.xsd";
            var XMLFilePath = pathTBox.Text;
            vehicles = new MotoVehiclesParser(XMLFilePath, XMLSchemaPath);
        }
        public void FillTable()
        {
            VehiclesListView.Items.Clear();
            var gridView = new GridView();
            VehiclesListView.View = gridView;
            var properties = Enum.GetNames(typeof(ClassProperties));
            var columnNames = Enum.GetNames(typeof(MotorVehiclesLibrary.Properties));
            for (var i = 0; i < properties.Length; i++)
            { 
                GridViewColumn column = new GridViewColumn();
                column.DisplayMemberBinding = new Binding(properties[i]);
                column.Header = columnNames[i];
                column.Width = 100;
                gridView.Columns.Add(column);
            }
            for (var i = 0; i < vehicles.Count; i++)
            {
                VehiclesListView.Items.Add(vehicles[i]);
            }
            saveButton.IsEnabled = true;
            addButton.IsEnabled = true;
        }

        private void SelectFilePathButton(object sender, RoutedEventArgs e)
        {
            
            Microsoft.Win32.OpenFileDialog openFile = new Microsoft.Win32.OpenFileDialog();
            openFile.Filter = "XML файл(*.xml)|*.xml";
            bool? result = openFile.ShowDialog();
            if (result == true)
            {
                pathTBox.Text = openFile.FileName;
            }
        }

        private void OpenFileButton(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFile();
                FillTable();
            }
            catch (InvalidXMLFileException exception)
            {
                ErrorMessage(exception.Message);
            }
            catch (DirectoryNotFoundException)
            {
                ErrorMessage("File not found");
            }
            catch (NotSupportedException)
            {
                ErrorMessage("Invalid directory");
            }
        }

        private void ExitButton(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SaveButton(object sender, RoutedEventArgs e)
        {
            var path = string.Empty;
            Microsoft.Win32.SaveFileDialog saveFile = new Microsoft.Win32.SaveFileDialog();
            saveFile.Filter = "XML файл(*.xml)|*.xml";
            bool? result = saveFile.ShowDialog();
            if (result == true)
            {
                path = saveFile.FileName;
                vehicles.WriteXML(path);
            }
        }

        private void VehiclesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            deleteButton.IsEnabled = true;
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            vehicles.Remove(VehiclesListView.Items.IndexOf(VehiclesListView.SelectedItem));
            VehiclesListView.Items.Remove(VehiclesListView.SelectedItem);
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            var editWindow = new EditingWindow(vehicles);
            editWindow.ShowDialog();
            VehiclesListView.Items.Add(vehicles[vehicles.Count - 1]);
        }

        private void ErrorMessage(string textMessage)
        {
            MessageBox.Show(textMessage, "ERROR!!!", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
