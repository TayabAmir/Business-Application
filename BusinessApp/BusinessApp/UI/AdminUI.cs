using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TechStream.BL;
namespace BusinessApp.UI
{
    internal class AdminUI
    {
        public static int WatchDevices()
        {
            Console.WriteLine("Enter 1 to watch Mobiles List");
            Console.WriteLine("Enter 2 to watch Laptops List");
            Console.WriteLine("Enter 3 to watch Smart Watches List");
            Console.WriteLine("Enter 4 to watch Wireless Earbuds List");
            Console.WriteLine("Enter 0 to go back to admin menu\n");
            return ConsoleUtility.ValidOption();

        }
        public static int AddDevicesMenu()
        {
            Console.WriteLine("\n\nEnter 1 to add new Mobile Model");
            Console.WriteLine("Enter 2 to add new Laptops Model");
            Console.WriteLine("Enter 3 to add new Smart Watches Model");
            Console.WriteLine("Enter 4 to add new Wireless Earbuds Model");
            Console.WriteLine("Enter 0 to go back to admin menu\n");

            return ConsoleUtility.ValidOption();

        }

        public static int EditDevicesMenu()
        {
            Console.WriteLine("Enter 1 to edit Mobile Price");
            Console.WriteLine("Enter 2 to edit Laptop Price");
            Console.WriteLine("Enter 3 to edit Smart Watch Price");
            Console.WriteLine("Enter 4 to edit Earbuds Price");
            Console.WriteLine("Enter 0 to return to Admin Menu");
            return ConsoleUtility.ValidOption();
        }
        public static string TakeCompany(string input)
        {
                Console.WriteLine("Enter the name of company in which you want to " + input + " model...");
                return Console.ReadLine().ToUpper(); ;
        }
        public static string TakeModel()
        {
            Console.Write("Enter the model name: ");
            return Console.ReadLine().ToUpper();
        }

        public static string TakePrice()
        {
            Console.Write("Enter the new price: ");
            return Console.ReadLine();
        }
        public static Device AddDevice(string type,string company)
        {
            string addMobileModel,input;
            double addMobilePrices;
            while (true)
            {
                Console.WriteLine("Enter the name of model you want to add...");
                addMobileModel = Console.ReadLine().ToUpper();
                if (!string.IsNullOrEmpty(addMobileModel))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid model name");
                }
            }
            while (true)
            {
                Console.WriteLine("Enter the price for this model...");
                input = Console.ReadLine();
                if (ConsoleUtility.CheckOptionValidation(input))
                {
                    addMobilePrices = double.Parse(input);
                    break;
                }
                else
                {
                    Console.WriteLine("Write valid input\n");
                }
            }
            return new Device(type,company,addMobileModel, addMobilePrices);
        }

        public static void ShowSales(double sales)
        {
            Console.WriteLine("Your Sales: " + sales);
            Console.WriteLine("\nPress any key to go back to Menu.... ");
        }
        public static int DisplayMenu()
        {

            Console.WriteLine("\t \t \t \t \t \t ADMIN MENU ");
            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Enter 1 to check registered users and admins.");
            Console.WriteLine("Enter 2 to watch devices.");
            Console.WriteLine("Enter 3 to add Device Model.");
            Console.WriteLine("Enter 4 to edit Device Price.");
            Console.WriteLine("Enter 5 to delete Device.");
            Console.WriteLine("Enter 6 to handle second hand devices.");
            Console.WriteLine("Enter 7 to view your sales");
            Console.WriteLine("Enter 8 to see Feedbacks");
            Console.WriteLine("Enter 9 to change Theme ");
            Console.WriteLine("Enter 0 to escape the matrix");
            return ConsoleUtility.ValidOption();

        }
        public static int DeleteDeviceMenu()
        {
            Console.WriteLine("Enter 1 to Delete Mobile");
            Console.WriteLine("Enter 2 to Delete Laptops");
            Console.WriteLine("Enter 3 to Delete Smart Watches");
            Console.WriteLine("Enter 4 to Delete Wireless Earbuds");
            Console.WriteLine("Enter 0 to return to Admin Menu");
            return ConsoleUtility.ValidOption();

        }
        public static int SHMenu()
        {
            Console.WriteLine("Enter 1 to watch Second Hand Devices ");
            Console.WriteLine("Enter 2 to add Second Hand Devices ");
            Console.WriteLine("Enter 3 to edit Second Hand Device Price");
            Console.WriteLine("Enter 4 to delete Second Hand Device");
            Console.WriteLine("Enter 0 to return to Admin Menu");
            
            return ConsoleUtility.ValidOption();
        }
        public static void RegiseteredUsers(List<User> users)
        {
            int x = 35, y = 18;
            Console.WriteLine("\t Registered Admins and Customers are: ");
            Console.WriteLine("\t Username \t \t Status ");
            Console.WriteLine("--------------------------------------------------------------");

            Console.SetCursorPosition(7, 18);
            for (int i = 0; i < users.Count; i++)
            {
                Console.Write("\t" + users[i].GetName());
                Console.SetCursorPosition(x, y);
                Console.WriteLine(users[i].GetRole());
                y++;
            }

            Console.WriteLine("\nPress any key to go back to Menu....");
        }
        public static void ShowFeedbacks(List<string> customers,List<string> feedbacks)
        {
            Console.WriteLine("                                       FEEDBACKS              ");
            Console.WriteLine("=======================================================================================================");
            Console.WriteLine();
            if (feedbacks.Count == 0)
            {
                Console.SetCursorPosition(35, 12);
                Console.WriteLine("No Feedbacks Till Now.");
            }
            else
            {

                for (int i = 0; i < feedbacks.Count; i++)
                {
                    Console.WriteLine($"Feedback by {customers[i]}: {feedbacks[i]}");
                }
            }
            Console.WriteLine("\nPress any key to go back to Menu.... ");

        }
    }
}
