using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStream.BL;
using TechStream.DL.DB;
using TechStream.DLInterfaces;

namespace TechStream.DL.FH
{
    public class DeviceFH:IDeviceDL
    {
        public static List<Device> Devices = new List<Device>();
        static DeviceFH instance = null;
        public static DeviceFH GetInstance()
        {
            if (instance == null)
                instance = new DeviceFH();
            return instance;
        }
        public List<Device> GetDevices()
        { return Devices; }

        public Device GetDeviceByName(string name)
        {
            foreach(Device device in Devices)
                if(device.GetModel().Equals(name))
                    return device;
            return null;
        }
        public void AddDevice(Device device)
        {
            Devices.Add(device);
            SaveDeviceData();
        }
        public List<Device> LoadDeviceData()
        {
            Devices.Clear();
            string record;
            StreamReader file = new StreamReader("Device.txt");
            while ((record = file.ReadLine()) != null)
            {
                string[] devicess = record.Split(',');
                Devices.Add(new Device(devicess[0], devicess[1], devicess[2], double.Parse(devicess[3])));
            }
            file.Close();
            return Devices;
        }
        public void SaveDeviceData()
        {
            using (StreamWriter writer = new StreamWriter("Device.txt",false))
            {
                foreach (Device device in Devices)
                {
                    writer.WriteLine($"{device.GetDeviceType()},{device.GetCompany()},{device.GetModel()},{device.GetModelPrice()}");
                }
            }
        }
        public bool EditPrice(string deviceType,string company,string model, double modelPrice)
        {
            foreach (Device device in Devices)
            {
                if ((company == null || device.GetCompany() == company ) && device.GetModel() == model)
                {
                    device.SetPrice(modelPrice);
                    SaveDeviceData();
                    return true;
                }
            }
            return false;
        }
        public bool DeleteModel(string company, string model)
        {
            for (int j = 0; j < Devices.Count; j++)
            {
                if ((Devices[j].GetCompany() == company || company == null) && Devices[j].GetModel() == model)
                {
                    Devices.RemoveAt(j);
                    SaveDeviceData();
                    return true;
                }
            }
            return false;
        }
        public bool ModelExisted(string buyModel)
        {
            foreach (Device device in Devices)
                if (buyModel.Equals(device.GetModel()))
                    return true;
            return false;
        }
        public double GetDevicePrice(string buyModel)
        {
            for (int i = 0; i < Devices.Count; i++)
            {
                if (Devices[i].GetModel() == buyModel)
                {
                    return Devices[i].GetModelPrice();
                }
            }
            return 0.0;
        }

        public void DeviceSold(string name, string model, double modelPrice)
        {
            using (StreamWriter writer = new StreamWriter("SoldDevices.txt", true))
            {
                writer.WriteLine($"{ name},{model},{modelPrice}");
            }
        }

        public List<string> GetSoldDevices()
        {
            List<string> devices = new List<string>();
            string record;
            StreamReader file = new StreamReader("SoldDevices.txt");
            while ((record = file.ReadLine()) != null)
            {
                devices.Add(record);
            }
            file.Close();
            return devices;
        }
    }
}
