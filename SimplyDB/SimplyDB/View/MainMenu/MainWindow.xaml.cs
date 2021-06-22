using SimplyDB.ViewModel;
using SimplyDB.View;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

namespace SimplyDB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DatabaseViewModel mainDbVM; // Main View Model Object
        public DatabaseViewModel MainDbVM { get { return mainDbVM; } set { mainDbVM = value; } }

        private bool editModeFlag = false; // Locking flag
        public bool EditModeFlag { get { return editModeFlag; } set { editModeFlag = value; } }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this; // set Data Context
            MainDbVM = new ViewModel.DatabaseViewModel(); // Create Main Database ViewModel
        }

        // Orange Buttons
        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            MainDbVM.Refresh();
        }

        private void LockingButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainDbVM.DbPath != null)
            {
                if (MainDbVM.LockFile())
                {
                    LockingButton.Content = "Unlock DB";
                }
                else
                {
                    LockingButton.Content = "Lock DB";
                }
            }

        }

        // Green Buttons
        private void CreateDbButton_Click(object sender, RoutedEventArgs e)
        {
            MainDbVM.CreateWindow();
            MainDbVM.Refresh();
        }

        private void LoadDbButton_Click(object sender, RoutedEventArgs e)
        {
            MainDbVM.LoadWindow();
            MainDbVM.Refresh();
        }

        private void BackupDbButton_Click(object sender, RoutedEventArgs e)
        {
            MainDbVM.BackupWindow();
            MainDbVM.Refresh();
        }

        private void DeleteDbButton_Click(object sender, RoutedEventArgs e)
        {
            MainDbVM.DeleteWindow();
        }

        // Yellow Buttons
        private void CreateTableButton_Click(object sender, RoutedEventArgs e)
        {
            TableMenu.CreateTableWindow objCreateTableWindow = new TableMenu.CreateTableWindow(ref mainDbVM);
            objCreateTableWindow.ShowDialog();
            MainDbVM.Refresh();
        }

        private void EditModeButton_Click(object sender, RoutedEventArgs e)
        {
            if (EditModeFlag)
            {
                tableContentDataGrid.IsReadOnly = true;
                tableContentDataGrid.CanUserAddRows = false;
                tableContentDataGrid.CanUserDeleteRows = false;
                EditModeButton.Content = "Activate Edit Mode";
            }
            else
            {
                tableContentDataGrid.IsReadOnly = false;
                tableContentDataGrid.CanUserAddRows = true;
                tableContentDataGrid.CanUserDeleteRows = true;
                EditModeButton.Content = "Disable Edit Mode";
            }
            EditModeFlag = !EditModeFlag;
        }

        private void DeleteTableButton_Click(object sender, RoutedEventArgs e)
        {
            MainDbVM.DeleteTable();
            MainDbVM.Refresh();
        }

        private void CreateViewButton_Click(object sender, RoutedEventArgs e)
        {
            ViewMenu.CreateViewWindow objCreateViewWindow = new ViewMenu.CreateViewWindow(ref mainDbVM);
            objCreateViewWindow.ShowDialog();
        }

        private void DeleteViewButton_Click(object sender, RoutedEventArgs e)
        {
            MainDbVM.DeleteView();
            MainDbVM.Refresh();
        }

        // Custom Query
        private void CustomQuerySubmitButton_Click(object sender, RoutedEventArgs e)
        {
            MainDbVM.CustomQuery = customQueryTextBox.Text;
            MainDbVM.SubmitCustomQuery();
            tableContentDataGrid.ItemsSource = MainDbVM.TableContentData.DefaultView;
            MainDbVM.Refresh();
        }

        // Selection Changed
        private void TableListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tableListBox.SelectedItem != null)
            {
                MainDbVM.SelectedTable = tableListBox.SelectedItem.ToString(); // sets the new selected table
                SelectedTableNameTextBlock.Text = tableListBox.SelectedItem.ToString(); // sets the textbox for selected table
                MainDbVM.GetTableInfo();
                MainDbVM.GetTableData();
                tableInfoDataGrid.ItemsSource = MainDbVM.TableInfoData.DefaultView;
                tableContentDataGrid.ItemsSource = MainDbVM.TableContentData.DefaultView;
            }
        }

        private void ViewListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (viewListBox.SelectedItem!=null)
            {
                MainDbVM.SelectedView = viewListBox.SelectedItem.ToString(); // sets the new selected view
                MainDbVM.GetViewTableData();
                tableContentDataGrid.ItemsSource = MainDbVM.TableContentData.DefaultView;
            }
        }

        // Other
        private void TableNameSetButton_Click(object sender, RoutedEventArgs e) // set table name
        {
            MainDbVM.EditTableName(SelectedTableNameTextBlock.Text);
        }

        private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e) // index for datagrids
        {
            e.Row.Header = (e.Row.GetIndex()+1).ToString();
        }

        private void ContentSaveButton_Click(object sender, RoutedEventArgs e)
        {
            MainDbVM.ContentSave();
        }

        private void DeleteRowButton_Click(object sender, RoutedEventArgs e) // Delete Info Row
        {
            DataRowView dataRowView = (DataRowView)tableInfoDataGrid.SelectedItem;
            if ((string)dataRowView["name"]!=null)
            {
                var result = System.Windows.Forms.MessageBox.Show("Delete selectd table column? (Irreversible)", "Deletion Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    MainDbVM.DeleteTableColumn((string)dataRowView["name"]);
                    tableInfoDataGrid.ItemsSource = MainDbVM.TableInfoData.DefaultView;
                }
            }
        }

        private void RenameRowButton_Click(object sender, RoutedEventArgs e) // Rename Info Row
        {
            DataRowView dataRowView = (DataRowView)tableInfoDataGrid.SelectedItem;
            View.TableMenu.RenameCol objRenameCol = new View.TableMenu.RenameCol((string)dataRowView["name"]);
            objRenameCol.ShowDialog();
            if (objRenameCol.newName != null && objRenameCol.newName != "" && objRenameCol.newName != (string)dataRowView["name"])
            {
                MainDbVM.ApplyCellUpdate((string)dataRowView["name"], objRenameCol.newName);
                MainDbVM.GetTableInfo();
                tableInfoDataGrid.ItemsSource = MainDbVM.TableInfoData.DefaultView;
                tableContentDataGrid.ItemsSource = MainDbVM.TableContentData.DefaultView;
            }
        }

        private void InsertColumn_Click(object sender, RoutedEventArgs e)
        {
            View.TableMenu.CreateColumn createColumnWindow = new View.TableMenu.CreateColumn(ref mainDbVM);
            createColumnWindow.ShowDialog();
            if(MainDbVM.SelectedTable!=null)
            {
                MainDbVM.Refresh();
                tableInfoDataGrid.ItemsSource = MainDbVM.TableInfoData.DefaultView;
                tableContentDataGrid.ItemsSource = MainDbVM.TableContentData.DefaultView;
            }
        }
    }
}