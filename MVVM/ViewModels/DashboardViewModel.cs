using System.Collections.ObjectModel;
using System.Linq;
using WhereIsMyMoney.Core;
using WhereIsMyMoney.MVVM.Models;
using WhereIsMyMoney.Stores;

namespace WhereIsMyMoney.MVVM.ViewModels
{
    class DashboardViewModel : BaseViewModel
    {
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

        private AccountModelAdapterViewModel _account;

        public AccountModelAdapterViewModel Account
        {
            get { return _account; }
            set
            {
                _account = value;
                ChangeAccount();
                OnPropertyChanged();
            }
        }

        private Analytics analyser;

        private decimal _income = 0.0m;

        public decimal Income
        {
            get { return _income; }
            set 
            { 
                _income = value; 
                OnPropertyChanged();
            }
        }

        private decimal _spendings = 0.0m;

        public decimal Spendings
        {
            get { return _spendings; }
            set 
            { 
                _spendings = value; 
                OnPropertyChanged();
            }
        }

        private ObservableCollection<AnalyserModelAdapter> _spendingsByCategory = new ObservableCollection<AnalyserModelAdapter>();

        public ObservableCollection<AnalyserModelAdapter> SpendingsByCategory
        {
            get { return _spendingsByCategory; }
            set 
            { 
                _spendingsByCategory = value; 
                OnPropertyChanged();
            }
        }

        private ObservableCollection<JournalEntryAdapterViewModel> _lastBookings = new ObservableCollection<JournalEntryAdapterViewModel>();

        public ObservableCollection<JournalEntryAdapterViewModel> LastBookings
        {
            get { return _lastBookings; }
            set 
            {
                _lastBookings = value;
                OnPropertyChanged();
            }
        }


        public DashboardViewModel()
        {
            Accounts = AccountManager.Instance.GetAdapterCollection();
            Account = AccountManager.Instance.GetAdapter();
        }

        private void ChangeAccount()
        {
            AccountManager.Instance.SetAccount(_account.AccountName);
            analyser = new Analytics(AccountManager.Instance.GetAccount());
            LoadAnalytics();
        }

        private void LoadAnalytics()
        {
            var tempBookings = analyser.GetLastBookings();
            var tempSpendingsByCategory = analyser.GetSpendingsByCategory();
            var tempValues = analyser.GetValues();

            if (tempBookings.Count>0)
            {
                if (LastBookings.Count>0)
                {
                    LastBookings.Clear();
                }
                else
                {
                    LastBookings = new ObservableCollection<JournalEntryAdapterViewModel>();
                }
                foreach (var item in tempBookings)
                {
                    LastBookings.Add(new JournalEntryAdapterViewModel(item));
                }
            }
            else
            {
                LastBookings = new ObservableCollection<JournalEntryAdapterViewModel>();
            }

            if (tempSpendingsByCategory.Count>0)
            {
                if (SpendingsByCategory.Count>0)
                {
                    SpendingsByCategory.Clear();
                }
                else
                {
                    SpendingsByCategory = new ObservableCollection<AnalyserModelAdapter>();
                }
                foreach (var item in tempSpendingsByCategory)
                {
                    if (item.AvarageValue<0)
                    {
                        SpendingsByCategory.Add(new AnalyserModelAdapter(item));
                    }
                }
                SpendingsByCategory = new ObservableCollection<AnalyserModelAdapter>
                        (
                            SpendingsByCategory.OrderBy(x => x.AvarageValue)
                        );
            }
            else
            {
                SpendingsByCategory = new ObservableCollection<AnalyserModelAdapter>();
            }

            Income = tempValues[2];
            Spendings = tempValues[3];
        }

        public void AccountChanged(AccountModelAdapterViewModel selectedAccount)
        {
            Account = selectedAccount;
        }
    }
}
