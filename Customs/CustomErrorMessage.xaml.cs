using System;
using System.Collections.Generic;
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

namespace WhereIsMyMoney.Customs
{
    /// <summary>
    /// Interaction logic for CustomErrorMessage.xaml
    /// </summary>
    public partial class CustomErrorMessage : Window
    {
        public string ErrorMessage {  get; set; }
        public string ErrorTitle { get; set; }

        public CustomErrorMessage(string errorTitle,string errorMessage)
        {
            InitializeComponent();
            DataContext = this;
            ErrorMessage = errorMessage;
            ErrorTitle = errorTitle;
        }

        private void ConfirmError(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
