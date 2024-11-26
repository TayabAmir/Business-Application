using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Xml.Linq;
using System.Data;
using TechStream.BL;
using TechStream.DL.DB;
using TechStream.DL.FH;
using TechStream.DLInterfaces;
using TechStream.Utils;
using BusinessApp.UI;
using System.Data.SqlClient;

namespace BusinessApp
{
    internal class Program
    {
        static IUserDL Iuser = UserFH.GetInstance();
        static IDeviceDL Idevice = DeviceFH.GetInstance();
        static void Main(string[] args)
        {
            List<Device> devices = Idevice.LoadDeviceData();
            List<User> users = Iuser.LoadUsers();
            while (true)
            {
                ConsoleUtility.PrintHeader();
                Console.Write("\n\n\n");
                int opt;
                string input = SignUI.LoginPage();
                if (ConsoleUtility.CheckOptionValidation(input))
                {
                    opt = int.Parse(input);
                    if (opt == 1)
                    {
                        Console.Clear();
                        ConsoleUtility.PrintHeader();
                        SignUI.SignInHeader();
                        string Name, Password;

                        while (true)
                        {
                            Name = SignUI.TakeName();
                            Password = SignUI.TakePassword();
                            User user = Iuser.SignIn(Name, Password);

                            if (user != null && user.GetRole() == "ADMIN")
                            {
                                ConsoleUtility.CsPh();
                                Console.WriteLine("Signing in as an ADMIN.");
                                Thread.Sleep(1500);
                                AdminMenu(users);
                                break;
                            }
                            else if (user != null && user.GetRole() == "CUSTOMER")
                            {
                                ConsoleUtility.CsPh();
                                Console.WriteLine("Signing in as a Customer.");
                                Thread.Sleep(1500);
                                CustomerMenu(user);
                                break;
                            }
                            else if (user == null)
                            {
                                Console.WriteLine("You are not registered yet");
                                break;
                            }
                        }
                    }
                    else if (opt == 2)
                    {
                        Console.Clear();
                        ConsoleUtility.PrintHeader();
                        SignUI.SignUpHeader();
                        Iuser.SaveUserData(SignUI.SignUp(Iuser));
                    }
                    else if (opt == 3)
                    {
                        Console.Clear();
                        ConsoleUtility.PrintHeader();
                        Console.WriteLine("Thanks for coming here.");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid Option Selection.");
                        Thread.Sleep(750);
                        Console.Clear();
                        continue;
                    }

                    Console.WriteLine("\n\n\nPress any Key to go to Login Page...");
                    Console.ReadKey();
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("Invalid input. Please write valid input.");
                    Thread.Sleep(750);
                    Console.Clear();
                }
            }
            Iuser.StoreAllUsers();
        }
        static public void AdminMenu(List<User> users)
        {
            int choice, selectOption;
            List<string> feedbacks = Iuser.GetFeedbacks();
            string input, model, modelPrice, addMobile, addLaptop, addSW, addEarbud, deleteMobile, deleteMobileModel, editshDevice, deleteshDevice;
            double tempBudget = 0, addMobilePrices, editshDevicePrice;
            do
            {

                ConsoleUtility.CsPh();
                choice = AdminUI.DisplayMenu();
                ConsoleUtility.CsPh();

                if (choice == 1)
                {
                    AdminUI.RegiseteredUsers(users);
                    Console.ReadKey();
                }

                else if (choice == 2)
                {
                    List<Device> devices = Idevice.GetDevices();
                    do
                    {
                        ConsoleUtility.CsPh();
                        selectOption = AdminUI.WatchDevices();

                        if (selectOption == 1)
                        {
                            ConsoleUtility.CsPh();
                            ConsoleUtility.PrintMobiles(devices);
                            Console.ReadKey();
                        }
                        else if (selectOption == 2)
                        {
                            ConsoleUtility.CsPh();
                            ConsoleUtility.PrintLaptops(devices);
                            Console.ReadKey();
                        }
                        else if (selectOption == 3)
                        {
                            ConsoleUtility.CsPh();
                            ConsoleUtility.PrintSW(devices);
                            Console.ReadKey();
                        }
                        else if (selectOption == 4)
                        {
                            ConsoleUtility.CsPh();
                            ConsoleUtility.PrintEarbuds(devices);
                            Console.ReadKey();
                        }
                        else if (selectOption > 4)
                        {
                            Thread.Sleep(800);
                            ConsoleUtility.CsPh();
                            Console.WriteLine("\n /!\\ Write valid option please.\n\n\n");
                        }
                    } while (selectOption != 0);
                }
                else if (choice == 3)
                {
                    do
                    {
                        ConsoleUtility.CsPh();
                        selectOption = AdminUI.AddDevicesMenu();

                        if (selectOption == 1)
                        {
                            ConsoleUtility.CsPh();
                            addMobile = AdminUI.TakeCompany("Add");
                            Device addDevice = AdminUI.AddDevice("MOBILE", addMobile.ToUpper());
                            Idevice.AddDevice(addDevice);
                            Console.WriteLine("Mobile model added successfully to " + addDevice.GetCompany());

                            Console.WriteLine("\nPress any key to go back...");
                            Console.ReadKey();
                        }
                        else if (selectOption == 2)
                        {
                            ConsoleUtility.CsPh();
                            addLaptop = AdminUI.TakeCompany("Add");
                            Device addDevice = AdminUI.AddDevice("LAPTOP", addLaptop.ToUpper());
                            Idevice.AddDevice(addDevice);
                            Console.WriteLine("Laptop model added successfully to " + addDevice.GetCompany());
                            Console.WriteLine("\nPress any key to go back...");
                            Console.ReadKey();
                        }
                        else if (selectOption == 3)
                        {
                            ConsoleUtility.CsPh();
                            addSW = AdminUI.TakeCompany("Add");
                            Device addDevice = AdminUI.AddDevice("SMARTWATCH", addSW.ToUpper());
                            Idevice.AddDevice(addDevice);
                            Console.WriteLine("Smart Watch added successfully to " + addDevice.GetCompany());
                            Console.WriteLine("\nPress any key to go back...");
                            Console.ReadKey();
                        }
                        else if (selectOption == 4)
                        {
                            ConsoleUtility.CsPh();
                            addEarbud = AdminUI.TakeCompany("Add");
                            Device addDevice = AdminUI.AddDevice("EARBUDS", addEarbud.ToUpper());
                            Idevice.AddDevice(addDevice);
                            Console.WriteLine("Earbud added successfully to " + addDevice.GetCompany());
                            Console.WriteLine("\nPress any key to go back...");
                            Console.ReadKey();
                        }
                        else if (selectOption > 4)
                        {
                            Console.WriteLine("Write valid option.");
                        }
                    } while (selectOption != 0);
                }
                else if (choice == 4)
                {
                    List<Device> devices = Idevice.GetDevices();
                    do
                    {
                        ConsoleUtility.CsPh();
                        selectOption = AdminUI.EditDevicesMenu();
                        if (selectOption == 1)
                        {
                            ConsoleUtility.CsPh();
                            ConsoleUtility.PrintMobiles(devices);
                            model = AdminUI.TakeCompany("Edit");
                            modelPrice = AdminUI.TakeModel();
                            while (true)
                            {
                                input = AdminUI.TakePrice();
                                if (ConsoleUtility.CheckOptionValidation(input))
                                {
                                    addMobilePrices = double.Parse(input);
                                    break;
                                }
                                else
                                    Console.WriteLine("Write valid input\n");
                            }

                            bool updated = Idevice.EditPrice("MOBILE", model, modelPrice, addMobilePrices);

                            if (updated)
                                Console.WriteLine("Mobile price updated successfully.");
                            else
                                Console.WriteLine("No model found. Price not updated");
                            Console.WriteLine("\nPress any key to go back...");
                            Console.ReadKey();
                        }
                        else if (selectOption == 2)
                        {
                            ConsoleUtility.CsPh();
                            ConsoleUtility.PrintLaptops(devices);
                            model = AdminUI.TakeCompany("Edit");
                            modelPrice = AdminUI.TakeModel();
                            while (true)
                            {
                                input = AdminUI.TakePrice();
                                if (ConsoleUtility.CheckOptionValidation(input))
                                {
                                    addMobilePrices = double.Parse(input);
                                    break;
                                }
                                else
                                    Console.WriteLine("Write valid input\n");
                            }
                            bool updated = Idevice.EditPrice("MOBILE", model, modelPrice, addMobilePrices);

                            if (updated)
                                Console.WriteLine("Mobile price updated successfully.");
                            else
                                Console.WriteLine("No model found. Price not updated");
                            Console.WriteLine("\nPress any key to go back...");
                            Console.ReadKey();
                        }
                        else if (selectOption == 3)
                        {
                            ConsoleUtility.CsPh();
                            ConsoleUtility.PrintSW(devices);
                            model = AdminUI.TakeCompany("Edit");
                            modelPrice = AdminUI.TakeModel();
                            while (true)
                            {
                                input = AdminUI.TakePrice();
                                if (ConsoleUtility.CheckOptionValidation(input))
                                {
                                    addMobilePrices = double.Parse(input);
                                    break;
                                }
                                else
                                    Console.WriteLine("Write valid input\n");
                            }

                            bool updated = Idevice.EditPrice("MOBILE", model, modelPrice, addMobilePrices);

                            if (updated)
                                Console.WriteLine("Mobile price updated successfully.");
                            else
                                Console.WriteLine("No model found. Price not updated");
                            Console.WriteLine("\nPress any key to go back...");
                            Console.ReadKey();
                        }
                        else if (selectOption == 4)
                        {
                            ConsoleUtility.CsPh();
                            ConsoleUtility.PrintEarbuds(devices);
                            model = AdminUI.TakeCompany("Edit");
                            modelPrice = AdminUI.TakeModel();
                            while (true)
                            {
                                input = AdminUI.TakePrice();
                                if (ConsoleUtility.CheckOptionValidation(input))
                                {
                                    addMobilePrices = double.Parse(input);
                                    break;
                                }
                                else
                                    Console.WriteLine("Write valid input\n");
                            }

                            bool updated = Idevice.EditPrice("MOBILE", model, modelPrice, addMobilePrices);

                            if (updated)
                                Console.WriteLine("Mobile price updated successfully.");
                            else
                                Console.WriteLine("No model found. Price not updated");
                            Console.WriteLine("\nPress any key to go back...");
                            Console.ReadKey();
                        }
                        else if (selectOption > 4)
                        {
                            Console.WriteLine("Invalid Option");
                        }
                    } while (selectOption != 0);
                }
                else if (choice == 5)
                {
                    List<Device> devices = Idevice.GetDevices();

                    do
                    {
                        ConsoleUtility.CsPh();
                        selectOption = AdminUI.DeleteDeviceMenu();
                        if (selectOption == 1)
                        {
                            ConsoleUtility.CsPh();
                            ConsoleUtility.PrintMobiles(devices);
                            deleteMobile = AdminUI.TakeCompany("Delete");
                            deleteMobileModel = AdminUI.TakeModel().ToUpper();
                            bool Deleted = Idevice.DeleteModel(deleteMobile, deleteMobileModel);
                            if (Deleted)
                                Console.WriteLine("Model deleted successfully.");
                            else
                                Console.WriteLine("Model not found.");
                            Console.WriteLine("\nPress any key to go back...");
                            Console.ReadKey();
                        }
                        else if (selectOption == 2)
                        {
                            ConsoleUtility.CsPh();
                            ConsoleUtility.PrintLaptops(devices);
                            deleteMobile = AdminUI.TakeCompany("Delete");
                            deleteMobileModel = AdminUI.TakeModel().ToUpper();
                            bool Deleted = Idevice.DeleteModel(deleteMobile, deleteMobileModel);
                            if (Deleted)
                                Console.WriteLine("Model deleted successfully.");
                            else
                                Console.WriteLine("Model not found.");
                            Console.WriteLine("\nPress any key to go back...");
                            Console.ReadKey();
                        }
                        else if (selectOption == 3)
                        {
                            ConsoleUtility.CsPh();
                            ConsoleUtility.PrintSW(devices);
                            deleteMobile = AdminUI.TakeCompany("Delete");
                            deleteMobileModel = AdminUI.TakeModel().ToUpper();
                            bool Deleted = Idevice.DeleteModel(deleteMobile, deleteMobileModel);
                            if (Deleted)
                                Console.WriteLine("Model deleted successfully.");
                            else
                                Console.WriteLine("Model not found.");
                            Console.WriteLine("\nPress any key to go back...");
                            Console.ReadKey();
                        }
                        else if (selectOption == 4)
                        {
                            ConsoleUtility.CsPh();
                            ConsoleUtility.PrintEarbuds(devices);
                            deleteMobile = AdminUI.TakeCompany("Delete");
                            deleteMobileModel = AdminUI.TakeModel().ToUpper();
                            bool Deleted = Idevice.DeleteModel(deleteMobile, deleteMobileModel);
                            if (Deleted)
                                Console.WriteLine("Model deleted successfully.");
                            else
                                Console.WriteLine("Model not found.");
                            Console.WriteLine("\nPress any key to go back...");
                            Console.ReadKey();
                        }
                        else if (selectOption > 4)
                        {
                            Console.WriteLine("Invalid Option");
                        }
                    } while (selectOption != 0);
                }
                else if (choice == 6)
                {
                    List<Device> devices = Idevice.GetDevices();

                    do
                    {
                        ConsoleUtility.CsPh();
                        selectOption = AdminUI.SHMenu();

                        if (selectOption == 1)
                        {
                            ConsoleUtility.CsPh();
                            ConsoleUtility.PrintSHDevices(devices);
                            Console.ReadKey();
                        }
                        else if (selectOption == 2)
                        {
                            ConsoleUtility.CsPh();
                            Device device = AdminUI.AddDevice("SECOND HAND DEVICE", null);
                            Idevice.AddDevice(device);
                            Console.ReadKey();
                        }
                        else if (selectOption == 3)
                        {
                            ConsoleUtility.CsPh();
                            ConsoleUtility.PrintSHDevices(devices);
                            editshDevice = AdminUI.TakeModel().ToUpper();
                            while (true)
                            {
                                input = AdminUI.TakePrice();
                                if (ConsoleUtility.CheckOptionValidation(input))
                                {
                                    editshDevicePrice = int.Parse(input);
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Write valid input\n");
                                }
                            }
                            bool Edit = Idevice.EditPrice("SECOND HAND DEVICE", null, editshDevice, editshDevicePrice);
                            if (Edit)
                                Console.WriteLine("Model Price Updated successfully");
                            else
                                Console.WriteLine("Model not found");

                            Console.WriteLine("\nPress any key to go back...");
                            Console.ReadKey();
                        }
                        else if (selectOption == 4)
                        {
                            ConsoleUtility.CsPh();
                            deleteshDevice = AdminUI.TakeModel().ToUpper();
                            if (Idevice.DeleteModel(null, deleteshDevice))
                                Console.WriteLine("Model Deleted Successfully");
                            else
                                Console.WriteLine("Model not found");
                            Console.WriteLine("\nPress any key to go back...");
                            Console.ReadKey();
                        }
                        else if (selectOption > 4)
                        {
                            Console.WriteLine("Enter valid option.");
                        }
                    } while (selectOption != 0);
                }
                else if (choice == 7)
                {
                    AdminUI.ShowSales(tempBudget);
                    Console.ReadKey();
                }
                else if (choice == 8)
                {
                    List<string> customers = Iuser.GetCurrentCustomerNames();
                    AdminUI.ShowFeedbacks(customers, feedbacks);
                    Console.ReadKey();
                }
                else if (choice == 0)
                    break;
                else
                {
                    Console.WriteLine("Invalid choice. Please enter a valid option.");
                    Thread.Sleep(800);
                }

            } while (choice != 0);
        }
        static public void CustomerMenu(User customer)
        {
            int choice = 1;
            string buyDevice, showDevice, buyModel, buySHDevice;
            double shPrice, budget = 0, tempBudget = 0;
            List<Device> devices = Idevice.GetDevices();
            Customer user = (Customer)customer;

            while (choice != 0)
            {
                ConsoleUtility.CsPh();
                choice = CustomerUI.DisplayMenu();
                ConsoleUtility.CsPh();
                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        ConsoleUtility.PrintHeader();
                        CustomerUI.WatchHeader();

                        showDevice = CustomerUI.TakeDevice().ToUpper();

                        if (showDevice == "MOBILE")
                        {
                            ConsoleUtility.PrintMobiles(devices);
                        }
                        else if (showDevice == "LAPTOP")
                        {
                            ConsoleUtility.PrintLaptops(devices);
                        }
                        else if (showDevice == "SMART WATCH")
                        {
                            ConsoleUtility.PrintSW(devices);
                        }
                        else if (showDevice == "EARBUDS")
                        {
                            ConsoleUtility.PrintEarbuds(devices);
                        }
                        else
                        {
                            Console.WriteLine("Invalid Device Name.");
                        }
                        break;
                    case 2:
                        CustomerUI.AddMoney(user, ref budget, Iuser);
                        break;

                    case 3:
                        if (Iuser.AccountMoney(user) == 0)
                        {
                            Console.WriteLine("Please first Enter the Amount by going to Option 2.");
                        }
                        else
                        {
                            buyDevice = CustomerUI.TakeDevice().ToUpper();

                            if (buyDevice == "MOBILE")
                            {
                                Console.WriteLine();
                                CustomerUI.PrintMobiles(devices, Iuser.AccountMoney(user));
                            }
                            else if (buyDevice == "LAPTOP")
                            {
                                Console.WriteLine();
                                CustomerUI.PrintLaptops(devices, Iuser.AccountMoney(user));
                            }
                            else if (buyDevice == "SMART WATCH")
                            {
                                Console.WriteLine();
                                CustomerUI.PrintSW(devices, Iuser.AccountMoney(user));
                            }
                            else if (buyDevice == "EARBUDS")
                            {
                                Console.WriteLine();
                                CustomerUI.PrintEarbuds(devices, Iuser.AccountMoney(user));
                            }
                            else
                            {
                                Console.WriteLine("Wrong device Name.");
                            }
                        }
                        break;

                    case 4:
                        ConsoleUtility.PrintSHDevices(devices);
                        break;

                    case 5:
                        if (Iuser.AccountMoney(user) == 0)
                        {
                            Console.WriteLine("Please first Enter the Amount by going to Option 2.");
                        }
                        else
                        {
                            buyDevice = CustomerUI.TakeDevice().ToUpper();

                            if (buyDevice == "MOBILE" || buyDevice == "MOBILES")
                            {
                                CustomerUI.PrintMobiles(devices, Iuser.AccountMoney(user));
                                Console.WriteLine();
                            }
                            else if (buyDevice == "LAPTOP" || buyDevice == "LAPTOPS")
                            {
                                CustomerUI.PrintLaptops(devices, Iuser.AccountMoney(user));
                                Console.WriteLine();
                            }
                            else if (buyDevice == "SMART WATCH" || buyDevice == "SMARTWATCH")
                            {
                                CustomerUI.PrintSW(devices, Iuser.AccountMoney(user));
                                Console.WriteLine();
                            }
                            else if (buyDevice == "EARBUDS" || buyDevice == "EARBUD")
                            {
                                CustomerUI.PrintEarbuds(devices, Iuser.AccountMoney(user));
                                Console.WriteLine();
                            }
                            else
                            {
                                Console.WriteLine("This device ain't available.");
                            }

                            Console.WriteLine();
                            Console.WriteLine("If no device available Enter 'Return' to go back.");

                            while (true)
                            {
                                Console.WriteLine("Enter the name of model: ");
                                buyModel = Console.ReadLine().ToUpper();

                                if (Idevice.ModelExisted(buyModel))
                                {
                                    double devicePrice = Idevice.GetDevicePrice(buyModel);

                                    if (devicePrice > 0 && devicePrice <= Iuser.AccountMoney(user))
                                    {
                                        Device device = Idevice.GetDeviceByName(buyModel);
                                        Console.WriteLine("The selected device " + buyDevice + " (" + buyModel + ") is within your budget and is bought successfully.");
                                        Console.WriteLine("Price: " + devicePrice);
                                        tempBudget += devicePrice;
                                        user.SetAccountMoney(Iuser.AccountMoney(user) - devicePrice);
                                        Iuser.SaveUserDevice(user, device);
                                        Idevice.DeviceSold(user.GetName(), device.GetModel(), device.GetModelPrice());
                                        break;
                                    }
                                    else if (devicePrice > Iuser.AccountMoney(user))
                                    {
                                        Console.WriteLine("The selected device " + buyDevice + " (" + buyModel + ")  is out of your budget.");
                                        break;
                                    }
                                }
                                else if (buyModel == "RETURN")
                                {
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Wrong Model Name");
                                }
                            }
                        }
                        break;

                    case 6:
                        if (Iuser.AccountMoney(user) == 0)
                        {
                            Console.WriteLine("Please first Enter the Amount by going to Option 2.");
                        }
                        else
                        {
                            ConsoleUtility.PrintSHDevices(devices);
                            Console.WriteLine();
                            Console.WriteLine();
                            Console.WriteLine("Enter the name of device you want to buy: ");
                            buySHDevice = Console.ReadLine().ToUpper();
                            shPrice = Idevice.GetDevicePrice(buySHDevice);

                            if (shPrice > 0 && shPrice <= Iuser.AccountMoney(user))
                            {
                                Device device = Idevice.GetDeviceByName(buySHDevice);
                                Console.WriteLine("The selected device " + buySHDevice + " is within your budget and is bought successfully.");
                                Console.WriteLine("Price: " + shPrice);
                                tempBudget += shPrice;
                                user.SetAccountMoney(Iuser.AccountMoney(user) - shPrice);
                                Iuser.SaveUserDevice(user, device);
                                Idevice.DeviceSold(user.GetName(), device.GetModel(), device.GetModelPrice());
                            }
                            else if (shPrice > Iuser.AccountMoney(user))
                            {
                                Console.WriteLine("The selected device " + buySHDevice + " is out of your budget.");
                            }
                            else if (shPrice == 0)
                            {
                                Console.WriteLine("Invalid selection. Please check your input.");
                            }
                        }
                        break;
                    case 7:
                        CustomerUI.Bill(tempBudget, Iuser.AccountMoney(user));
                        break;
                    case 8:
                        Console.WriteLine("\n");
                        Iuser.AddFeedback(user, CustomerUI.TakeFeedback());
                        break;
                    case 9:
                        List<Device> devicesBought = Iuser.GetUserDevices(user);
                        CustomerUI.ShowDevices(devicesBought);
                        break;
                    case 0:
                        Console.WriteLine(" ");
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please enter a valid option.");
                        break;
                }
                if (choice != 0)
                {
                    Console.WriteLine("\n\nPress any key to go back to Menu.... ");
                    Console.ReadKey();
                }

            }
        }

    }
}