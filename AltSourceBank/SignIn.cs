using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AltSourceBankLibrary;

namespace AltSourceBank
{
    public class SignIn
    {
        public static Login RunLogin(Login login)
        {
            do
            {
                login = Login(login);
            }
            while (login.UserName == "" || login.UserName == null);
            return login;
        }
        public static Login Login(Login login)
        {
            if (login.VerifyAnyUsers())
            {
                Console.WriteLine("Please enter your username and press enter(For new users, please type \"/c\" and press enter): ");
                string userName = Console.ReadLine();
                if (userName != "")
                {
                    if (userName.Trim('"').ToUpper() == "/C")
                    {
                        login = CreateNewUser(login);
                        Console.WriteLine("User successfully created!");
                        Program.MainMenu();
                    }
                    else
                    {
                        if (login.VerifyUserName(userName))
                        {
                            Console.WriteLine("Please enter your password and press enter: ");
                            string password = Console.ReadLine();
                            if (login.VerifyPassword(userName, password))
                            {
                                login.UserName = userName;
                                login.Password = password;
                                Console.WriteLine("Login Successful!");
                                Program.MainMenu();
                            }
                            else
                            {
                                Console.WriteLine("Oops, looks like your password doesn't match(Case sensitive). Please try again.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No user found for " + userName + ". Please try again or create a new account.");
                        }

                    }
                }
            }
            else
            {
                Console.WriteLine("No users found, lets create a new account for you.");
                login = CreateNewUser(login);
                Program.MainMenu();
            }
            return login;
        }
        public static Login CreateNewUser(Login login)
        {
            do
            {
                Console.WriteLine("Please enter your email address and press enter");
                string userName = Console.ReadLine();
                if (login.VerifyEmail(userName))
                {

                    if (login.VerifyNotDuplicateUser(userName))
                        login.UserName = userName;
                    else
                    {
                        Console.WriteLine("Oops, that user already exists. Please try again");
                        RunLogin(login);
                    }
                }
                else
                    Console.WriteLine("Oops, your email is not valid. Please try again.");
            } while (login.UserName == "" || login.UserName == null);
            do
            {
                Console.WriteLine("Please create a password for your account and press enter");
                string password = Console.ReadLine();
                if (password.Length > 0)
                    login.Password = password;
                else
                    Console.WriteLine("You must enter a value for your password, please try again");
            } while (login.Password == "" || login.Password == null);
            login.CreateUser(login);
            return login;
        }
    }
}
