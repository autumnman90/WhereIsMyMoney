using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WhereIsMyMoney.Core;
using WhereIsMyMoney.Customs;
using WhereIsMyMoney.MVVM.Models;
using WhereIsMyMoney.Stores;

namespace WhereIsMyMoney.MVVM.ViewModels
{
    class AccountViewModel : BaseViewModel
    {

        public DelegateCommand CreateNewAccountCommand { get; private set; }
        public DelegateCommand DeleteSelectedAccountCommand { get; private set; }
        public DelegateCommand SetAccountCommand { get; private set; }

        private ObservableCollection<AccountModelAdapterViewModel> _accounts = new ObservableCollection<AccountModelAdapterViewModel>();
        public ObservableCollection<AccountModelAdapterViewModel> Accounts
        {
            get { return _accounts; }
            set
            {
                _accounts = value;
                OnPropertyChanged();
            }
        }

        private string _editAccount;

        public string EditAccount
        {
            get { return _editAccount; }
            set
            {
                _editAccount = value;
                OnPropertyChanged();
            }
        }

        private AccountModelAdapterViewModel _selectedAccount;

        public AccountModelAdapterViewModel SelectedAccount
        {
            get { return _selectedAccount; }
            set
            {
                _selectedAccount = value;
                OnPropertyChanged();
            }
        }

        public AccountViewModel()
        {
            CreateNewAccountCommand = new DelegateCommand((o) => CreateNewAccount());
            DeleteSelectedAccountCommand = new DelegateCommand((o) => DeleteSelectedAccount());
            SetAccountCommand = new DelegateCommand((o) => SetAccount());
            InitAccounts();
        }

        private void SetAccount()
        {
            AccountManager.Instance.SetAccount(SelectedAccount.AccountName);
            var customWarningBox = new CustomWrongInputWarning($"\"{SelectedAccount.AccountName}\" wurde als aktives Konto ausgewählt!");
            customWarningBox.ShowDialog();
        }

        private void CreateNewAccount()
        {
            var temp = new AccountModel(EditAccount, new List<JournalsEntryModel>());
            Accounts.Add(new AccountModelAdapterViewModel(temp));
            AccountManager.Instance.AddNewAccount(temp);
            EditAccount = null;
        }

        private void DeleteSelectedAccount()
        {
            var temp = SelectedAccount;
            Accounts.Remove(temp);
            AccountManager.Instance.DeleteAccount(temp.AccountName);
        }

        private void InitAccounts()
        {
            var temp = new List<AccountModel>();
            temp = AccountManager.Instance.GetAccountList();
            foreach (var account in temp)
            {
                Accounts.Add(new AccountModelAdapterViewModel(account));
            }
        }

        //private void InitDummies()
        //{
        //    Accounts.Add(new AccountModelAdapterViewModel
        //    (new AccountModel()
        //    {
        //        AccountName = "Gehaltskonto",
        //        Value = new decimal(2200.99),
        //        Journals = new List<JournalsEntryModel>()
        //    }));

        //    Accounts.Add(new AccountModelAdapterViewModel
        //    (new AccountModel
        //    {
        //        AccountName = "Sparkonto",
        //        Value = new decimal(5667),
        //        Journals = new List<JournalsEntryModel>()
        //    }));

        //    Accounts.Add(new AccountModelAdapterViewModel
        //    (new AccountModel
        //    {
        //        AccountName = "Gemeinschaftskonto",
        //        Value = new decimal(12.89),
        //        Journals = new List<JournalsEntryModel>()
        //    }));

        //    Accounts.Add(new AccountModelAdapterViewModel
        //    (new AccountModel
        //    {
        //        AccountName = "Aktiensparen",
        //        Value = new decimal(23333),
        //        Journals = new List<JournalsEntryModel>()
        //    }));

        //    Accounts.Add(new AccountModelAdapterViewModel
        //    (new AccountModel
        //    {
        //        AccountName = "Haushalt",
        //        Value = new decimal(2176.77),
        //        Journals = new List<JournalsEntryModel>()
        //    }));

        //    Accounts.Add(new AccountModelAdapterViewModel
        //    (new AccountModel
        //    {
        //        AccountName = "Urlaubsparen",
        //        Value = new decimal(120.98),
        //        Journals = new List<JournalsEntryModel>()
        //    }));
        //}
    }
}
