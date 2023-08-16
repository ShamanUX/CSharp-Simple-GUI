using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Simple_GUI
{
    /// <summary>
    /// Class for storing data for devices
    /// </summary>
    public class Device
    {
        private int _deviceID;
        private string _deviceName;
        private double[] _dataX; //Dataset, X values
        private double[] _dataY; //Dataset, Y values

        public Device(int deviceID, string deviceName, (double[], double[]) dataArrays)
        {
            _deviceID = deviceID;
            _deviceName = deviceName;
            _dataX = dataArrays.Item1;
            _dataY = dataArrays.Item2;
        }

        public string GetName()
        {
            return _deviceName;
        }

        public double[] GetDataX()
        {
            return _dataX;
        }

        public double[] GetDataY()
        {
            return _dataY;
        }
    }
}
