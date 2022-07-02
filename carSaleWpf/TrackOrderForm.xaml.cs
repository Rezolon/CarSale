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
using MySql.Data.MySqlClient;

namespace carSaleWpf
{
    /// <summary>
    /// Логика взаимодействия для TrackOrderForm.xaml
    /// </summary>
    public partial class TrackOrderForm : Window
    {
        MySqlConnection connection = new MySqlConnection("Server=localhost;UserId=root;Password=1234;Database=carsale");
        MySqlCommand command;
        MySqlDataReader dataReader;
        MySqlDataAdapter dataAdapter;
        DataTable dataTable;


        public TrackOrderForm()
        {
            InitializeComponent();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            if (maskedTextBoxNumber.IsMaskFull == false)
            {
                return;
            }
            
            connection.Open();
            string query = $"SELECT `date` AS 'Дата',surname AS 'Фамилия',`name` AS 'Имя',patronomyc AS 'Отчество',price AS 'Цена' FROM orders WHERE id_order={numericUpDownOrderId.Value} AND phone = '{maskedTextBoxNumber.Text}'";
            dataAdapter = new MySqlDataAdapter(query, connection);
            dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            if (dataTable.Rows.Count == 0)
            {
                MessageBox.Show("Заказа с таким телефоном и номером не найден");
            }
            dataGridViewOrders.ItemsSource = dataTable.DefaultView;
            connection.Close();

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
