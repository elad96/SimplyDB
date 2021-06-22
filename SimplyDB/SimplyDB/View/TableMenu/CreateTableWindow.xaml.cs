using SimplyDB.ViewModel;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace SimplyDB.TableMenu
{
    /// <summary>
    /// Interaction logic for CreateTableWindow.xaml
    /// </summary>
    /// 

    public partial class CreateTableWindow : Window
    {
        private DatabaseViewModel mainDbVM;
        public List<string> ComboTypes { get; set; }

        public CreateTableWindow(ref DatabaseViewModel mainDbVM)
        {
            InitializeComponent();
            this.mainDbVM = mainDbVM;
            DataContext = this;

            ComboTypes = new List<string>();
            ComboTypes.Add("INTEGER");
            ComboTypes.Add("REAL");
            ComboTypes.Add("TEXT");
            ComboTypes.Add("BLOB");

            mainDbVM.ClearCreateTable();
            createTableDataGrid.ItemsSource = mainDbVM.CreateTableInfo.DefaultView; // set item source to data table list
        }

        private void TableCreateCancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TableCreateConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            mainDbVM.CreateTable(tableNameTextBox.Text);
            this.Close();
        }

        private void createTableDataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex()).ToString();
        }
    }
}
