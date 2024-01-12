using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WhereIsMyMoney.MVVM.Models;

namespace WhereIsMyMoney.Stores
{
    static class AccountStore
    {
        public static Dictionary<string, AccountModel> Accounts { get; private set; }
        public static AccountModel SelectedAccount { get; private set; }
        private static readonly string _dirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Accounts");
        public static string[] Files { get; private set; }


        public static void LoadAccounts()
        {
            //Accounts = new Dictionary<string, AccountModel>();

            //if (!Directory.Exists(_dirPath))
            //{
            //    Directory.CreateDirectory(_dirPath);
            //}

            //Files = Directory.GetFiles(_dirPath, "*.json");

            //if (Files != null)
            //{
            //    foreach (string file in Files)
            //    {
            //        string dataPath = Path.Combine(_dirPath, file + ".json");
            //        if (File.Exists(dataPath))
            //        {
            //            string jsonContent = File.ReadAllText(dataPath);
            //            AccountModel account = JsonConvert.DeserializeObject<AccountModel>(jsonContent);
            //            Accounts[file] = account;
            //        }
            //    }
            //}
            //else
            //{
            //    Accounts["Dummy"] = new AccountModel
            //    {
            //        AccountName = "Dummy Account",
            //        Value = 0,
            //        Journals = new List<JournalsEntryModel> 
            //        { 
            //            new JournalsEntryModel 
            //            { 
            //                Category = Category.Sparen, 
            //                Value = 0, 
            //                Date = DateTime.Now, 
            //                Description = "Dummy Journal Entry", 
            //                LedgerSide = LedgerSide.DebitSide, 
            //                Name = "Dummy Business" 
            //            } 
            //        }
            //    };
            //}
        }

        public static void SelectAcount(string accountName)
        {
            SelectedAccount = Accounts[accountName];
        }

        public static void SaveAccount()
        {
            if (Accounts.ContainsKey("Dummy"))
            {
                Accounts.Remove("Dummy");
            }

            if (Accounts.Any())
            {
                string dataPath = Path.Combine(_dirPath, SelectedAccount.AccountName + ".json");
                string jsonData = JsonConvert.SerializeObject(SelectedAccount, Formatting.Indented);
                File.WriteAllText(dataPath, jsonData);
            }
        }

        public static Dictionary<string, AccountModel> GetStore()
        {
            return Accounts;
        }
    }
}
