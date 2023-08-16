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
        private Device[] devices;
        private static Random rng;

        public MainWindow()
        {
            //Initialize RandomNumberGenerator
            rng = new Random();

            InitializeComponent();
            InitializeDevices();
            InitializeComboBox();
            InitializeStyles();

            // Update the chart with the first device's data
            UpdateChart(devices[0].GetDataX(), devices[0].GetDataY());
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

        private void UpdateChart(double[] dataX, double[] dataY)
        {
            WpfPlot1.Plot.Clear();
            WpfPlot1.Plot.AddScatter(dataX, dataY);
            WpfPlot1.Refresh();
        }

        private void InitializeStyles()
        {
            headingLabel.FontSize = 30;
        }

        private void InitializeComboBox()
        {
            deviceComboBox = new ComboBox
            {
                Width = 200,
                Height = 30,
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

        // deviceChanged is triggered on the SelectionChanged event on deviceComboBox.
        private void deviceChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            //Get the current selection's content (device name) and display it in the deviceName label
            ComboBoxItem currentItem = deviceComboBox.SelectedItem as ComboBoxItem;
            string currentDeviceName = currentItem.Content.ToString();
            deviceName.Content = currentDeviceName;

            // Find corresponding device in device list
            foreach (Device device in devices)
            {
                if (device.GetName() == currentDeviceName)
                {
                    UpdateChart(device.GetDataX(), device.GetDataY());
                    break;
                }
            }
        }

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

            // Sort the X array
            Array.Sort(dataX);

            return (dataX, dataY);
        }
    }
}
