using Microsoft.VisualBasic;
using Microsoft.VisualBasic.FileIO;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Navigation;
using WhereIsMyMoney.Core;
using WhereIsMyMoney.Customs;
using WhereIsMyMoney.MVVM.Models;
using WhereIsMyMoney.Stores;

namespace WhereIsMyMoney.MVVM.ViewModels
{
    class DataImportViewModel : BaseViewModel
    {
        public DelegateCommand OpenFileDialogCommand { get; private set; }
        public DelegateCommand ImportDataCommand { get; private set; }

        private string _fileName;

        /// <summary>
        /// 0-> Valutadatum
        /// 1-> Begünstigter
        /// 2-> Verwendungszweck
        /// 3-> Betrag
        /// </summary>
        private int[] headerIndexes = new int[4];

        public string FileName 
        {
            get { return _fileName; }
            set 
            {
                _fileName = value;
                OnPropertyChanged();
            } 
        }

        private ObservableCollection<JournalEntryAdapterViewModel> _fileJournalEntries = new ObservableCollection<JournalEntryAdapterViewModel>();

        public ObservableCollection<JournalEntryAdapterViewModel> FileJournalEntries
        {
            get { return _fileJournalEntries; }
            set 
            { 
                _fileJournalEntries = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<AccountModelAdapterViewModel> Accounts { get; private set; }

        private AccountModelAdapterViewModel _selectedAccount;

        public AccountModelAdapterViewModel SelectedAccount
        {
            get { return _selectedAccount; }
            set 
            { 
                _selectedAccount = value;
                ImportDataCommand.RaiseCanExecuteChanged();
            }
        }


        public DataImportViewModel()
        {
            OpenFileDialogCommand = new DelegateCommand((o)=> OpenCsvFile());
            ImportDataCommand = new DelegateCommand((o)=> FileJournalEntries.Count>0 && _selectedAccount!=null,(o)=> ImportData());
            Accounts = new ObservableCollection<AccountModelAdapterViewModel>();
            foreach (var item in AccountManager.Instance.accounts)
            {
                Accounts.Add(new AccountModelAdapterViewModel(item));
            }
            FileName = "Noch bin ich leer";
        }

        private void OpenCsvFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV Dateien (*.csv)|*.csv|Alle Dateien (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                FileName = openFileDialog.FileName;
            }

            try
            {
                using (TextFieldParser parser = new TextFieldParser(FileName))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(";");

                    string[] headers = parser.ReadFields();

                    for (int i = 0; i < headers.Length; i++)
                    {
                        string temp = headers[i].Trim().ToLower();
                        switch (temp)
                        {
                            case "valutadatum":
                                headerIndexes[0] = i;
                                break;
                            case "beguenstigter/zahlungspflichtiger":
                                headerIndexes[1] = i;
                                break;
                            case "verwendungszweck":
                                headerIndexes[2] = i;
                                break;
                            case "betrag":
                                headerIndexes[3] = i;
                                break;
                            default:
                                break;
                        }
                    }

                    List<JournalsEntryModel> fileJournal = new List<JournalsEntryModel>();

                    while (!parser.EndOfData)
                    {
                        string[] fields = parser.ReadFields();
                        fileJournal.Add(new JournalsEntryModel
                        {
                            Date = DateTime.Parse(fields[headerIndexes[0]]),
                            Value = System.Convert.ToDecimal(fields[headerIndexes[3]]),
                            Description = fields[headerIndexes[2]],
                            Name = fields[headerIndexes[1]],
                            Category = CategoryGenerator.GenerateCategory(fields[headerIndexes[2]])
                        }) ;
                    }
                    
                    foreach (JournalsEntryModel entry in fileJournal)
                    {
                        FileJournalEntries.Add(new JournalEntryAdapterViewModel(entry));
                    }
                }
            }
            catch (Exception e)
            {
                var customErrorMessage = new CustomErrorMessage("Parse Error", e.ToString());
                customErrorMessage.ShowDialog();
            }
            ImportDataCommand.RaiseCanExecuteChanged();
        }

        private void ImportData()
        {
            var customMessage = new CustomDeleteWarning("Daten import", $"Bist du dir sicher dass du die unten stehenden Daten in {_selectedAccount.AccountName} importieren möchtest? Achtung dieser Vorgang ist nicht umkehrbar!");
            bool? dialog = customMessage.ShowDialog();

            if (dialog==true)
            {
                List<JournalsEntryModel> dataJournal = new List<JournalsEntryModel>();
                foreach (var item in FileJournalEntries)
                {
                    dataJournal.Add(item.GetModel());
                }

                AccountManager.Instance.SetAccount(SelectedAccount.AccountName);
                var account = SelectedAccount.getAccountModel();
                account.Journals.AddRange(dataJournal);
                AccountManager.Instance.UpdateSetAccount(account);
            }
        }
    }
}
