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
    /// Логика взаимодействия для Product.xaml
    /// </summary>
    public partial class Product : Window
    {
        ProductTableAdapter product = new ProductTableAdapter();
        public Product()
        {
            InitializeComponent();
            ProductDataGrid.ItemsSource = product.GetData();
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

        private void ProductAddAction(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((productNameTb.Text == string.Empty)
                    || (articleTb.Text == string.Empty)
                    || (countTb.Text == string.Empty)
                    || (factCountTb.Text == string.Empty)
                    || (descriptionTb.Text == string.Empty)
                    || (priceTb.Text == string.Empty)
                    )
                {
                    MessageBox.Show("Введено пустое значение");
                    return;
                }

                if (dateDp.Text == string.Empty)
                {
                    MessageBox.Show("Дата не выбрана");
                    return;
                }
                if (!int.TryParse(priceTb.Text, out int intprice)
                    || !int.TryParse(countTb.Text, out int intcount)
                    || !int.TryParse(factCountTb.Text, out int intdfactcount))
                {
                    MessageBox.Show("Невозможно преобразовать строковое значение из полей, подразумевающих числовой ввод");
                    return;
                }

                product.InsertQuery(productNameTb.Text, articleTb.Text, descriptionTb.Text, intprice, intcount, intdfactcount, dateDp.Text);
                ProductDataGrid.ItemsSource = product.GetData();
            }
            catch { MessageBox.Show("Ошибка добавления"); }
        }
        private void ProductSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                var products = product.GetData().Rows;
                int i = ((DataGrid)sender).SelectedIndex;
                productNameTb.Text = Convert.ToString(products[i][1]);
                articleTb.Text = Convert.ToString(products[i][2]);
                descriptionTb.Text = Convert.ToString(products[i][3]);
                priceTb.Text = Convert.ToString(products[i][4]);
                countTb.Text = Convert.ToString(products[i][5]);
                factCountTb.Text = Convert.ToString(products[i][6]);
                dateDp.Text = Convert.ToString(products[i][7]);
            }
            catch (IndexOutOfRangeException) { }
        }

        private void ProductEditAction(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((productNameTb.Text == string.Empty)
                    || (articleTb.Text == string.Empty)
                    || (countTb.Text == string.Empty)
                    || (factCountTb.Text == string.Empty)
                    || (descriptionTb.Text == string.Empty)
                    || (priceTb.Text == string.Empty)
                    )
                {
                    MessageBox.Show("Введено пустое значение");
                    return;
                }

                if (dateDp.Text == string.Empty)
                {
                    MessageBox.Show("Дата не выбрана");
                    return;
                }
                if (!int.TryParse(priceTb.Text, out int intprice)
                    || !int.TryParse(countTb.Text, out int intcount)
                    || !int.TryParse(factCountTb.Text, out int intdfactcount))
                {
                    MessageBox.Show("Невозможно преобразовать строковое значение из полей, подразумевающих числовой ввод");
                    return;
                }

                object id = (ProductDataGrid.SelectedItem as DataRowView).Row[0];

                product.UpdateQuery(productNameTb.Text, articleTb.Text, descriptionTb.Text, intprice, intcount, intdfactcount, dateDp.Text, Convert.ToInt32(id));
                ProductDataGrid.ItemsSource = product.GetData();

                productNameTb.Text = "";
                articleTb.Text = "";
                descriptionTb.Text = "";
                priceTb.Text = "";
                countTb.Text = "";
                factCountTb.Text = "";
                dateDp.Text = "";
            }
            catch { MessageBox.Show("Ошибка изменения"); }
        }

        private void ProductDeleteAction(object sender, RoutedEventArgs e)
        {
            try
            {
                object id = (ProductDataGrid.SelectedItem as DataRowView).Row[0];
                product.DeleteQuery(Convert.ToInt32(id));
                ProductDataGrid.ItemsSource = product.GetData();


                productNameTb.Text = "";
                articleTb.Text = "";
                descriptionTb.Text = "";
                priceTb.Text = "";
                countTb.Text = "";
                factCountTb.Text = "";
                dateDp.Text = "";
            }
            catch { MessageBox.Show("Ошибка удаления"); }
        }
    }
}
