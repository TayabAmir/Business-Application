using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStream.BL;
namespace TechStream.DLInterfaces
{
    public interface IDeviceDL
    {
        List<Device> LoadDeviceData();
        List<Device> GetDevices();
        Device GetDeviceByName(string name);
        void AddDevice(Device device);
        bool EditPrice(string deviceType,string company,string model, double modelPrice);
        bool DeleteModel(string company, string model);
        bool ModelExisted(string buyModel);
        double GetDevicePrice(string buyModel);
        void DeviceSold(string name, string model, double modelPrice);
        List<string> GetSoldDevices();
    }
}
