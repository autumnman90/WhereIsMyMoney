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
    /// Interaction logic for CustomDeleteWarning.xaml
    /// </summary>
    public partial class CustomDeleteWarning : Window
    {
        public string boxTitle { get; private set; }
        public string boxDescription { get; private set; }
        public CustomDeleteWarning(string boxTitle, string boxDescription)
        {
            this.boxTitle = boxTitle;
            this.boxDescription = boxDescription;
            DataContext = this;
            InitializeComponent();
        }

        public void YesButton_Click(object sender, RoutedEventArgs e) 
        {
            DialogResult = true;
            Close();
        }

        private void NoButton_Click(object sender, RoutedEventArgs e) 
        {
            DialogResult=false;
            Close();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
