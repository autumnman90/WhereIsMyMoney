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
    /// Interaction logic for CustomWrongInputWarning.xaml
    /// </summary>
    public partial class CustomWrongInputWarning : Window
    {
        public String DescriptionText { get; private set; }
        public CustomWrongInputWarning(String descriptionText)
        {
            InitializeComponent();
            DataContext = this;
            DescriptionText = descriptionText;
        }
        public void YesButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
