using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using WhereIsMyMoney.MVVM.Models;
using WhereIsMyMoney.MVVM.ViewModels;

namespace WhereIsMyMoney.Stores
{
    public class AccountManager
    {
        private static readonly AccountManager instance = new AccountManager();

        public static AccountManager Instance
        {
            get { return instance; }
        }

        private string localAccountsFolderPath;
        private string file;
        private string localFilePath;
        private string[] files;
        private Dictionary<string, AccountModel> accountData;
        public AccountModel selectedAccount;
        public List<AccountModel> accounts; 

        private AccountManager()
        {
            localAccountsFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "WhereIsMyMoney", "Accounts");
            accountData = new Dictionary<string, AccountModel>();
            selectedAccount = new AccountModel();
            accounts = new List<AccountModel>();
            LoadAccountData();
        }

        private void LoadAccountData()
        {
            if (!Directory.Exists(localAccountsFolderPath))
            {
                Directory.CreateDirectory(localAccountsFolderPath);
            }

            files = Directory.GetFiles(localAccountsFolderPath, "*.json");
            string[] fileNames = new string[files.Length];
            fileNames = Directory.GetFiles(localAccountsFolderPath, "*.json")
                .Select(Path.GetFileNameWithoutExtension).ToArray();

            if (files.Length>0)
            {
                foreach (var file in fileNames)
                {
                    string dataPath = Path.Combine(localAccountsFolderPath, file+".json");
                    if (File.Exists(dataPath))
                    {
                        string jsonContent = File.ReadAllText(dataPath);
                        AccountModel account = JsonConvert.DeserializeObject<AccountModel>(jsonContent);
                        accountData[file] = account;
                    }
                }
                SetAccount(fileNames[0]);
                accounts = new List<AccountModel>();
                foreach (var account in accountData)
                {
                    accounts.Add(account.Value);
                }
                //if (File.Exists(localFilePath))
                //{
                //    string jsonContent = File.ReadAllText(localFilePath);
                //    accounts = JsonConvert.DeserializeObject<List<AccountModel>>(jsonContent);
                //    accountData = new Dictionary<string, AccountModel>();
                //    foreach (var account in accounts)
                //    {
                //        accountData[account.AccountName] = account;
                //    }
                //}
                //else
                //{
                //    accountData = new Dictionary<string, AccountModel>();
                //    accountData["Dummy"] = new AccountModel
                //    {
                //        AccountName = "Dummy Account",
                //        Value = 0,
                //        Journals = new List<JournalsEntryModel>
                //    {
                //        new JournalsEntryModel
                //        {
                //            Category = new CategoryModel
                //            {
                //                Name = "Lebensmittel",
                //                Icon = new CategoryIconModel("Lebensmittel")
                //            },
                //            Value = 0,
                //            Date = DateTime.Now,
                //            Description = "Dummy Journal Entry",
                //            Name = "Dummy Business"
                //        }
                //    }
                //    };
                //    SetAccount("Dummy");
                //    accounts = new List<AccountModel>
                //{
                //    accountData["Dummy"]
                //};
                //}
            }
            else
            {
                accountData = new Dictionary<string, AccountModel>();
                accountData["Dummy"] = new AccountModel("Dummy", new List<JournalsEntryModel>
                    {
                        new JournalsEntryModel
                        {
                            Category = new CategoryModel("Dummy", new CategoryIconModel("Lebensmittel")),
                            Value = 123,
                            Date = DateTime.Now,
                            Description = "Dummy Journal Entry",
                            Name = "Dummy Business"
                        }
                    });
                SetAccount("Dummy");
                accounts = new List<AccountModel>
                {
                    accountData["Dummy"]
                };
                SaveToFile("Dummy");
            }
        }

        public void SetAccount(string accountName)
        {
            selectedAccount = accountData[accountName];
        }

        public AccountModel GetAccount()
        {
            return selectedAccount;
        }

        public AccountModelAdapterViewModel GetAdapter()
        {
            return new AccountModelAdapterViewModel(selectedAccount);
        }

        public void UpdateSetAccount(AccountModel accountModel)
        {
            selectedAccount = accountModel;
            accountData[accountModel.AccountName] = accountModel;
            accounts[accounts.FindIndex(obj=> obj.AccountName == accountModel.AccountName)] = accountModel;
        }

        public void AddNewAccount(AccountModel accountToAdd)
        {
            accounts.Add(accountToAdd);
            accountData[accountToAdd.AccountName] = accountToAdd;
            SaveToFile(accountToAdd.AccountName);
        }

        /// <summary>
        /// Deletes the given Account from AccountList and the corosponding file from filesystem.
        /// If the deleted Account is the actual selected Account, the Account on position [0] in list will be 
        /// the new selected Account. If there are no Acounts left, then a new Dummy Account will be created.
        /// </summary>
        /// <param name="accountName"></param>
        public void DeleteAccount(string accountName)
        {
            accountData.Remove(accountName);
            accounts.RemoveAt(accounts.FindIndex(obj=> obj.AccountName == accountName));
            DeleteFile(accountName);
            if (accountName==selectedAccount.AccountName)
            {
                if (accounts.Count > 0)
                {
                    accounts.Sort();
                    selectedAccount = accounts[0];
                }
                else
                {
                    accountData = new Dictionary<string, AccountModel>();
                    accountData["Dummy"] = new AccountModel("Dummy", new List<JournalsEntryModel>
                    {
                        new JournalsEntryModel
                        {
                            Category = new CategoryModel("Dummy", new CategoryIconModel("Lebensmittel")),
                            Value = 123,
                            Date = DateTime.Now,
                            Description = "Dummy Journal Entry",
                            Name = "Dummy Business"
                        }
                    });
                    SetAccount("Dummy");
                    accounts = new List<AccountModel>
                    {
                    accountData["Dummy"]
                    };
                    SaveToFile("Dummy");
                }
            }
        }

        private void SaveToFile(string accountName)
        {
            file = accountName+".json";
            localFilePath = Path.Combine(localAccountsFolderPath, file);
            if (File.Exists(localFilePath))
            {
                string jsonData = JsonConvert.SerializeObject(accountData[accountName], Formatting.Indented);
                File.WriteAllText(localFilePath, jsonData);
            }
            else
            {
                string jsonData = JsonConvert.SerializeObject(accountData[accountName], Formatting.Indented);
                File.WriteAllText(localFilePath, jsonData);
            }
        }

        /// <summary>
        /// Saves all Account to there corosponding files on filesystem
        /// </summary>
        public void SaveAllWhenQuit()
        {
            foreach (var item in accountData)
            {
                SaveToFile(item.Key);
            }
        }

        private void DeleteFile(string accountName)
        {
            string filetoDelete = Path.Combine(localAccountsFolderPath,accountName+".json");
            if (File.Exists(filetoDelete))
            {
                File.Delete(filetoDelete);
            }
        }

        /// <summary>
        /// returns the actual AccountList
        /// </summary>
        /// <returns>List<AccountModel></returns>
        public List<AccountModel> GetAccountList()
        {
            return accounts;
        }

        /// <summary>
        /// returns the actual AccountList as ObservableCollection
        /// </summary>
        /// <returns>returns the actual AccountList as ObservableCollection</returns>
        public ObservableCollection<AccountModelAdapterViewModel> GetAdapterCollection()
        {
            var collection = new ObservableCollection<AccountModelAdapterViewModel>();
            foreach (var item in accounts)
            {
                collection.Add(new AccountModelAdapterViewModel(item));
            }
            return collection;
        }
    }
}
