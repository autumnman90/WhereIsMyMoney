using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
        private string filepath;

        private List<List<string>> csvData;

        public ObservableCollection<JournalEntryAdapterViewModel> DataTemplate { get; private set; }

        public CustomHeaderControl(string filepath)
        {
            InitializeComponent();
            DataContext = this;
            this.filepath = filepath;
            //Info bereich, löschen später wenn fertig ;)
            //inputDataView = inputData;
            //GridView gridView = new GridView();
            //GridViewColumn column = new GridViewColumn();
            //gridView.Columns.Add(column);
            //-------------------------------------------
            csvData = new List<List<string>>(ReadCsvFile());

            if (csvData.Count>0)
            {
                var columnheaders = csvData[0];
                csvData.RemoveAt(0);
                SetGridViewColumns(columnheaders);
                inputDataView.ItemsSource = csvData.Select(row => row.Zip(columnheaders, (value, header) => new {Header = header, Value = value})
                                                   .ToDictionary(item => item.Header, item => item.Value))
                                                   .ToList();
            }
        }

        private void SetGridViewColumns(List<string> columnHeaders)
        {
            inputData.View = new GridView();

            foreach (var header in columnHeaders)
            {
                GridViewColumn gridViewColumn = new GridViewColumn 
                {
                    Header = header,
                    DisplayMemberBinding = new Binding($"[{header}]")
                };

                ((GridView)inputData.View).Columns.Add(gridViewColumn);
            }

        }

        private ObservableCollection<JournalEntryAdapterViewModel> CreateDataTemplate()
        {
            return new ObservableCollection<JournalEntryAdapterViewModel>();
        }

        
        private List<List<string>> ReadCsvFile()
        {
            List<List<string>> csvData = new List<List<string>>();

            try
            {
                using (StreamReader reader = new StreamReader(filepath))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        List<string> row = line.Split(';').ToList();
                        csvData.Add(row);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Lesen der CSV-Datei: {ex.Message}");
            }

            return csvData;
        }
    }
}
