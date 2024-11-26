using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStream.BL;
using TechStream.DL.DB;
using TechStream.DL.FH;
using TechStream.DLInterfaces;
using TechStream.Utils;

namespace BusinessApp.UI
{
    internal class ConsoleUtility
    {
        public static void PrintHeader()
        {
            Console.WriteLine("___________________________________________________________________________________________________________________________");
            Console.WriteLine("|                                                                                                                         |");
            Console.WriteLine("|                                                                                                                         |");
            Console.WriteLine("|                              ____  _   _ _____ ___ _  ___   _   _____ _____ ____ _   _                                  |");
            Console.WriteLine("|                             | ___|| | | | ____|_ _| |/ | | | | |_   _| ____| ___| | | |                                 |");
            Console.WriteLine("|                             |___ || |_| |  _|  | || ' /| |_| |   | | |  _| | |  | |_| |                                 |");
            Console.WriteLine("|                              ___)||  _  | |___ | || . \\|  _  |   | | | |__ | |__|  _  |                                 |");
            Console.WriteLine("|                             |____||_| |_|_____|___|_|\\_|_| |_|   |_| |_____|____|_| |_|                                 |");
            Console.WriteLine("|                                                                                                                         |");
            Console.WriteLine("|                                                                                                                         |");
            Console.WriteLine("|                                                                                                                         |");
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------");
        }
        static public bool CheckOptionValidation(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                char ch = input[i];
                if (ch < '0' || ch > '9')
                {
                    return false;
                }
            }
            return true;
        }
        public static int ValidOption()
        {
            while (true)
            {
                Console.WriteLine("Enter your choice... ");
                string input = Console.ReadLine();
                if (ConsoleUtility.CheckOptionValidation(input))
                {
                    return int.Parse(input);
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer.");
                }
            }
        }
        public static void CsPh()
        {
            Console.Clear();
            ConsoleUtility.PrintHeader();
        }

        public static void PrintMobiles(List<Device> Devices)
        {
            Console.WriteLine("\nFollowing is the list of Mobiles with their prices.\n");

            int mX = 81;
            int mY = 24;

            var uniqueCompanies = Devices.Where(d => d.GetDeviceType().Equals("MOBILE", StringComparison.OrdinalIgnoreCase)).Select(d => d.GetCompany().ToUpper()).Distinct();
            Console.WriteLine("\n \n");
            foreach (string companyName in uniqueCompanies)
            {
                Console.WriteLine($"\t \t \t \t \t \t \t \t {companyName}\n");
                Console.WriteLine("\t \t \t \t \t Model" + new string(' ', 28) + "\t \t Prices\n");
                Console.WriteLine("                              ---------------------------------------------------------------------------\n");

                foreach (Device device in Devices)
                {
                    if (device.GetCompany().ToUpper() == companyName && device.GetDeviceType()=="MOBILE")
                    {
                        Console.WriteLine($"\t \t \t \t{device.GetModel()}");
                        Console.SetCursorPosition(mX, mY);
                        Console.WriteLine(device.GetModelPrice());
                        mY++;
                    }
                }

                Console.WriteLine("                              ---------------------------------------------------------------------------\n");

                mY += 8;
            }
        }

        public static void PrintLaptops(List<Device> devices)
        {
            Console.WriteLine("\nFollowing is the list of Laptops with their prices.\n");
            int mX = 81;
            int mY = 24;

            var uniqueCompanies = devices
                .Where(d => d.GetDeviceType().Equals("LAPTOP", StringComparison.OrdinalIgnoreCase))
                .Select(d => d.GetCompany().ToUpper())
                .Distinct();

            Console.WriteLine("\n \n");
            foreach (string companyName in uniqueCompanies)
            {
                Console.WriteLine($"\t \t \t \t \t \t \t \t {companyName}\n");
                Console.WriteLine("\t \t \t \t \t Model" + new string(' ', 28) + "\t \t Prices\n");
                Console.WriteLine("                              ---------------------------------------------------------------------------\n");

                foreach (Device device in devices)
                {
                    if (device.GetCompany().ToUpper() == companyName && device.GetDeviceType().Equals("Laptop", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine($"\t \t \t \t{device.GetModel()}");
                        Console.SetCursorPosition(mX, mY);
                        Console.WriteLine(device.GetModelPrice());
                        mY++;
                    }
                }

                Console.WriteLine("                              ---------------------------------------------------------------------------\n");

                mY += 8; 
            }
        }

        public static void PrintSW(List<Device> devices)
        {
            Console.WriteLine("\nFollowing is the list of Smart Watches with their prices.\n");
            int mX = 90;
            int mY = 24;

            var uniqueCompanies = devices
                .Where(d => d.GetDeviceType().Equals("SMART WATCH", StringComparison.OrdinalIgnoreCase))
                .Select(d => d.GetCompany().ToUpper())
                .Distinct();

            Console.WriteLine("\n \n");
            foreach (string companyName in uniqueCompanies)
            {
                Console.WriteLine($"\t \t \t \t \t \t \t \t {companyName}\n");
                Console.WriteLine("\t \t \t \t \t Model" + new string(' ', 28) + "\t \t Prices\n");
                Console.WriteLine("                              ---------------------------------------------------------------------------\n");

                foreach (Device device in devices)
                {
                    if (device.GetCompany().ToUpper() == companyName && device.GetDeviceType().Equals("SMART WATCH", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine($"\t \t \t \t{device.GetModel()}");
                        Console.SetCursorPosition(mX, mY);
                        Console.WriteLine(device.GetModelPrice());
                        mY++;
                    }
                }

                Console.WriteLine("                              ---------------------------------------------------------------------------\n");

                mY += 8;
            }

            Console.WriteLine("                              ---------------------------------------------------------------------------\n");
            Console.WriteLine("\nPress any key to go back...");
        }

        public static void PrintEarbuds(List<Device> devices)
        {
            Console.WriteLine("\nFollowing is the list of Earbuds with their prices.\n");
            int mX = 82;
            int mY = 24;

            var uniqueCompanies = devices
                .Where(d => d.GetDeviceType().Equals("EARBUD", StringComparison.OrdinalIgnoreCase))
                .Select(d => d.GetCompany().ToUpper())
                .Distinct();

            Console.WriteLine("\n \n");
            foreach (string companyName in uniqueCompanies)
            {
                Console.WriteLine($"\t \t \t \t \t \t \t \t {companyName}\n");
                Console.WriteLine("\t \t \t \t \t Model" + new string(' ', 28) + "\t \t Prices\n");
                Console.WriteLine("                              ---------------------------------------------------------------------------\n");

                foreach (Device device in devices)
                {
                    if (device.GetCompany().ToUpper() == companyName && device.GetDeviceType().Equals("EARBUD", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine($"\t \t \t \t{device.GetModel()}");
                        Console.SetCursorPosition(mX, mY);
                        Console.WriteLine(device.GetModelPrice());
                        mY++;
                    }
                }

                Console.WriteLine("                              ---------------------------------------------------------------------------\n");

                mY += 8;
            }

            Console.WriteLine("                              ---------------------------------------------------------------------------\n");
            Console.WriteLine("\nPress any key to go back...");
        }
        public static void PrintSHDevices(List<Device> shDevices)
        {
            int x = 56, y = 15;
            Console.WriteLine("\t \t \t SECOND HAND DEVICES \n");
            Console.WriteLine("---------------------------------------------------------------------------");
            foreach(Device device in shDevices) 
            {
                if(device.GetDeviceType() == "SECOND HAND DEVICE")
                {
                    Console.Write(device.GetModel());
                    Console.SetCursorPosition(x, y);
                    Console.WriteLine(device.GetModelPrice());
                    y++;
                }
            }
            Console.WriteLine("\nPress any key to go back...");
        }
    }
}