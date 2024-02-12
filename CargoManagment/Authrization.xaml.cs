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
    /// Логика взаимодействия для Authrization.xaml
    /// </summary>
    /// 
    public partial class Authrization : Window
    {
        UserTableAdapter UserTableAdapter = new UserTableAdapter();
        public Authrization()
        {
            InitializeComponent();
        }

        private void AuthAction(object sender, RoutedEventArgs e)
        {

            if ((loginTb.Text == String.Empty) || (passwdTb.Text == String.Empty))
            {
                MessageBox.Show("Введена пустая строка");
            }
            else
            {
                foreach (var item in UserTableAdapter.GetData())
                {
                    if (item.Login == loginTb.Text)
                    {
                        if (item.Password == passwdTb.Text)
                        {
                            switch (item.Role_ID)
                            {
                                case 1:
                                    Orders orders = new Orders();
                                    orders.Show();
                                    Close();
                                    return;
                                case 2:
                                    WorkerPanel worker = new WorkerPanel(item.Surname + " " + item.Name);
                                    worker.Show();
                                    Close();
                                    return;
                                case 3:
                                    SupplyPanel supply = new SupplyPanel(item.Surname + " " + item.Name);
                                    supply.Show();
                                    Close();
                                    return;
                                default:
                                    MessageBox.Show("Некорректная роль");
                                    return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Неверный пароль");
                            return;
                        }
                    }
                }
                MessageBox.Show("Пользоватлея не сущесвует");
                return;
            }
        }
    }
}
