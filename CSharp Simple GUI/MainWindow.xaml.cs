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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CSharp_Simple_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ComboBox deviceComboBox;
        private Grid infoGrid;
        private Device[] devices;
        private static Random rng;

        public MainWindow()
        {
            //Initialize RandomNumberGenerator
            rng = new Random();

            InitializeComponent();
            InitializeDevices();
            InitializeComboBox();
            InitializeInfoGrid();
            InitializeStyles();

            // Update the chart and infoGrid with the first device's data
            UpdateChart(devices[0].GetDataX(), devices[0].GetDataY());
            UpdateInfoGrid(devices[0]);
        }

        private void InitializeDevices()
        {
            // The number of devices to initialize
            int deviceAmount = 3;
            devices = new Device[3];

            for (int deviceIndex = 0; deviceIndex < deviceAmount; deviceIndex++)
            {
                // Corrected index, so that indexing starts from 1
                int correctedIndex = deviceIndex + 1;
                (double[], double[]) dataArrays = GenerateDummyData(5);
                Device newDevice = new Device(correctedIndex, "Device #" + correctedIndex, dataArrays);
                devices[deviceIndex] = newDevice;
            }
        }

        private void InitializeComboBox()
        {
            deviceComboBox = new ComboBox
            {
                VerticalAlignment = VerticalAlignment.Center
            };

            // Create ComboBoxItems for each device
            foreach (Device device in devices)
            {
                ComboBoxItem newItem = new ComboBoxItem { Content = device.GetName() };
                deviceComboBox.Items.Add(newItem);
            }

            // Select the first item initially
            deviceComboBox.SelectedItem = deviceComboBox.Items[0];
            deviceName.Content = devices[0].GetName();

            // Add function for handling SelectionChanged 
            deviceComboBox.SelectionChanged += deviceChanged;

            //Add the ComboBox to the mainwindow container
            deviceSelectorStackPanel.Children.Add(deviceComboBox);
        }

        private void InitializeInfoGrid()
        {
            infoGrid = new Grid();
            infoGrid.Height = 200;
            infoGrid.HorizontalAlignment = HorizontalAlignment.Stretch;
            infoGrid.VerticalAlignment = VerticalAlignment.Top;
            
            infoGrid.ShowGridLines = true;

            // Define the Columns
            ColumnDefinition colDef1 = new ColumnDefinition();
            ColumnDefinition colDef2 = new ColumnDefinition();
            infoGrid.ColumnDefinitions.Add(colDef1);
            infoGrid.ColumnDefinitions.Add(colDef2);

            // Define the Rows
            RowDefinition rowDef1 = new RowDefinition();
            RowDefinition rowDef2 = new RowDefinition();
            RowDefinition rowDef3 = new RowDefinition();
            RowDefinition rowDef4 = new RowDefinition();
            RowDefinition rowDef5 = new RowDefinition();
            RowDefinition rowDef6 = new RowDefinition();
            infoGrid.RowDefinitions.Add(rowDef1);
            infoGrid.RowDefinitions.Add(rowDef2);
            infoGrid.RowDefinitions.Add(rowDef3);
            infoGrid.RowDefinitions.Add(rowDef4);
            infoGrid.RowDefinitions.Add(rowDef5);
            infoGrid.RowDefinitions.Add(rowDef6);

            // Add the first header row X 
            TextBlock headerTextX = new TextBlock();
            headerTextX.Text = "X";
            headerTextX.FontSize = 20;
            headerTextX.FontWeight = FontWeights.Bold;
            Grid.SetRow(headerTextX, 0);
            Grid.SetColumn(headerTextX, 0);
            headerTextX.HorizontalAlignment = HorizontalAlignment.Center;

            TextBlock headerTextY = new TextBlock();
            headerTextY.Text = "Y";
            headerTextY.FontSize = 20;
            headerTextY.FontWeight = FontWeights.Bold;
            Grid.SetRow(headerTextY, 0);
            Grid.SetColumn(headerTextY, 1);
            headerTextY.HorizontalAlignment = HorizontalAlignment.Center;

            // Add textBlocks as children to infogrid
            infoGrid.Children.Add(headerTextX);
            infoGrid.Children.Add(headerTextY);

            infoStackPanel.Children.Add(infoGrid);
        }
        private void InitializeStyles()
        {
            headingLabel.FontSize = 30;
        }

        // Updates the infoGrid with that device's data which is given in the parameter
        private void UpdateInfoGrid(Device device)
        {
            // Clear all textBlocks that are not in the header row
            for (int i = infoGrid.Children.Count - 1; i >= 0; i--)
            {
                int rowIndex = Grid.GetRow(infoGrid.Children[i]);
                if (rowIndex != 0)
                {
                    infoGrid.Children.RemoveAt(i);
                }
            }

            double[] dataX = device.GetDataX();
            double[] dataY = device.GetDataY();

            // Add X and Y values to the grid for each data point
            for (int i = 0; i < dataX.Length; i++ )
            {
                TextBlock textX = new TextBlock();
                TextBlock textY = new TextBlock();
                textX.Text = dataX[i].ToString();
                textY.Text = dataY[i].ToString();
                textX.HorizontalAlignment = HorizontalAlignment.Center;
                textY.HorizontalAlignment = HorizontalAlignment.Center;

                // We're starting to add values from the second row on the grid, as the header is the first row
                Grid.SetRow(textX, i + 1);
                Grid.SetColumn(textX, 0);
                Grid.SetRow(textY, i + 1);
                Grid.SetColumn(textY, 1);

                infoGrid.Children.Add(textX);
                infoGrid.Children.Add(textY);
            }
        }

        private void UpdateChart(double[] dataX, double[] dataY)
        {
            WpfPlot1.Plot.Clear();
            WpfPlot1.Plot.AddScatter(dataX, dataY);
            WpfPlot1.Refresh();
        }


        // deviceChanged is triggered on the SelectionChanged event on deviceComboBox.
        private void deviceChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            //Get the current selection's content (device name) and display it in the deviceName label
            ComboBoxItem currentItem = deviceComboBox.SelectedItem as ComboBoxItem;
            string currentDeviceName = currentItem.Content.ToString();
            deviceName.Content = currentDeviceName;

            // Find corresponding device in device list and update its data to the chart
            foreach (Device device in devices)
            {
                if (device.GetName() == currentDeviceName)
                {
                    UpdateChart(device.GetDataX(), device.GetDataY());
                    UpdateInfoGrid(device);
                    break;
                }
            }
        }

        // Creates data arrays for devices, from random values between 0 and 21.  
        private (double[], double[]) GenerateDummyData(int maxDataPoints)
        {
            double[] dataX = new double[maxDataPoints];
            double[] dataY = new double[maxDataPoints];

            // Add a number of random values to the lists
            for (int i = 0; i < maxDataPoints; i++)
            {
                dataX[i] = rng.Next(0, 21);
                dataY[i] = rng.Next(0, 21);
            }

            // Sort the X array, to make the data appear linear on that axis
            Array.Sort(dataX);

            return (dataX, dataY);
        }
    }
}
