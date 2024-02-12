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
    /// Логика взаимодействия для WorkerPanel.xaml
    /// </summary>
    public partial class WorkerPanel : Window
    {
        ProductTableAdapter product = new ProductTableAdapter();
        public WorkerPanel(string name)
        {
            InitializeComponent();
            ProductDataGrid.ItemsSource = product.GetData();
            ProductCmbx.ItemsSource = product.GetData();
            ProductCmbx.DisplayMemberPath = "ID";

            nameTbl.Text = "Работник - " + name;
        }
        private void OpenOrderCreationWindow(object sender, RoutedEventArgs e)
        {
            CreateOrder win = new CreateOrder();
            win.Show();
        }

        private void SaveAction(object sender, RoutedEventArgs e)
        {
            try
            {
                object cell = (ProductCmbx.SelectedItem as DataRowView).Row[0];
                if (int.TryParse(countTb.Text, out int num))
                {
                    product.UpdateQuantityQuery(num, Convert.ToInt32(cell));
                    ProductDataGrid.ItemsSource = product.GetData();
                }
                else
                {
                    MessageBox.Show("В поле ввода нечисловое значение");
                    return;
                }
            } catch { MessageBox.Show("Ошибка изменения"); }
        }

        private void ProductSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                var products = product.GetData().Rows;
                int i = ((ComboBox)sender).SelectedIndex;
                countTb.Text = Convert.ToString(products[i][5]);
            }
            catch (IndexOutOfRangeException) { }
        }

        private void Increment(object sender, RoutedEventArgs e)
        {
            if(int.TryParse(countTb.Text, out int num))
            {
                countTb.Text = Convert.ToString(num + 1);
            } else
            {
                MessageBox.Show("В поле ввода нечисловое значение");
                return;
            }
        }

        private void Decrement(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(countTb.Text, out int num))
            {
                if (num > 0)
                {
                    countTb.Text = Convert.ToString(num - 1);
                }
            }
            else
            {
                MessageBox.Show("В поле ввода нечисловое значение");
                return;
            }
        }
    }
}
