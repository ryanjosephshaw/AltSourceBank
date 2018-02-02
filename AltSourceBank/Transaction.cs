using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AltSourceBankLibrary;

namespace AltSourceBank
{
    public class Transaction
    {
        public static void Withdrawal(Login login)
        {
            string input = "";
            decimal amount = 0;
            do
            {
                Console.WriteLine("Please enter the amount you would like to withdraw and press enter. To go back to the main menu type \"/m\" and press enter");
                input = Console.ReadLine();
                if (input.Trim('"').ToUpper() != "/M" && !decimal.TryParse(input, out amount))
                {
                    Console.WriteLine(input + " is not a valid entry. Please try again");
                }
            } while (input.Trim('"').ToUpper() != "/M" && !decimal.TryParse(input, out amount));
            if (input.Trim('"').ToUpper() == "/M")
            {
                Program.MainMenu();
            }
            else
            {
                Transactions tran = new Transactions();
                tran.User = login.UserName;
                tran.Date = DateTime.Now;
                tran.Amount = amount;
                tran.Type = TransType.WITHDRAWAL;
                Console.WriteLine(tran.MakeTransaction(tran));
                Program.MainMenu();
            }
        }

        public static void Deposit(Login login)
        {
            string input = "";
            decimal amount = 0;
            do
            {
                Console.WriteLine("Please enter the amount you would like to deposit and press enter. To go back to the main menu type \"/m\" and press enter");
                input = Console.ReadLine();
                if (input.Trim('"').ToUpper() != "/M" && !decimal.TryParse(input, out amount))
                {
                    Console.WriteLine(input + " is not a valid entry. Please try again");
                }
            } while (input.Trim('"').ToUpper() != "/M" && !decimal.TryParse(input, out amount));
            if (input.Trim('"').ToUpper() == "/M")
            {
                Program.MainMenu();
            }
            else
            {
                Transactions tran = new Transactions();
                tran.User = login.UserName;
                tran.Date = DateTime.Now;
                tran.Amount = amount;
                tran.Type = TransType.DEPOSIT;
                Console.WriteLine(tran.MakeTransaction(tran));
                Program.MainMenu();
            }
        }

        public static void CheckBalance(Login login)
        {
            Transactions tran = new Transactions();
            Console.WriteLine("Your available balance is $" + tran.GetBalance(login.UserName).ToString());
            Program.MainMenu();

        }
        public static void TransactionHistory(Login login)
        {
            Transactions tran = new Transactions();
            List<Transactions> trans = tran.GetTransactionHistory(login.UserName);
            Console.WriteLine("Date, Amount, Type");
            foreach(Transactions t in trans)
            {
                Console.WriteLine(t.Date.ToString() + ", " + t.Amount.ToString() + ", " + t.Type.ToString());
            }
            Program.MainMenu();
        }
    }
}
