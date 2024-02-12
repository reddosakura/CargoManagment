using System;
using System.Collections.Generic;
using System.Data;
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
using CargoManagment.CargoManagmentDataSetTableAdapters;

namespace CargoManagment
{
    /// <summary>
    /// Логика взаимодействия для CreateOrder.xaml
    /// </summary>
    public partial class CreateOrder : Window
    {
        SupplierTableAdapter supply = new SupplierTableAdapter();
        ProductTableAdapter product = new ProductTableAdapter();
        OrderTableAdapter order = new OrderTableAdapter();
        public CreateOrder()
        {
            InitializeComponent();

            ProductCmbx.ItemsSource = product.GetData();
            SupplyCmbx.ItemsSource = supply.GetData();

            ProductCmbx.DisplayMemberPath = "Product_name";
            SupplyCmbx.DisplayMemberPath = "User_ID";
        }

        private void CreateOrderAction(object sender, RoutedEventArgs e)
        {

            try
            {
                if (countTb.Text == string.Empty)
                {
                    MessageBox.Show("Введено пустое значение");
                    return;
                }

                if (dateDp.Text == string.Empty)
                {
                    MessageBox.Show("Дата не выбрана");
                    return;
                }

                if (!int.TryParse(countTb.Text, out int num))
                {
                    MessageBox.Show("В поле ввода нечисловое значение");
                    return;
                }

                object productCell = (ProductCmbx.SelectedItem as DataRowView).Row[0];
                object supplyCell = (SupplyCmbx.SelectedItem as DataRowView).Row[0];

                order.InsertQuery(Convert.ToInt32(supplyCell), Convert.ToInt32(productCell), num, dateDp.Text);
                Close();

            }
            catch { MessageBox.Show("Ошибка добавления"); }
        }
    }
}
