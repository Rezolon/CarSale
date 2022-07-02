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
using Word = Microsoft.Office.Interop.Word;

namespace carSaleWpf
{

    /// <summary>
    /// Логика взаимодействия для AdminMenuForm.xaml
    /// </summary>
    public partial class AdminMenuForm : Window
    {
        MySqlConnection connection = new MySqlConnection("Server=localhost;UserId=root;Password=1234;Database=carsale");
        MySqlCommand command;
        MySqlDataReader dataReader;
        MySqlDataAdapter dataAdapter;
        DataTable dataTable;
        AuthorizationForm authorizationForm;

        public AdminMenuForm(AuthorizationForm authorizationForm)
        {
            InitializeComponent();
            this.authorizationForm = authorizationForm;
        }

        private void AdminMenuForm_Loaded(object sender, RoutedEventArgs e)
        {
            comboBoxAllTable.SelectedIndex = 0;
        }

        private void comboBoxAllTable_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            buttonAdd.IsEnabled = true;
            buttonUpdate.IsEnabled = true;
            buttonDelete.IsEnabled = true;
            buttonCreateExcel.IsEnabled = false;
            switch (comboBoxAllTable.SelectedIndex)
            {
                case 0:
                    buttonAdd.IsEnabled = false;
                    buttonUpdate.IsEnabled = false;
                    buttonDelete.IsEnabled = false;
                    buttonCreateExcel.IsEnabled = true;
                    break;
            }

            fillDataGrid();
        }

        public void fillDataGrid()
        {
           
            string query = "";
            switch (comboBoxAllTable.SelectedIndex)
            {
                case 0:
                    connection.Open();
                    query = $"SELECT orders.id_order,complektacii.`id_complektacii`,marki.marki AS 'Марка', modeli.`name` AS 'Модель', orders.`date` AS 'Дата продажи', orders.surname AS 'Фамилия'," +
                        $"orders.`name` AS 'Имя', orders.patronomyc AS 'Отчество', orders.phone AS 'Телефон', `car(nalichiye)`.№PTC AS 'ПТС', `car(nalichiye)`.color AS 'Цвет', " +
                        $"`car(nalichiye)`.`release` AS 'Год выпуска' FROM orders, `car(nalichiye)`, marki, modeli, complektacii WHERE orders.id_car=`car(nalichiye)`.id_car " +
                        $"AND `car(nalichiye)`.id_model=modeli.id_model AND modeli.id_marka=marki.id_marki AND complektacii.`id_car`=`car(nalichiye)`.`id_car`";
                    dataAdapter = new MySqlDataAdapter(query, connection);
                    dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    dataGridViewAllTable.ItemsSource = dataTable.DefaultView;
                    dataGridViewAllTable.Columns[0].Visibility = Visibility.Collapsed;
                    dataGridViewAllTable.Columns[1].Visibility = Visibility.Collapsed;
                    connection.Close();
                    break;
                case 1:
                    connection.Open();
                    query = $"SELECT `car(nalichiye)`.`id_car`, complektacii.id_complektacii ,marki.marki AS 'Марка', modeli.name AS 'Модель', kyzov.name_kyzov AS 'Кузов', privod.name_privod AS 'Привод', mest AS 'Кол-во мест', `dvigatel'`.`name_dvigatel'` AS 'ДВС', `ob\"yem dvigatelya`.`name_Ob\"yem dvigatelya` AS 'Объем ДВС', `loshadinyye sily` AS 'ЛС', kpp.name_KPP AS 'КПП', `car(nalichiye)`.color AS 'Цвет', modeli.`price` AS 'Цена'  " +
                        $"FROM complektacii, modeli, kyzov, privod, `dvigatel'`, `ob\"yem dvigatelya`, kpp, marki, `car(nalichiye)`" +
                        $" WHERE complektacii.id_car=`car(nalichiye)`.id_car AND complektacii.id_kyzov=kyzov.id_kyzov AND complektacii.id_privod=privod.id_privod AND complektacii.`id_dvigatel'`=`dvigatel'`.`id_dvigatel'` AND complektacii.`id_Ob\"yem dvigatelya` =`ob\"yem dvigatelya`.`id_Ob\"yem dvigatelya` AND complektacii.id_KPP=kpp.id_KPP AND marki.id_marki = modeli.id_marka AND `car(nalichiye)`.buying =0 AND `car(nalichiye)`.`id_model`=modeli.`id_model` GROUP BY `car(nalichiye)`.id_car";
                    dataAdapter = new MySqlDataAdapter(query, connection);
                    dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    dataGridViewAllTable.ItemsSource = dataTable.DefaultView;
                    dataGridViewAllTable.Columns[0].Visibility = Visibility.Collapsed;
                    dataGridViewAllTable.Columns[1].Visibility = Visibility.Collapsed;
                    connection.Close();
                    break;
                case 2:
                    connection.Open();
                    query = $"SELECT `id_dvigatel'`,`name_dvigatel'` AS 'Название', price AS 'Цена' FROM `dvigatel'`";
                    dataAdapter = new MySqlDataAdapter(query, connection);
                    dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    dataGridViewAllTable.ItemsSource = dataTable.DefaultView;
                    dataGridViewAllTable.Columns[0].Visibility = Visibility.Collapsed;
                    connection.Close();
                    break;
                case 3:
                    connection.Open();
                    query = $"SELECT id_KPP,name_KPP AS 'Название', price AS 'Цена' FROM kpp";
                    dataAdapter = new MySqlDataAdapter(query, connection);
                    dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    dataGridViewAllTable.ItemsSource = dataTable.DefaultView;
                    dataGridViewAllTable.Columns[0].Visibility = Visibility.Collapsed;
                    connection.Close();
                    break;
                case 4:
                    connection.Open();
                    query = $"SELECT id_option,name AS 'Название', price AS 'Цена' FROM dop_options";
                    dataAdapter = new MySqlDataAdapter(query, connection);
                    dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    dataGridViewAllTable.ItemsSource = dataTable.DefaultView;
                    dataGridViewAllTable.Columns[0].Visibility = Visibility.Collapsed;
                    connection.Close();
                    break;
                case 5:
                    connection.Open();
                    query = $"SELECT id_marki,marki AS 'Название' FROM marki";
                    dataAdapter = new MySqlDataAdapter(query, connection);
                    dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    dataGridViewAllTable.ItemsSource = dataTable.DefaultView;
                    dataGridViewAllTable.Columns[0].Visibility = Visibility.Collapsed;
                    connection.Close();
                    break;
                case 6:
                    connection.Open();
                    query = $"SELECT id_model, `name` AS 'Название', price AS 'Цена', (SELECT marki.marki FROM marki WHERE marki.id_marki = modeli.id_marka) AS 'Модель', photo AS 'Фото' FROM modeli;";
                    dataAdapter = new MySqlDataAdapter(query, connection);
                    dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    dataGridViewAllTable.ItemsSource = dataTable.DefaultView;
                    dataGridViewAllTable.Columns[0].Visibility = Visibility.Collapsed;
                    connection.Close();
                    break;
            }
            dataGridViewAllTable.SelectedIndex = 0;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            
           
            switch (comboBoxAllTable.SelectedIndex)
            {
                case 1:
                    AddAndUpdateCarForm addAndUpdateCarForm = new AddAndUpdateCarForm(this);
                    addAndUpdateCarForm.ShowDialog();
                    break;
                case 2:
                    AddAndUpdateDvs addAndUpdateDvs = new AddAndUpdateDvs(this, "dvigatel'", "id_dvigatel'", "name_dvigatel'");
                    addAndUpdateDvs.ShowDialog();
                    break;
                case 3:
                    AddAndUpdateDvs addAndUpdateKpp = new AddAndUpdateDvs(this, "kpp", "id_KPP", "name_KPP");
                    addAndUpdateKpp.ShowDialog();
                    break;
                case 4:
                    AddAndUpdateDvs addAndUpdateDopOption = new AddAndUpdateDvs(this, "dop_options", "id_option", "name");
                    addAndUpdateDopOption.ShowDialog();
                    break;
                case 5:
                    AddAndUpdateDvs addAndUpdateMarki = new AddAndUpdateDvs(this, "marki", "id_marki", "marki");
                    addAndUpdateMarki.ShowDialog();
                    break;
                case 6:
                    AddAndUpdateModelForm addAndUpdateModelForm = new AddAndUpdateModelForm(this);
                    addAndUpdateModelForm.ShowDialog();
                    break;
            }
        }

        private void AdminMenuForm_Load(object sender, EventArgs e)
        {

        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            
            var currentRow = dataTable.Rows[dataGridViewAllTable.Items.IndexOf(dataGridViewAllTable.SelectedItem)].ItemArray;
           
            switch (comboBoxAllTable.SelectedIndex)
            {
                case 1:
                    AddAndUpdateCarForm addAndUpdateCarForm = new AddAndUpdateCarForm(this, currentRow[0].ToString());
                    addAndUpdateCarForm.ShowDialog();
                    break;
                case 2:
                    AddAndUpdateDvs addAndUpdateDvs = new AddAndUpdateDvs(this, currentRow[0].ToString(), "dvigatel'", "id_dvigatel'", "name_dvigatel'");
                    addAndUpdateDvs.ShowDialog();
                    break;
                case 3:
                    AddAndUpdateDvs addAndUpdateKpp = new AddAndUpdateDvs(this, currentRow[0].ToString(), "kpp", "id_KPP", "name_KPP");
                    addAndUpdateKpp.ShowDialog();
                    break;
                case 4:
                    AddAndUpdateDvs addAndUpdateDopOption = new AddAndUpdateDvs(this, currentRow[0].ToString(), "dop_options", "id_option", "name");
                    addAndUpdateDopOption.ShowDialog();
                    break;
                case 5:
                    AddAndUpdateDvs addAndUpdateMarki = new AddAndUpdateDvs(this, currentRow[0].ToString(), "marki", "id_marki", "marki");
                    addAndUpdateMarki.ShowDialog();
                    break;
                case 6:
                    AddAndUpdateModelForm addAndUpdateModelForm = new AddAndUpdateModelForm(this, currentRow[0].ToString());
                    addAndUpdateModelForm.ShowDialog();
                    break;
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы точно хотите удалить эту запись ?", "", MessageBoxButton.YesNo);
            if (result != MessageBoxResult.Yes)
            {
                return;
            }
            
           
            string query = "";
            var currentRow = dataTable.Rows[dataGridViewAllTable.Items.IndexOf(dataGridViewAllTable.SelectedItem)].ItemArray;
            switch (comboBoxAllTable.SelectedIndex)
            {
                case 1:
                    connection.Open();
                    query = $"DELETE FROM complektacii WHERE id_car ={currentRow[0].ToString()}";
                    command = new MySqlCommand(query, connection);
                    command.ExecuteNonQuery();

                    
                    query = $"DELETE FROM `car(nalichiye)` WHERE id_car ={currentRow[0].ToString()}";
                    command = new MySqlCommand(query, connection);
                    command.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("Автомобиль успешно удален!");
                    break;
                case 2:
                    connection.Open();
                    query = $"DELETE FROM `dvigatel'` WHERE `id_dvigatel'` ={currentRow[0].ToString()}";
                    command = new MySqlCommand(query, connection);
                    command.ExecuteNonQuery();

                    connection.Close();
                    MessageBox.Show("Двигатель успешно удален!");
                    break;
                case 3:
                    connection.Open();
                    query = $"DELETE FROM kpp WHERE id_KPP ={currentRow[0].ToString()}";
                    command = new MySqlCommand(query, connection);
                    command.ExecuteNonQuery();

                    connection.Close();
                    MessageBox.Show("КПП успешно удалена!");
                    break;
                case 4:
                    connection.Open();
                    query = $"DELETE FROM dop_options WHERE id_option ={currentRow[0].ToString()}";
                    command = new MySqlCommand(query, connection);
                    command.ExecuteNonQuery();

                    connection.Close();
                    MessageBox.Show("Опция успешно удалена!");
                    break;
                case 5:
                    connection.Open();
                    query = $"DELETE FROM marki WHERE id_marki ={currentRow[0].ToString()}";
                    command = new MySqlCommand(query, connection);
                    command.ExecuteNonQuery();

                    connection.Close();
                    MessageBox.Show("Марка успешно удалена!");
                    break;
                case 6:
                    connection.Open();
                    query = $"DELETE FROM modeli WHERE id_model ={currentRow[0].ToString()}";
                    command = new MySqlCommand(query, connection);
                    command.ExecuteNonQuery();

                    connection.Close();
                    MessageBox.Show("Модель успешно удалена!");
                    break;
            }

            fillDataGrid();
            
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
            authorizationForm.Show();
        }

        private void buttonCreateExcel_Click(object sender, EventArgs e)
        {

            var currentRow = dataTable.Rows[dataGridViewAllTable.Items.IndexOf(dataGridViewAllTable.SelectedItem)].ItemArray;

          
            int lastPrice = 0;
          
            Word.Application WordApp = new Word.Application();
           
            object missing = Type.Missing;
            Word._Document WordDoc = WordApp.Documents.Add(
                ref missing, ref missing, ref missing, ref missing);
            object start = 0, end = 0;
           
            Word.Range rng = WordDoc.Range(ref start, ref end);
            Word.Table _table = WordDoc.Tables.Add(rng, 14, 3, missing, missing);
            WordDoc.Range(_table.Cell(1, 1).Range.Start, _table.Cell(1, 3).Range.End).Cells.Merge();
            WordDoc.Range(_table.Cell(2, 1).Range.Start, _table.Cell(2, 3).Range.End).Cells.Merge();
            WordDoc.Range(_table.Cell(3, 1).Range.Start, _table.Cell(3, 3).Range.End).Cells.Merge();
            _table.Cell(1, 1).Range.Text = "ЧЕК ЗАКАЗА №" + currentRow[0].ToString(); ;
            _table.Cell(2, 1).Range.Text = "Дата: " + DateTime.Now.ToString().Split(' ')[0];
            _table.Cell(3, 1).Range.Text = "Заказчик: " + currentRow[5].ToString() + " " + currentRow[6].ToString() + " " + currentRow[7].ToString(); ;
            connection.Open();
            string query = "SELECT marki.marki AS 'Марка', modeli.name AS 'Модель', kyzov.name_kyzov AS 'Кузов',`car(nalichiye)`.`№kyzova`, privod.name_privod AS 'Привод', mest AS 'Кол-во мест', `dvigatel'`.`name_dvigatel'` AS 'ДВС', `ob\"yem dvigatelya`.`name_Ob\"yem dvigatelya` AS 'Объем ДВС', `car(nalichiye)`.`№dvigatel`, `loshadinyye sily` AS 'Л.С.', kpp.name_KPP AS 'КПП',`car(nalichiye)`.release AS 'Выпуск', `car(nalichiye)`.color AS 'Цвет', modeli.`price` AS 'Цена'  FROM complektacii, modeli, kyzov, privod, `dvigatel'`, `ob\"yem dvigatelya`, kpp, marki, `car(nalichiye)` " +
    $"WHERE `car(nalichiye)`.id_model=modeli.id_model AND complektacii.id_kyzov=kyzov.id_kyzov AND complektacii.id_privod=privod.id_privod AND complektacii.`id_dvigatel'`=`dvigatel'`.`id_dvigatel'` AND complektacii.`id_Ob\"yem dvigatelya` =`ob\"yem dvigatelya`.`id_Ob\"yem dvigatelya` AND complektacii.id_KPP=kpp.id_KPP AND marki.id_marki = modeli.id_marka AND complektacii.id_car=`car(nalichiye)`.id_car  AND `car(nalichiye)`.id_car = {currentRow[1].ToString()}";
            command = new MySqlCommand(query, connection);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                _table.Cell(6, 1).Range.Text = "Автомобиль: ";
                _table.Cell(6, 2).Range.Text = dataReader[0].ToString() + " " + dataReader[1].ToString() + " " + dataReader[2].ToString() + " " + dataReader[4].ToString();
                _table.Cell(7, 1).Range.Text = "№ Кузова: ";
                _table.Cell(7, 2).Range.Text = dataReader[3].ToString();
                _table.Cell(8, 1).Range.Text = "ДВС: ";
                _table.Cell(8, 2).Range.Text = dataReader[6].ToString() + " " + dataReader[7].ToString() + " " + dataReader[8].ToString() + " " + dataReader[9].ToString();
                _table.Cell(9, 1).Range.Text = "КПП: ";
                _table.Cell(9, 2).Range.Text = dataReader[10].ToString();
                _table.Cell(10, 1).Range.Text = "Год выпуска: ";
                _table.Cell(10, 2).Range.Text = dataReader[11].ToString();
                _table.Cell(11, 1).Range.Text = "Цвет: ";
                _table.Cell(11, 2).Range.Text = dataReader[12].ToString();
                _table.Cell(12, 1).Range.Text = "Цена: ";
                _table.Cell(12, 3).Range.Text = dataReader[13].ToString();
                lastPrice = Convert.ToInt32(dataReader[13].ToString());
            }
            dataReader.Close();
            _table.Cell(14, 1).Range.Text = "Доп. опции:";
            _table.Cell(14, 2).Range.Text = "Название";
            _table.Cell(14, 3).Range.Text = "Цена";

            int x = 15;
            query = $"SELECT * FROM carsale.dop_options WHERE id_option IN (SELECT  id_option FROM orders_options WHERE id_order = {currentRow[1].ToString()});";
            command = new MySqlCommand(query, connection);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                _table.Rows.Add();
                _table.Cell(x, 2).Range.Text = dataReader[1].ToString();
                _table.Cell(x, 3).Range.Text = dataReader[2].ToString();
                lastPrice += Convert.ToInt32(dataReader[2].ToString());
                x++;
            }
            dataReader.Close();

            x++;
            _table.Rows.Add();
            _table.Cell(x, 1).Range.Text = "Общая Стоимость:";
            _table.Cell(x, 3).Range.Text = lastPrice.ToString();

            connection.Close();
           
            _table.Range.Font.Size = 12;
            _table.Columns.DistributeWidth();
            _table.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
            _table.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleDouble;
           
            WordApp.Visible = true;
            
        }
    }
}
