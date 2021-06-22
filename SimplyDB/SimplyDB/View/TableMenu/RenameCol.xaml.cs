using System.Windows;


namespace SimplyDB.View.TableMenu
{
    /// <summary>
    /// Interaction logic for RenameCol.xaml
    /// </summary>
    public partial class RenameCol : Window
    {
        public string newName;
        
        public RenameCol(string oldName)
        {
            InitializeComponent();
            colNameTextBox.Text = oldName;
        }

        private void RenameCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RenameConfirm_Click(object sender, RoutedEventArgs e)
        {
            newName = colNameTextBox.Text;
            this.Close();
        }
    }
}
