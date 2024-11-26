using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStream.DLInterfaces;
using TechStream.DL.DB ;
using TechStream.DL.FH;

namespace BusinessApp_Forms_
{
    public class ObjectHandler
    {
        private static IUserDL UserDL = UserDB.GetInstance();
        private static IDeviceDL DeviceDL = DeviceDB.GetInstance();
        public static string type;
        public static IUserDL GetUserDL() { return UserDL; }
        public static IDeviceDL GetDeviceDL() {  return DeviceDL; }
        public static string GetDeviceType() {  return type; }
    }
}
