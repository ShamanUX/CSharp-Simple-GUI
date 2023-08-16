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
        int deviceID;
        string deviceName;
        private double[] dataX;
        private double[] dataY;

        public Device(int deviceID, string deviceName, (double[], double[]) dataArrays)
        {
            this.deviceID = deviceID;
            this.deviceName = deviceName;
            this.dataX = dataArrays.Item1;
            this.dataY = dataArrays.Item2;
        }

        public string GetName()
        {
            return deviceName;
        }

        public double[] GetDataX()
        {
            return dataX;
        }

        public double[] GetDataY()
        {
            return dataY;
        }
    }
}
