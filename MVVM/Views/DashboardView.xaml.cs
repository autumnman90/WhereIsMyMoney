using System.Windows.Controls;
using WhereIsMyMoney.MVVM.ViewModels;

namespace WhereIsMyMoney.MVVM.Views
{
    /// <summary>
    /// Interaction logic for DashboardView.xaml
    /// </summary>
    public partial class DashboardView : UserControl
    {
        public DashboardView()
        {
            InitializeComponent();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                var selectedAcount = e.AddedItems[0] as AccountModelAdapterViewModel;
                if (selectedAcount != null)
                {
                    (DataContext as DashboardViewModel)?.AccountChanged(selectedAcount);
                }
            }
        }
    }
}
