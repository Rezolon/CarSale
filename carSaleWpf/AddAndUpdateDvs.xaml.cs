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
using MySql.Data.MySqlClient;

namespace carSaleWpf
{
    /// <summary>
    /// Логика взаимодействия для AddAndUpdateDvs.xaml
    /// </summary>
    public partial class AddAndUpdateDvs : Window
    {
        AdminMenuForm adminMenuForm;
        MySqlConnection connection = new MySqlConnection("Server=localhost;UserId=root;Password=1234;Database=carsale");
        MySqlCommand command;
        MySqlDataReader dataReader;
        string dvsId;
        string tableName;
        string id;
        string name;

       
        public AddAndUpdateDvs(AdminMenuForm adminMenuForm, string tableName, string id, string name)
        {
            InitializeComponent();
            this.adminMenuForm = adminMenuForm;
            this.tableName = tableName;
            this.id = id;
            this.name = name;
            if (tableName == "marki")
            {
                label6.Visibility = Visibility.Collapsed;
                numericUpDownPrice.Visibility = Visibility.Collapsed;
            }
        }
       
        public AddAndUpdateDvs(AdminMenuForm adminMenuForm, string dvsId, string tableName, string id, string name)
        {
            InitializeComponent();
            this.adminMenuForm = adminMenuForm;
            this.dvsId = dvsId;
            this.adminMenuForm = adminMenuForm;
            this.tableName = tableName;
            this.id = id;
            this.name = name;
            buttonAdd.Content = "Обновить";
            if (tableName == "marki")
            {
                label6.Visibility = Visibility.Collapsed;
                numericUpDownPrice.Visibility = Visibility.Collapsed;
            }
            connection.Open();

            string query = $"SELECT * FROM `{tableName}` WHERE `{id}`={dvsId}";
            command = new MySqlCommand(query, connection);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                textBoxName.Text = dataReader[1].ToString();
                if (tableName != "marki")
                {
                    numericUpDownPrice.Value = Convert.ToInt32(dataReader[2].ToString());
                }
            }
            dataReader.Close();
            connection.Close();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {

            if (textBoxName.Text == "")
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }

            string query = "";
            if (buttonAdd.Content == "Обновить")
            {
               
                connection.Open();
                if (tableName == "marki")
                {
                    query = $"UPDATE `{tableName}` SET `{name}` = '{textBoxName.Text}' WHERE `{id}`={dvsId}";
                }
                else
                {
                    query = $"UPDATE `{tableName}` SET `{name}` = '{textBoxName.Text}', price = {numericUpDownPrice.Value} WHERE `{id}`={dvsId}";
                }
                command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();

                MessageBox.Show("Успешно обновлен!");
                connection.Close();
            }
            else
            {
                
                connection.Open();

                query = $"SELECT MAX(`{id}`)+1 FROM `{tableName}`";
                string maxDvsId = "";
                command = new MySqlCommand(query, connection);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    maxDvsId = dataReader[0].ToString();
                }
                dataReader.Close();

                if (tableName == "marki")
                {
                    query = $"INSERT `{tableName}` VALUE ({maxDvsId}, '{textBoxName.Text}')";
                }
                else
                {
                    query = $"INSERT `{tableName}` VALUE ({maxDvsId}, '{textBoxName.Text}', {numericUpDownPrice.Value})";
                }

                command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();

                connection.Close();

                MessageBox.Show("Успешно добавлен!");
            }

            this.Close();
            adminMenuForm.fillDataGrid();
        }

       

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            adminMenuForm.fillDataGrid();
        }

       
    }
}
