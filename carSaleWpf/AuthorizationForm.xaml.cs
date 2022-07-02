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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace carSaleWpf
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationForm.xaml
    /// </summary>
    public partial class AuthorizationForm : Window
    {
        
        MySqlConnection connection = new MySqlConnection("Server=localhost;UserId=root;Password=1234;Database=carsale");
        MySqlCommand command;
        MySqlDataReader dataReader;

        public AuthorizationForm()
        {
            InitializeComponent();
        }

        private void buttonAuthorization_Click(object sender, EventArgs e)
        {
            
            if (textBoxLogin.Text == "")
            {
                MessageBox.Show("Введите логин!");
                return;
            }
            if (textBoxPassword.Password == "")
            {
                MessageBox.Show("Введите пароль!");
                return;
            }
            connection.Open();
            string query = "SELECT * FROM users";
            command = new MySqlCommand(query, connection);
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                if (dataReader[1].ToString() == textBoxLogin.Text && dataReader[2].ToString() == textBoxPassword.Password)
                {
                    string typeUser = dataReader[3].ToString();
                    dataReader.Close();
                    connection.Close();

                    this.Hide();
                    if (typeUser == "Admin")
                    {
                        MessageBox.Show("Вы вошли как Администратор!");
                        AdminMenuForm adminMenuForm = new AdminMenuForm(this);
                        adminMenuForm.Show();
                    }
                    else
                    {
                        MessageBox.Show("Вы вошли как Сотрудник!");
                        CarListForm carListForm = new CarListForm(this);
                        carListForm.Show();
                    }

                    textBoxLogin.Text = "";
                    textBoxPassword.Password = "";

                    return;
                }
            }
            MessageBox.Show("Не корректные данные!");
            dataReader.Close();
            connection.Close();
        }


        private void checkBoxShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            
            if (checkBoxShowPassword.IsChecked == true)
            {
                textBoxPasswordShow.Text = textBoxPassword.Password;
                textBoxPassword.Visibility = Visibility.Collapsed;
                textBoxPasswordShow.Visibility = Visibility.Visible;
                textBoxPasswordShow.Focus();
            }
            else
            {
                textBoxPassword.Password = textBoxPasswordShow.Text;
                textBoxPasswordShow.Visibility = Visibility.Collapsed;
                textBoxPassword.Visibility = Visibility.Visible;
                textBoxPassword.Focus();
            }
        }

        private void buttonAuthorization_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void buttonAuthorization_Click()
        {

        }
    }
}

