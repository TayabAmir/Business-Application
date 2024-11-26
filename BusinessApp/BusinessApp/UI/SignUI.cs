using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TechStream.BL;
using TechStream.DLInterfaces;
using TechStream.Utils;
namespace BusinessApp.UI
{
    internal class SignUI
    {

        public static string LoginPage()
        {
            Console.WriteLine("\t \t \t \t \t \tLOGIN PAGE ");
            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine(" 1. Sign In with your credentials.");
            Console.WriteLine(" 2. Sign Up your credentials");
            Console.WriteLine(" 3. Exit");
            Console.WriteLine("Enter the option...");
            return Console.ReadLine();
        }
        public static void SignInHeader()
        {
            Console.WriteLine("\t \t \t \t \t \t SIGN IN PAGE ");
            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------");
        }
        public static void SignUpHeader()
        {
            Console.WriteLine("\t \t \t \t \t \t SIGN UP PAGE");
            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Instructions for Sign Up => \n Username must be at least 6 characters long and must not contain any special character and contain at least 3 letters \n Password must be at least 8 characters long\n");
        }
        public static string TakeName()
        {
            Console.Write("Enter UserName: ");
            return Console.ReadLine();
        }

        public static string TakePassword()
        {
            Console.Write("Enter Password: ");
            return Console.ReadLine();
        }
        private static bool CheckName(int num)
        {
            if (num == 0)
            {
                Console.WriteLine("Username already taken. Please choose another.");
                return false;
            }
            if (num == 1)
            {
                Console.WriteLine("Invalid character in username. Use only letters and numbers.");
                return false;
            }
            if (num == 2)
            {
                return true;
            }
            if (num == 3)
            {
                Console.WriteLine("Invalid username. Username must contain at least 3 letters.");
                return false;
            }
            if (num == 4)
            {
                Console.WriteLine("Invalid username. Username must be at least 6 characters long.");
                return false;
            }
            return true;
        }
        public static User SignUp(IUserDL user)
        {
            string mName, mPassword, mRole, mAccount;
            while (true)
            {
                Console.Write("Enter Username: ");
                mName = Console.ReadLine();
                if (CheckName(user.CheckUserName(mName)))
                {
                    break;
                }
            }

            while (true)
            {
                Console.Write("Enter Password: ");
                mPassword = Console.ReadLine();
                if (mPassword.Length < 8)
                {
                    Console.WriteLine("Invalid input. Password does not contain 8 characters.");
                    continue;
                }
                break;
            }

            while (true)
            {
                Console.Write("Enter your role (Admin/Customer): ");
                mRole = Console.ReadLine().ToUpper();
                if (mRole != "ADMIN" && mRole != "CUSTOMER")
                {
                    Console.WriteLine("\nInvalid Role. Please select a valid role.");
                    continue;
                }
                break;
            }

            if (mRole == "CUSTOMER")
            {
                while (true)
                {
                    Console.Write("Enter your Account No: ");
                    mAccount = Console.ReadLine();
                    if (mAccount.Length == 13 && ConsoleUtility.CheckOptionValidation(mAccount) && !user.AccountExisted(mAccount))
                    {
                        Customer newObj = new Customer(mName, mPassword, mRole, mAccount);
                        Console.WriteLine("\nYou have successfully registered your credentials.");
                        return newObj;
                    }
                    else
                    {
                        Console.WriteLine("\nInvalid Account Number / Account Number must be 13 digits.");
                    }
                }
            }
            else if (mRole == "ADMIN")
            {
                Admin newObj = new Admin(mName, mPassword, mRole);
                Console.WriteLine("\nYou have successfully registered your credentials.");
                return newObj;
            }
            return null;
        }
    }
}
