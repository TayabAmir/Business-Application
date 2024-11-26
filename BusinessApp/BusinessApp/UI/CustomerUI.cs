using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TechStream.BL;
using TechStream.DLInterfaces;

namespace BusinessApp.UI
{
    internal class CustomerUI
    {
        public static int DisplayMenu()
        {
            Console.WriteLine("\t \t \t \t \t \t CUSTOMER MENU \n");
            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------\n");
            Console.WriteLine("\n\n\n");
            Console.WriteLine("Enter 1 to Watch the Device you want (Mobile / Laptop / Smart Watch/ Earbuds)");
            Console.WriteLine("Enter 2 to Add money in your account");
            Console.WriteLine("Enter 3 to Watch the Device (Mobile / Laptop / Smart Watch/ Earbuds) within your budget");
            Console.WriteLine("Enter 4 to Watch Second Hand Devices");
            Console.WriteLine("Enter 5 to Select the Device you Wanna Buy ");
            Console.WriteLine("Enter 6 to Select the Second Hand Device you Want to Buy ");
            Console.WriteLine("Enter 7 to see your bill and remaining amount.");
            Console.WriteLine("Enter 8 to give your feedback");
            Console.WriteLine("Enter 9 to see the devices you buy");
            Console.WriteLine("Enter 0 to escape the matrix\n");

            return ConsoleUtility.ValidOption();
        }

        public static string TakeDevice()
        {
            Console.Write("Enter the name of device (Mobile / Laptop / Smart Watch/ Earbuds): ");
            return Console.ReadLine();
        }

        public static void AddMoney(Customer user,ref double budget,IUserDL user1)
        {
            string aNo, input;
            while (true)
            {
                Console.Write("Enter your Account Number: ");
                aNo = Console.ReadLine();
                if (user1.CheckAccount(aNo, user.GetAccountNo()) && aNo.Length == 13)
                {
                    while (true)
                    {
                        Console.Write("Enter the amount of money you want to add: ");
                        input = Console.ReadLine();
                        if (ConsoleUtility.CheckOptionValidation(input))
                        {
                            budget = double.Parse(input);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Write valid input\n\n");
                        }
                    }
                    if (user1.UpdateMoney(user, user.GetAccountMoney() + budget))
                    {
                        Console.WriteLine($"\n\nYour amount of {budget} has been updated.");
                        budget = user.GetAccountMoney();
                    }   else
                        Console.WriteLine($"Amount cannot be upgraded to greater than 400k");
                    break;
                }
                else
                {
                    Console.WriteLine("Wrong Account Number.");
                }
            }
        }
        public static string TakeFeedback()
        {
            while (true)
            {
                Console.Write("Enter your feedback: ");
                string feed = Console.ReadLine();
                if (string.IsNullOrEmpty(feed))
                {
                    Console.WriteLine("Feedback cannot be empty. Please Enter correct feedback.");
                }
                else
                {
                    Console.WriteLine("Thanks for your Feedback.");
                    return feed;
                }
            }
        }
        public static void ShowDevices(List<Device> devices)
        {
            Console.Write(" Model  \t \t \t Price \n \n");
            foreach(Device device in devices)
            {
                Console.WriteLine($"{device.GetModel()} \t \t \t {device.GetModelPrice()}");
            }
        }
        public static void Bill(double paid, double remaining)
        {
            Console.WriteLine(" \t \t \t BILL");
            Console.WriteLine("-------------------------------------------------------------------------\n");
            Console.WriteLine($"Your Initial Amount: ${paid + remaining}");
            Console.WriteLine($"Your Paid Amount: ${paid}");
            Console.WriteLine($"Your Remaining Amount: ${remaining}");
        }
        public static void WatchHeader()
        {
            Console.WriteLine("\t \t \t \t \t \t CUSTOMER MENU ");
            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------\n\n\n");
        }
        public static void PrintMobiles(List<Device> Devices,double Range)
        {
            Console.WriteLine("Following is the list of Mobiles with their prices.\n");

            int mX = 81;
            int mY = 26;

            var uniqueCompanies = Devices.Where(d => d.GetDeviceType().Equals("MOBILE", StringComparison.OrdinalIgnoreCase)).Select(d => d.GetCompany().ToUpper()).Distinct();

            foreach (string companyName in uniqueCompanies)
            {
                Console.WriteLine($"\t \t \t \t \t \t \t \t {companyName}\n");
                Console.WriteLine("\t \t \t \t \t Model" + new string(' ', 28) + "\t \t Prices\n");
                Console.WriteLine("                              ---------------------------------------------------------------------------\n");

                foreach (Device device in Devices)
                {
                    if (device.GetCompany().ToUpper() == companyName && device.GetModelPrice()<=Range)
                    {
                        Console.WriteLine($"\t \t \t \t{device.GetModel()}");
                        Console.SetCursorPosition(mX, mY);
                        Console.WriteLine(device.GetModelPrice());
                        mY++;
                    }
                }

                Console.WriteLine("                              ---------------------------------------------------------------------------\n");

                mY += 8;}
        }

        public static void PrintLaptops(List<Device> devices, double Range)
        {
            Console.WriteLine("Following is the list of Laptops with their prices.\n");
            int mX = 81;
            int mY = 26;

            var uniqueCompanies = devices
                .Where(d => d.GetDeviceType().Equals("LAPTOP", StringComparison.OrdinalIgnoreCase))
                .Select(d => d.GetCompany().ToUpper())
                .Distinct();

            foreach (string companyName in uniqueCompanies)
            {
                Console.WriteLine($"\t \t \t \t \t \t \t \t {companyName}\n");
                Console.WriteLine("\t \t \t \t \t Model" + new string(' ', 28) + "\t \t Prices\n");
                Console.WriteLine("                              ---------------------------------------------------------------------------\n");

                foreach (Device device in devices)
                {
                    if (device.GetCompany().ToUpper() == companyName && device.GetModelPrice() <= Range)
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

        public static void PrintSW(List<Device> devices, double Range)
        {
            Console.WriteLine("Following is the list of Smart Watches with their prices.\n");
            int mX = 90;
            int mY = 26;

            var uniqueCompanies = devices
                .Where(d => d.GetDeviceType().Equals("SMART WATCH", StringComparison.OrdinalIgnoreCase))
                .Select(d => d.GetCompany().ToUpper())
                .Distinct();

            foreach (string companyName in uniqueCompanies)
            {
                Console.WriteLine($"\t \t \t \t \t \t \t \t {companyName}\n");
                Console.WriteLine("\t \t \t \t \t Model" + new string(' ', 28) + "\t \t Prices\n");
                Console.WriteLine("                              ---------------------------------------------------------------------------\n");

                foreach (Device device in devices)
                {
                    if (device.GetCompany().ToUpper() == companyName && device.GetModelPrice() <= Range)
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

        public static void PrintEarbuds(List<Device> devices, double Range)
        {
            Console.WriteLine("Following is the list of Earbuds with their prices.\n");
            int mX = 82;
            int mY = 26;

            var uniqueCompanies = devices
                .Where(d => d.GetDeviceType().Equals("EARBUD", StringComparison.OrdinalIgnoreCase))
                .Select(d => d.GetCompany().ToUpper())
                .Distinct();

            foreach (string companyName in uniqueCompanies)
            {
                Console.WriteLine($"\t \t \t \t \t \t \t \t {companyName}\n");
                Console.WriteLine("\t \t \t \t \t Model" + new string(' ', 28) + "\t \t Prices\n");
                Console.WriteLine("                              ---------------------------------------------------------------------------\n");

                foreach (Device device in devices)
                {
                    if (device.GetCompany().ToUpper() == companyName && device.GetModelPrice() <= Range)
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
    }
}
