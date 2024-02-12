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
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Registration : Window
    {

        UserTableAdapter UserTableAdapter = new UserTableAdapter();
        public Registration()
        {
            InitializeComponent();
        }

        private void RegistarionAction(object sender, RoutedEventArgs e)
        {
            if ((loginTb.Text == string.Empty) || 
                (passwdTb.Text == string.Empty) ||
                (fioTb.Text == string.Empty))
            {
                MessageBox.Show("Введена пустая строка");
            } else
            {
                foreach (var user in UserTableAdapter.GetData()) 
                {
                    if (user.Login ==  loginTb.Text)
                    {
                        MessageBox.Show("Такой логин уже занят");
                        return;
                    }
                }

                if (fioTb.Text.Split(' ').Length != 2) 
                {
                    MessageBox.Show("К сожалению в системе ФИО может состоять из 2х слов");
                    return;
                }

                if (passwdTb.Text.Length < 8)
                {
                    MessageBox.Show("Пароль должен состоять минимум из 8 символов");
                    return;
                }

                string name = fioTb.Text.Split(' ')[1];
                string surname = fioTb.Text.Split(' ')[0];

                UserTableAdapter.InsertQuery(surname, name, loginTb.Text, passwdTb.Text, 3);

                SupplyPanel win = new SupplyPanel(surname + " " + name);
                win.Show();
                Close();
            }
        }
    }
}
