using SimplyDB.ViewModel;
using System.Windows;

namespace SimplyDB.ViewMenu
{
    /// <summary>
    /// Interaction logic for CreateViewWindow.xaml
    /// </summary>
    public partial class CreateViewWindow : Window
    {
        private DatabaseViewModel mainDbVM;
        public CreateViewWindow(ref DatabaseViewModel mainDbVM)
        {
            InitializeComponent();
            this.mainDbVM = mainDbVM;
        }

        private void CreateViewCancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CreateViewConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            this.mainDbVM.CreateView(ViewNameTextBox.Text, ViewQueryTextBox.Text);
            this.Close();
        }
    }
}
