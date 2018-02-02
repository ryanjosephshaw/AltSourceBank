using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace AltSourceBankLibrary
{
    public class Login
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        private string dataFile = Directory.GetCurrentDirectory() + "\\users.csv";

        public void CreateDataFile()
        {
            try
            {
                if (!File.Exists(dataFile))
                    using (File.Create(dataFile)) { }
            } catch(Exception ex)
            {
                dataFile = AppDomain.CurrentDomain.GetData("DataDirectory").ToString() + "\\users.csv";
                if (!File.Exists(dataFile))
                {
                    using (File.Create(dataFile)) { }
                }
            }
        }
        public List<Login> MapFileToLogins()
        {
            CreateDataFile();
            List<List<string>> fileData = CSV.GetCSVData(dataFile);
            List<Login> logins = new List<Login>();
            foreach(List<string> line in fileData)
            {
                Login login = new Login();
                login.UserName = line[0];
                login.Password = line[1];
                logins.Add(login);
            }
            return logins;
        }

        public bool VerifyUserName(string userName)
        {
            CreateDataFile();
            List<Login> logins = MapFileToLogins();
            return logins.Any(x => x.UserName.ToUpper() == userName.ToUpper());
        }
        public bool VerifyPassword(string userName, string password)
        {
            CreateDataFile();
            List<Login> logins = MapFileToLogins();
            string userPass = logins.Where(x => x.UserName.ToUpper() == userName.ToUpper()).FirstOrDefault().Password;

            byte[] hashBytes = Convert.FromBase64String(userPass);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            for (int i = 0; i < 20; i++)
                if (hashBytes[i + 16] != hash[i])
                    return false;
            return true;
        }
        public bool VerifyAnyUsers()
        {
            if (!File.Exists(dataFile))
            {
                CreateDataFile();
                return false;
            }
            else
            {
                string[] userInfo = File.ReadAllLines(dataFile);
                if (userInfo.Length > 0)
                    return true;
                else
                    return false;
            }
        }
        public bool VerifyEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        public bool VerifyNotDuplicateUser(string email)
        {
            CreateDataFile();
            List<Login> logins = MapFileToLogins();
            if (logins.Any(x => x.UserName.ToUpper() == email.ToUpper()))
                return false;
            else
                return true;
        }
        public void CreateUser(Login login)
        {
            CreateDataFile();
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(login.Password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            login.Password = Convert.ToBase64String(hashBytes);
            string line = "\"" + login.UserName + "\",\"" + login.Password + "\"" + Environment.NewLine;
            File.AppendAllText(dataFile, line);
        }
    }
}
