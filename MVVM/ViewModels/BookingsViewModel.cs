using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using WhereIsMyMoney.Core;
using WhereIsMyMoney.Customs;
using WhereIsMyMoney.MVVM.Models;
using WhereIsMyMoney.Stores;

namespace WhereIsMyMoney.MVVM.ViewModels
{
    class BookingsViewModel : BaseViewModel
    {
        public DelegateCommand AddJournalEntryCommand { get; private set; }
        public DelegateCommand DeleteSelectedEntryCommand { get; private set; }
        public DelegateCommand SetMonthCommand { get; private set; }

        private ObservableCollection<JournalEntryAdapterViewModel> journalEntries = new ObservableCollection<JournalEntryAdapterViewModel>();
        private ObservableCollection<JournalEntryAdapterViewModel> _sortedJournalEntries = new ObservableCollection<JournalEntryAdapterViewModel>();

        private ObservableCollection<CategoryAdapterViewModel> _categoryList = new ObservableCollection<CategoryAdapterViewModel>();

        public ObservableCollection<CategoryAdapterViewModel> CategoryList
        {
            get { return _categoryList; }
            set 
            { 
                _categoryList = value; 
                OnPropertyChanged();
            }
        }

        public ObservableCollection<JournalEntryAdapterViewModel> SortedJournalEntries
        {
            get { return _sortedJournalEntries; }
            set
            {
                _sortedJournalEntries = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<AccountModelAdapterViewModel> AccountList
        {
            get => AccountManager.Instance.GetAdapterCollection();
        }

        private JournalEntryAdapterViewModel _selectedJournalEntry;

        public JournalEntryAdapterViewModel SelectedJournalEntry
        {
            get { return _selectedJournalEntry; }
            set
            {
                _selectedJournalEntry = value;
                OnPropertyChanged();
            }
        }

        private string _dateEntry;
        private string _nameEntry;
        private string _descriptionEntry;
        private CategoryAdapterViewModel _selectedCategory; //TODO Muss durch Kategorie Liste getauscht werden!
        private decimal _valueEntry;

        private string _titleMonth;
        private string _titleYear;

        private DateTime selectedDate;

        //private AccountModel _account;
        //TODO was jetzt mit den beiden???
        private AccountModelAdapterViewModel _account;

        public AccountModelAdapterViewModel Account
        {
            get { return _account; }
            set
            {
                AccountManager.Instance.SetAccount(value.AccountName);
                _account = value;
                OnPropertyChanged();
            }
        }
        
        public string TitleYear
        {
            get { return _titleYear; }
            set 
            { 
                _titleYear = value; 
                OnPropertyChanged();
            }
        }
        public string DateEntry
        {
            get { return _dateEntry; }
            set
            {
                _dateEntry = value;
                OnPropertyChanged();
            }
        }
        public string NameEntry
        {
            get { return _nameEntry; }
            set
            {
                _nameEntry = value;
                OnPropertyChanged();
            }
        }
        public string DescriptionEntry
        {
            get { return _descriptionEntry; }
            set
            {
                _descriptionEntry = value;
                OnPropertyChanged();
            }
        }
        public CategoryAdapterViewModel SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                OnPropertyChanged();
            }
        }
        public decimal ValueEntry
        {
            get { return _valueEntry; }
            set
            {
                _valueEntry = value;
                OnPropertyChanged();
            }
        }
        public string TitleMonth
        {
            get { return _titleMonth; }
            set
            {
                _titleMonth = value;
                OnPropertyChanged();
            }
        }
        public BookingsViewModel()
        {
            Account = AccountManager.Instance.GetAdapter();
            InitData();
            AddJournalEntryCommand = new DelegateCommand((o) => AddJournalEntry());
            DeleteSelectedEntryCommand = new DelegateCommand((o) => DeleteSelectedJournalEntry());
            SetMonthCommand = new DelegateCommand(SetDate);
            selectedDate = DateTime.Now;
            TitleMonth = selectedDate.ToString("MMMM");
            TitleYear = selectedDate.ToString("yyyy");
            DateEntry = selectedDate.ToString("d");
            SetSortedJournalEntries();
        }
        private void InitData()
        {
            var tempCategories = new List<CategoryModel>(CategoryStore.Instance.categories);
            foreach (var item in tempCategories)
            {
                CategoryList.Add(new CategoryAdapterViewModel(item));
            }
            if (Account.Journals.Count>0)
            {
                foreach (var entry in Account.Journals)
                {
                    journalEntries.Add(new JournalEntryAdapterViewModel(entry));
                }
            }
        }
        private void SetSortedJournalEntries() 
        {
            SortedJournalEntries.Clear();
            foreach (var entry in journalEntries)
            {
                if (entry.Date.Month==selectedDate.Month&&entry.Date.Year==selectedDate.Year)
                {
                    SortedJournalEntries.Add(entry);
                }
            }
            SortedJournalEntries = new ObservableCollection<JournalEntryAdapterViewModel>(SortedJournalEntries.OrderBy(c => c.Date));
        }
        private void AddJournalEntry()
        {
            if (SelectedCategory != null && DateEntry != null && NameEntry != null && DescriptionEntry != null)
            {
                try
                {
                    DateTime dateFromString = DateTime.Parse(DateEntry);
                    var tempEntry = new JournalsEntryModel
                    {
                        Date = dateFromString,
                        Name = NameEntry,
                        Category = new CategoryModel(SelectedCategory.CategoryName, SelectedCategory.CategoryIcon),
                        Description = DescriptionEntry,
                        Value = ValueEntry
                    };
                    JournalEntryAdapterViewModel tempAdapterEntry = new JournalEntryAdapterViewModel(tempEntry);
                    journalEntries.Add(tempAdapterEntry);
                    Account.Journals.Add(tempEntry);
                    AccountManager.Instance.UpdateSetAccount(new AccountModel(Account.AccountName, Account.Journals));
                }
                catch
                {
                    var customWarningBox = new CustomWrongInputWarning("Falsches Datumsformat!");
                    customWarningBox.ShowDialog();
                }

            }
            else
            {
                var customWarningBox = new CustomWrongInputWarning("Bitte alle Felder ausfüllen!");
                customWarningBox.ShowDialog();
            }
            SetSortedJournalEntries();
        }
        private void DeleteSelectedJournalEntry()
        {
            var customMessageBox = new CustomDeleteWarning("Achtung!", $"Bist du dir sicher dass du den markierten Eintrag vom {SelectedJournalEntry.Date.ToString("dd.MM.yyyy")} mit dem Titel \"{SelectedJournalEntry.Name}\" löschen möchtest?");
            bool? result = customMessageBox.ShowDialog();

            if (result == true)
            {
                journalEntries.Remove(SelectedJournalEntry);
                var updatedJournal = new List<JournalsEntryModel>();
                foreach (var item in journalEntries)
                {
                    updatedJournal.Add(new JournalsEntryModel
                    {
                        Date = item.Date,
                        Value = item.Value,
                        Description = item.Description,
                        Category = new CategoryModel(item.Category.CategoryName, item.Category.CategoryIcon),
                        Name = item.Name,
                    });
                }
                var updatedAccount = new AccountModel(Account.AccountName, updatedJournal);
                Account.UpdateAccountModel(updatedAccount);
                AccountManager.Instance.UpdateSetAccount(updatedAccount);
                SetSortedJournalEntries();
            }
        }
        private void SetDate(object parameter)
        {
            int month = int.Parse(parameter.ToString());
            selectedDate = selectedDate.AddMonths(month);
            TitleMonth = selectedDate.ToString("MMMM");
            TitleYear = selectedDate.ToString("yyyy");
            SetSortedJournalEntries();
        }

        public void AccountChanged(AccountModelAdapterViewModel selectedAccount)
        {
            Account = selectedAccount;
            if (Account.Journals.Count > 0)
            {
                journalEntries.Clear();
                foreach (var entry in Account.Journals)
                {
                    journalEntries.Add(new JournalEntryAdapterViewModel(entry));
                }
                SetSortedJournalEntries();
            }
        }
    }
}
