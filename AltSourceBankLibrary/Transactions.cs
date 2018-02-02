using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AltSourceBankLibrary
{
    public enum TransType
    {
        DEPOSIT, WITHDRAWAL
    }
    public class Transactions
    {
        public string User { get; set; }
        public decimal Amount { get; set; }
        public TransType Type { get; set; }
        public DateTime Date { get; set; }
        private static string dataFile = Directory.GetCurrentDirectory() + "\\transactions.csv";

        public void CreateDataFile()
        {
            try
            {
                if (!File.Exists(dataFile))
                    using (File.Create(dataFile)) { }
            }
            catch (Exception ex)
            {
                dataFile = AppDomain.CurrentDomain.GetData("DataDirectory").ToString() + "\\transactions.csv";
                if (!File.Exists(dataFile))
                {
                    using (File.Create(dataFile)) { }
                }
            }
        }
        public string MakeTransaction(Transactions trans)
        {
            CreateDataFile();
            if (trans.Amount == 0)
                return "You cannot make a transaction of 0 dollars! Please try again.";
            string amountString = trans.Amount.ToString("0.00#################");
            trans.Amount = decimal.Parse(amountString);
            string[] amountParts = amountString.Split('.');
            if (amountParts[1].Length > 2)
                return "A transaction can not be made for less than a cent. Please try again.";
            if(trans.Type.Equals(TransType.DEPOSIT))
            {
                if (trans.Amount < 0)
                    return "A deposit cannot be a negative number, to make a withdrawal please select the correct option.";
            }
            if (trans.Type.Equals(TransType.WITHDRAWAL))
            {
                if (trans.Amount < 0)
                    return "A withdrawal cannot be a negative number, to make a deposit please select the correct option.";
            }
            if (trans.Type.Equals(TransType.WITHDRAWAL))
            {
                if (trans.Amount > GetBalance(trans.User))
                    return "Insufficient funds, please check your balance and try again.";
                trans.Amount = trans.Amount * -1;
            }
            string line = "\"" + trans.User + "\",\"" + trans.Amount + "\",\"" + trans.Date.ToString() + "\",\"" + trans.Type.ToString() + "\"" + Environment.NewLine;
            File.AppendAllText(dataFile, line);
            return trans.Type.ToString() + " has been successful.";
        }
        public decimal GetBalance(string user)
        {
            CreateDataFile();
            decimal balance = 0;
            List<Transactions> trans = MapFileToTransactions();
            balance = trans.Where(x => x.User == user).Sum(x => x.Amount);
            return balance;
        }
        public List<Transactions> GetTransactionHistory(string user)
        {
            CreateDataFile();
            List<Transactions> trans = MapFileToTransactions();
            return trans.Where(x => x.User == user).OrderBy(x => x.Date).ToList();
        }
        public List<Transactions> MapFileToTransactions()
        {
            CreateDataFile();
            List<List<string>> fileData = CSV.GetCSVData(dataFile);
            List<Transactions> trans = new List<Transactions>();
            foreach (List<string> line in fileData)
            {
                Transactions tran = new Transactions();
                tran.User = line[0];
                tran.Amount = Convert.ToDecimal(line[1]);
                tran.Date = Convert.ToDateTime(line[2]);
                tran.Type = (TransType)Enum.Parse(typeof(TransType), line[3]);
                trans.Add(tran);
            }
            return trans;
        }
    }
}
