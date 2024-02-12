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
    /// Логика взаимодействия для SupplyPanel.xaml
    /// </summary>
    public partial class SupplyPanel : Window
    {
        OrderTableAdapter order = new OrderTableAdapter();
        ProductTableAdapter product = new ProductTableAdapter();
        public SupplyPanel(string fio)
        {
            InitializeComponent();

            nameTbl.Text = "Поставщик - " + fio;

            OrderCmbx.ItemsSource = order.GetData();
            OrderDataGrid.ItemsSource = order.GetData();

            OrderCmbx.DisplayMemberPath = "ID";
        }

        private void SendOrderAction(object sender, RoutedEventArgs e)
        {
            try
            {
                if (OrderCmbx.Text == string.Empty)
                {
                    MessageBox.Show("ID заказа не выбран");
                }

                DataRow orderCell = (OrderCmbx.SelectedItem as DataRowView).Row;

                foreach (var item in product.GetData())
                {
                    if (item.ID == Convert.ToInt32(orderCell[2]))
                    {
                        product.UpdateQuantityQuery(item.Quantity_storage + Convert.ToInt32(orderCell[3]), item.ID);
                        order.DeleteQuery(Convert.ToInt32(orderCell[0]));
                        OrderDataGrid.ItemsSource = order.GetData();
                    }
                }
            } catch { MessageBox.Show("Ошибка выполенния"); }
        }
    }
}
