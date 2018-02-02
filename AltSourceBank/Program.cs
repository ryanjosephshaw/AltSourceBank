using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AltSourceBankLibrary;
using System.Runtime.Caching;

namespace AltSourceBank
{
    class Program
    {
        public static Login login = new Login();
        static void Main(string[] args)
        {
            login = new Login();
            login = SignIn.RunLogin(login);
        }
        public static void MainMenu()
        {
            string option = "";
            int optionNum = 0;
            do
            {
                Console.WriteLine("Please make a selection from the following items(Type the number associated with the option and press enter): ");
                Console.WriteLine("1 - Record a deposit");
                Console.WriteLine("2 - Record a withdrawal");
                Console.WriteLine("3 - Check balance");
                Console.WriteLine("4 - See transaaction history");
                Console.WriteLine("5 - Logout");
                option = Console.ReadLine();
                if (!int.TryParse(option, out optionNum))
                {
                    Console.WriteLine("Oops, you did not type a number value. Please make a selection from the menu and type the number and press enter.");
                }
            } while (!int.TryParse(option, out optionNum));
            //Work on logout next. 
            RunParts(optionNum);
        }
        public static void RunParts(int option)
        {
            switch (option)
            {
                case 1:
                    Transaction.Deposit(login);
                    break;
                case 2:
                    Transaction.Withdrawal(login);
                    break;
                case 3:
                    Transaction.CheckBalance(login);
                    break;
                case 4:
                    Transaction.TransactionHistory(login);
                    break;
                case 5:
                    login = new Login();
                    Console.WriteLine("Successfully logged out! You can sign in again if you want.");
                    SignIn.RunLogin(login);
                    break;
                default:
                    Console.WriteLine("Oops, looks like you made an invalid selection");
                    MainMenu();
                    break;
            }
        }
    }
}
