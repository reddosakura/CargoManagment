using System;
using System.Collections.Generic;
using System.Data;
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
using System.Xml.Linq;
using CargoManagment.CargoManagmentDataSetTableAdapters;

namespace CargoManagment
{
    /// <summary>
    /// Логика взаимодействия для Supply.xaml
    /// </summary>
    public partial class Supply : Window
    {
        SupplierTableAdapter supply = new SupplierTableAdapter();
        UserTableAdapter user = new UserTableAdapter();
        public Supply()
        {
            InitializeComponent();
            SupplyDataGrid.ItemsSource = supply.GetData();
            UserCmbx.ItemsSource = user.GetData();
            UserCmbx.DisplayMemberPath = "ID";
        }

        private void OpenOrdersWindow(object sender, RoutedEventArgs e)
        {
            Orders win = new Orders();
            win.Show();
            Close();
        }

        private void OpenSupplyWindow(object sender, RoutedEventArgs e)
        {
            Supply win = new Supply();
            win.Show();
            Close();
        }

        private void OpenProductWindow(object sender, RoutedEventArgs e)
        {
            Product product = new Product();
            product.Show();
            Close();
        }

        private void SupplyAddAction(object sender, RoutedEventArgs e)
        {
           if (productNameTb.Text == string.Empty) 
           {
                MessageBox.Show("Введено пустое значение");
                return;
           }

           if (UserCmbx.Text == string.Empty)
           {
                MessageBox.Show("Пользователь не выбран");
                return; 
           }

            object cell = (UserCmbx.SelectedItem as DataRowView).Row[0];

            supply.InsertQuery(Convert.ToInt32(cell), productNameTb.Text);
            SupplyDataGrid.ItemsSource = supply.GetData();

        }

        private void SupplyEditAction(object sender, RoutedEventArgs e)
        {
            try
            {
                if (productNameTb.Text == string.Empty)
                {
                    MessageBox.Show("Введено пустое значение");
                    return;
                }

                if (UserCmbx.Text == string.Empty)
                {
                    MessageBox.Show("Пользователь не выбран");
                    return;
                }
                object id = (SupplyDataGrid.SelectedItem as DataRowView).Row[0];
                object cell = (UserCmbx.SelectedItem as DataRowView).Row[0];

                supply.UpdateQuery(Convert.ToInt32(cell), productNameTb.Text, Convert.ToInt32(id));
                SupplyDataGrid.ItemsSource = supply.GetData();
            }
            catch { MessageBox.Show("Ошибка изменения"); }
        }

        private void SupplyDeleteAction(object sender, RoutedEventArgs e)
        {
            try
            {
                object id = (SupplyDataGrid.SelectedItem as DataRowView).Row[0];
                supply.DeleteQuery(Convert.ToInt32(id));
                SupplyDataGrid.ItemsSource = supply.GetData();
            }
            catch { MessageBox.Show("Ошибка удаления"); }
        }

        private void SupplySelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                var supplies = supply.GetData().Rows;
                int i = ((DataGrid)sender).SelectedIndex;
                UserCmbx.Text = Convert.ToString(supplies[i][1]);
                productNameTb.Text = Convert.ToString(supplies[i][2]);

            }
            catch (IndexOutOfRangeException) { }
        }
    }
}
