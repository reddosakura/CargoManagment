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
using CargoManagment.CargoManagmentDataSetTableAdapters;

namespace CargoManagment
{
    /// <summary>
    /// Логика взаимодействия для Orders.xaml
    /// </summary>
    public partial class Orders : Window
    {
        OrderTableAdapter orderTableAdapter = new OrderTableAdapter();
        public Orders()
        {
            InitializeComponent();
            OrdersDataGrid.ItemsSource = orderTableAdapter.GetData();
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
    }
}
