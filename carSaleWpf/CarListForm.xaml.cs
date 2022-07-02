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
    /// Логика взаимодействия для CarListForm.xaml
    /// </summary>
    public partial class CarListForm : Window
    {
        MySqlConnection connection = new MySqlConnection("Server=localhost;UserId=root;Password=1234;Database=carsale");
        MySqlCommand command;
        MySqlDataReader dataReader;
        MySqlDataAdapter dataAdapter;
        DataTable dataTable;
        AuthorizationForm authorizationForm;

        public CarListForm(AuthorizationForm authorizationForm)
        {
            InitializeComponent();
            this.authorizationForm = authorizationForm;
            //fillDataGRidCar();
        }

        private void buttonInfo_Click(object sender, EventArgs e)
        {
            var currentRow = dataTable.Rows[dataGridViewCarList.Items.IndexOf(dataGridViewCarList.SelectedItem)].ItemArray;
            CarInfoAndOrdering carInfoAndOrdering = new CarInfoAndOrdering(currentRow[0].ToString(), currentRow[1].ToString(), currentRow[2].ToString(), this);
            carInfoAndOrdering.ShowDialog();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
            authorizationForm.Show();
        }

        public void fillDataGRidCar()
        {
           
            connection.Open();
            string query = $"SELECT `car(nalichiye)`.`id_car`, complektacii.id_complektacii, marki.id_marki ,marki.marki AS 'Марка', modeli.name AS 'Модель', kyzov.name_kyzov AS 'Кузов', privod.name_privod AS 'Привод', mest AS 'Кол-во мест', `dvigatel'`.`name_dvigatel'` AS 'ДВС',`loshadinyye sily` AS 'ЛС', `ob\"yem dvigatelya`.`name_Ob\"yem dvigatelya` AS 'Объем ДВС', kpp.name_KPP AS 'КПП', `car(nalichiye)`.color AS 'Цвет', modeli.`price` AS 'Цена'  " +
                $"FROM complektacii, modeli, kyzov, privod, `dvigatel'`, `ob\"yem dvigatelya`, kpp, marki, `car(nalichiye)`" +
                $" WHERE complektacii.id_car=`car(nalichiye)`.id_car AND complektacii.id_kyzov=kyzov.id_kyzov AND complektacii.id_privod=privod.id_privod AND complektacii.`id_dvigatel'`=`dvigatel'`.`id_dvigatel'` AND complektacii.`id_Ob\"yem dvigatelya` =`ob\"yem dvigatelya`.`id_Ob\"yem dvigatelya` AND complektacii.id_KPP=kpp.id_KPP AND marki.id_marki = modeli.id_marka AND `car(nalichiye)`.buying =0 AND `car(nalichiye)`.`id_model`=modeli.`id_model` GROUP BY `car(nalichiye)`.id_car";
            dataAdapter = new MySqlDataAdapter(query, connection);
            dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            dataGridViewCarList.ItemsSource = dataTable.DefaultView;
            dataGridViewCarList.Columns[0].Visibility = Visibility.Collapsed;
            dataGridViewCarList.Columns[1].Visibility = Visibility.Collapsed;
            dataGridViewCarList.Columns[2].Visibility = Visibility.Collapsed;
            try
            {
                hideColumn();
            }
            catch { }
            connection.Close();
            dataGridViewCarList.SelectedIndex = 0;
        }

        private void CarListForm_Loaded(object sender, RoutedEventArgs e)
        {
            fillDataGRidCar();
        }

        bool hideColumn()
        {
            try
            {
                dataGridViewCarList.Columns[0].Visibility = Visibility.Collapsed;
                dataGridViewCarList.Columns[1].Visibility = Visibility.Collapsed;
                dataGridViewCarList.Columns[2].Visibility = Visibility.Collapsed;
                return false;
            }
            catch 
            {
                return true;
            }
        }

 

        private void buttonTrackOrder_Click(object sender, EventArgs e)
        {
            TrackOrderForm trackOrderForm = new TrackOrderForm();
            trackOrderForm.ShowDialog();
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
