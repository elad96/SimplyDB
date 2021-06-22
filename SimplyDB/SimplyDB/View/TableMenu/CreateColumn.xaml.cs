using SimplyDB.ViewModel;
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

namespace SimplyDB.View.TableMenu
{
    public partial class CreateColumn : Window
    {
        private DatabaseViewModel mainDbVM;

        private string colName = "";
        public string ColName
        { get { return colName; } set { colName = value; } }

        private string colType = "";
        public string ColType
        { get { return colType; } set { colType = value; } }

        private bool colNotNull = false;
        public bool ColNotNull
        { get { return colNotNull; } set { colNotNull = value; } }

        private string colDefaultValue ="";
        public string ColDefaultValue
        { get { return colDefaultValue; } set { colDefaultValue = value; } }

        public CreateColumn(ref DatabaseViewModel mainDbVM)
        {
            InitializeComponent();
            this.mainDbVM = mainDbVM;
            this.DataContext = this;
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.ColName==""||this.ColType=="")
            {
                MessageBox.Show("Invalid Name or Type");
            }
            else if (this.colNotNull==true && this.colDefaultValue=="")
            {
                MessageBox.Show("Using Not Null Requires Default Value");
            }
            else
            {
                mainDbVM.CreateColumn(this.ColName, this.ColType, this.ColNotNull, this.ColDefaultValue);
                this.Close();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TypeInput_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedType = (ComboBoxItem)TypeInput.SelectedValue;
            this.ColType = (string)selectedType.Content;
        }
    }
}
