using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using WhereIsMyMoney.Core;
using WhereIsMyMoney.Customs;
using WhereIsMyMoney.Stores;

namespace WhereIsMyMoney.MVVM.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        
        #region Commands
        public DelegateCommand Quit { get; private set; }
        public DelegateCommand Minimize { get; private set; }
        public DelegateCommand Maximize { get; private set; }
        public DelegateCommand SetCurrentViewModel { get; private set; }
        #endregion

        private String _nameOfView;
        public String NameOfView
        {
            get { return _nameOfView; }
            set
            {
                _nameOfView = value;
                OnPropertyChanged();
            }
        }

        private BaseViewModel _currentViewModel;
        public BaseViewModel CurrentViewModel
        {
            get { return _currentViewModel; }
            set
            {
                _currentViewModel = value;
                OnPropertyChanged();
            }
        }

        private List<VM> _views = new List<VM>();

        public MainViewModel()
        {
            Quit = new DelegateCommand((o) => QuitApp());
            Minimize = new DelegateCommand((o) => MinimizeWindow());
            Maximize = new DelegateCommand((o) => MaximizeWindow());
            SetCurrentViewModel = new DelegateCommand(SetViewModel);
            //InitViews();
            NameOfView = "Willkommen";
            CurrentViewModel = new EntryViewModel();
        }

        private void InitViews()
        {
            String[] views = new string[] { "Dashboard", "Buchungen", "Statistik", "Konten", "Kategorien", "Vorlagen", "Import", "Info", "Einstellungen" };
            BaseViewModel[] viewModels = new BaseViewModel[] { new DashboardViewModel(), new BookingsViewModel(), new StatisticsViewModel(), new AccountViewModel(),
            new CategoriesViewModel(), new TemplatesViewModel(), new DataImportViewModel(), new InfoViewModel(), new SettingsViewModel()};
            for (int i = 0; i < views.Length; i++)
            {
                VM v = new VM { Name = views[i], ViewModel = viewModels[i] };
                _views.Add(v);
            }
        }

        private void SetViewModel(object parameter)
        {
            string input = parameter as string;
            int p = Int32.Parse(input);
            String[] views = new string[] { "Dashboard", "Buchungen", "Statistik", "Konten", "Kategorien", "Vorlagen", "Import", "Info", "Einstellungen" };
            input = views[p];
            //NameOfView = _views[p].Name;
            //CurrentViewModel = _views[p].ViewModel;

            switch (input)
            {
                case "Dashboard":
                    NameOfView = input;
                    CurrentViewModel = new DashboardViewModel();
                    break;
                case "Buchungen":
                    CurrentViewModel = new BookingsViewModel();
                    NameOfView = input;
                    break;
                case "Statistik":
                    NameOfView = input;
                    CurrentViewModel = new StatisticsViewModel();
                    break;
                case "Konten":
                    CurrentViewModel = new AccountViewModel();
                    NameOfView = input;
                    break;
                case "Kategorien":
                    NameOfView = input;
                    CurrentViewModel = new CategoriesViewModel();
                    break;
                case "Vorlagen":
                    CurrentViewModel = new TemplatesViewModel();
                    NameOfView = input;
                    break;
                case "Import":
                    NameOfView = input;
                    CurrentViewModel = new DataImportViewModel();
                    break;
                case "Info":
                    CurrentViewModel = new InfoViewModel();
                    NameOfView = input;
                    break;
                case "Einstellungen":
                    CurrentViewModel = new SettingsViewModel();
                    NameOfView = input;
                    break;
            }
        }

        private void QuitApp()
        {
            var customMessageBox = new CustomDeleteWarning("Achtung!", "Ungespeicherte Inhalte gehen verloren wenn du jetzt beenden. Möchtest du vor dem Beenden speichern?");
            bool? result = customMessageBox.ShowDialog();
            if (result==true)
            {
                AccountManager.Instance.SaveAllWhenQuit();
                Application.Current.Shutdown();
            }
            Application.Current.Shutdown();
        }

        private void MaximizeWindow()
        {
            if (Application.Current.MainWindow.WindowState != WindowState.Maximized)
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
            else
                Application.Current.MainWindow.WindowState = WindowState.Normal;
        }

        private void MinimizeWindow()
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

    }

    class VM
    {
        public string Name { get; set; }
        public BaseViewModel ViewModel { get; set; }
    }
}
