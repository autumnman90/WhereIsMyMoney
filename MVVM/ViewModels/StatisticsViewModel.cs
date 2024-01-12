

using System.Collections.ObjectModel;
using WhereIsMyMoney.Core;
using WhereIsMyMoney.MVVM.Models;
using WhereIsMyMoney.Stores;

namespace WhereIsMyMoney.MVVM.ViewModels
{
    class StatisticsViewModel : BaseViewModel
    {
        private ObservableCollection<StatCubeAdapter> _cubeAdapters;

        public ObservableCollection<StatCubeAdapter> CubeAdapters
        { 
            get { return _cubeAdapters; } 
            set 
            { 
                _cubeAdapters = value; 
                OnPropertyChanged();
            }
        }

        private Analytics analyzer;

        public StatisticsViewModel()
        {
            analyzer = new Analytics(AccountManager.Instance.selectedAccount);
            CubeAdapters = new ObservableCollection<StatCubeAdapter>(analyzer.BalanceYear(225));//Int muss noch angepasst werden
        }

    }
}
