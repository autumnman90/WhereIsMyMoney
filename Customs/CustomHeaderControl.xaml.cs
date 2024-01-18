using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WhereIsMyMoney.MVVM.ViewModels;

namespace WhereIsMyMoney.Customs
{
    /// <summary>
    /// Interaction logic for CustomHeaderControl.xaml
    /// </summary>
    public partial class CustomHeaderControl : Window
    {
        private ListView inputData = new ListView();
        private String filepath;
        public ObservableCollection<JournalEntryAdapterViewModel> DataTemplate { get; private set; }

        public CustomHeaderControl(string filepath)
        {
            InitializeComponent();
            inputData = inputDataView;
            inputDataView = inputData;
            GridView gridView = new GridView();
            GridViewColumn column = new GridViewColumn();
            gridView.Columns.Add(column);
            DataContext = this;
            this.filepath = filepath;
        }

        private GridView CreateDataGrid()
        {
            return new GridView();
        }

        private ObservableCollection<JournalEntryAdapterViewModel> CreateDataTemplate()
        {
            return new ObservableCollection<JournalEntryAdapterViewModel>();
        }
    }
}
