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
    /// Логика взаимодействия для AddAndUpdateCarForm.xaml
    /// </summary>
    public partial class AddAndUpdateCarForm : Window
    {
        MySqlConnection connection = new MySqlConnection("Server=localhost;UserId=root;Password=1234;Database=carsale");
        MySqlCommand command;
        MySqlDataReader dataReader;
        List<string> markiId = new List<string>();
        List<string> modelId = new List<string>();
        List<string> kuzovId = new List<string>();
        List<string> privodId = new List<string>();
        List<string> dvsId = new List<string>();
        List<string> vDvsId = new List<string>();
        List<string> kppId = new List<string>();
        AdminMenuForm adminMenuForm;
        string carId;
        string selectedModelId = "";

        //Конструктор для добавления
        public AddAndUpdateCarForm(AdminMenuForm adminMenuForm)
        {
            InitializeComponent();

            this.adminMenuForm = adminMenuForm;

            connection.Open();
            string query = $"SELECT * FROM marki";
            command = new MySqlCommand(query, connection);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                markiId.Add(dataReader[0].ToString());
                comboBoxMarka.Items.Add(dataReader[1].ToString());
            }
            dataReader.Close();

            query = $"SELECT * FROM kyzov";
            command = new MySqlCommand(query, connection);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                kuzovId.Add(dataReader[0].ToString());
                comboBoxKuzov.Items.Add(dataReader[1].ToString());
            }
            dataReader.Close();

            query = $"SELECT * FROM privod";
            command = new MySqlCommand(query, connection);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                privodId.Add(dataReader[0].ToString());
                comboBoxPrivod.Items.Add(dataReader[1].ToString());
            }
            dataReader.Close();

            query = $"SELECT * FROM `dvigatel'`";
            command = new MySqlCommand(query, connection);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                dvsId.Add(dataReader[0].ToString());
                comboBoxDvs.Items.Add(dataReader[1].ToString());
            }
            dataReader.Close();

            query = $"SELECT * FROM `ob\"yem dvigatelya`";
            command = new MySqlCommand(query, connection);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                vDvsId.Add(dataReader[0].ToString());
                comboBoxVDvs.Items.Add(dataReader[1].ToString());
            }
            dataReader.Close();

            query = $"SELECT * FROM `kpp`";
            command = new MySqlCommand(query, connection);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                kppId.Add(dataReader[0].ToString());
                comboBoxKPP.Items.Add(dataReader[1].ToString());
            }
            dataReader.Close();

            connection.Close();
        }

        //Конструктор для обновления
        public AddAndUpdateCarForm(AdminMenuForm adminMenuForm, string carId)
        {
            InitializeComponent();

            this.adminMenuForm = adminMenuForm;
            this.carId = carId;
            string query = "";
            buttonAdd.Content = "Обновить";

            connection.Open();


            query = $"SELECT * FROM marki";
            command = new MySqlCommand(query, connection);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                markiId.Add(dataReader[0].ToString());
                comboBoxMarka.Items.Add(dataReader[1].ToString());
            }
            dataReader.Close();

            query = $"SELECT * FROM modeli";
            command = new MySqlCommand(query, connection);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                modelId.Add(dataReader[0].ToString());
                comboBoxModel.Items.Add(dataReader[1].ToString());
            }
            dataReader.Close();

            query = $"SELECT * FROM kyzov";
            command = new MySqlCommand(query, connection);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                kuzovId.Add(dataReader[0].ToString());
                comboBoxKuzov.Items.Add(dataReader[1].ToString());
            }
            dataReader.Close();

            query = $"SELECT * FROM privod";
            command = new MySqlCommand(query, connection);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                privodId.Add(dataReader[0].ToString());
                comboBoxPrivod.Items.Add(dataReader[1].ToString());
            }
            dataReader.Close();

            query = $"SELECT * FROM `dvigatel'`";
            command = new MySqlCommand(query, connection);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                dvsId.Add(dataReader[0].ToString());
                comboBoxDvs.Items.Add(dataReader[1].ToString());
            }
            dataReader.Close();

            query = $"SELECT * FROM `ob\"yem dvigatelya`";
            command = new MySqlCommand(query, connection);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                vDvsId.Add(dataReader[0].ToString());
                comboBoxVDvs.Items.Add(dataReader[1].ToString());
            }
            dataReader.Close();

            query = $"SELECT * FROM `kpp`";
            command = new MySqlCommand(query, connection);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                kppId.Add(dataReader[0].ToString());
                comboBoxKPP.Items.Add(dataReader[1].ToString());
            }
            dataReader.Close();

            query = $"SELECT * FROM `car(nalichiye)` WHERE id_car={carId}";
            command = new MySqlCommand(query, connection);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                selectedModelId = dataReader[1].ToString();
                textBoxKuzov.Text = dataReader[2].ToString();
                textBoxDvs.Text = dataReader[3].ToString();
                textBoxPts.Text = dataReader[4].ToString();
                textBoxColor.Text = dataReader[5].ToString();
                numericUpDownDate.Value = Convert.ToInt32(dataReader[6].ToString());
            }
            dataReader.Close();

            int selectedMarka = 0;
            query = $"SELECT id_marki FROM `car(nalichiye)`, marki,modeli WHERE `car(nalichiye)`.`id_model`=modeli.`id_model` AND modeli.`id_marka`=marki.`id_marki` AND `car(nalichiye)`.id_car = {carId} GROUP BY marki.id_marki";
            command = new MySqlCommand(query, connection);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                selectedMarka = markiId.IndexOf(dataReader[0].ToString());
            }
            dataReader.Close();

            query = $"SELECT * FROM complektacii WHERE id_car={carId}";
            command = new MySqlCommand(query, connection);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                comboBoxKuzov.SelectedIndex = kuzovId.IndexOf(dataReader[2].ToString());
                comboBoxPrivod.SelectedIndex = privodId.IndexOf(dataReader[3].ToString());
                numericUpDownMest.Value = Convert.ToInt32(dataReader[4].ToString());
                comboBoxDvs.SelectedIndex = dvsId.IndexOf(dataReader[5].ToString());
                comboBoxVDvs.SelectedIndex = vDvsId.IndexOf(dataReader[6].ToString());
                numericUpDownLs.Value = Convert.ToInt32(dataReader[7].ToString());
                comboBoxKPP.SelectedIndex = kppId.IndexOf(dataReader[8].ToString());
            }
            dataReader.Close();

            connection.Close();
            comboBoxMarka.SelectedIndex = selectedMarka;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {

            if (comboBoxDvs.SelectedIndex == -1 || comboBoxKPP.SelectedIndex == -1 || comboBoxKuzov.SelectedIndex == -1 || comboBoxMarka.SelectedIndex == -1 || comboBoxModel.SelectedIndex == -1 ||
                comboBoxPrivod.SelectedIndex == -1 || comboBoxVDvs.SelectedIndex == -1 || textBoxColor.Text == "" || textBoxDvs.Text == "" || textBoxKuzov.Text == "" || textBoxPts.Text == "")
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }
            string query = "";
            if (buttonAdd.Content == "Обновить")
            {
                //Обновление данных
                connection.Open();
                query = $"UPDATE `car(nalichiye)` SET id_model={modelId[comboBoxModel.SelectedIndex]},`№kyzova`='{textBoxKuzov.Text}', `№dvigatel`='{textBoxDvs.Text}', `№PTC`='{textBoxPts.Text}'," +
                    $" color='{textBoxColor.Text}', `release`='{numericUpDownDate.Value}' WHERE id_car={carId}";
                command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();

                query = $"UPDATE complektacii SET id_kyzov={kuzovId[comboBoxKuzov.SelectedIndex]}, id_privod={privodId[comboBoxPrivod.SelectedIndex]},mest={numericUpDownMest.Value}," +
                    $"`id_dvigatel'`={dvsId[comboBoxDvs.SelectedIndex]}, `id_Ob\"yem dvigatelya`={vDvsId[comboBoxVDvs.SelectedIndex]}, `loshadinyye sily`={numericUpDownLs.Value}," +
                    $"id_KPP={kppId[comboBoxKPP.SelectedIndex]} WHERE id_car={carId}";
                command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Автомобиль успешно обновлен!");
            }
            else
            {
                //Добавление данных
                connection.Open();
                query = $"INSERT `car(nalichiye)` (id_model, №kyzova, №dvigatel, №PTC, color, `release`, buying) " +
                    $"VALUE ({modelId[comboBoxModel.SelectedIndex]}, '{textBoxKuzov.Text}', '{textBoxDvs.Text}', '{textBoxPts.Text}', '{textBoxColor.Text}', '{numericUpDownDate.Value}', 0)";
                command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();

                query = $"INSERT complektacii(id_car, id_kyzov,id_privod, mest, `id_dvigatel'`, `id_Ob\"yem dvigatelya`, `loshadinyye sily`, id_KPP) " +
                    $"VALUE( (SELECT MAX(id_car) FROM `car(nalichiye)`), {kuzovId[comboBoxKuzov.SelectedIndex]}, {privodId[comboBoxPrivod.SelectedIndex]}, {numericUpDownMest.Value}, " +
                    $"{dvsId[comboBoxDvs.SelectedIndex]}, {vDvsId[comboBoxVDvs.SelectedIndex]}, {numericUpDownLs.Value}, {kppId[comboBoxKPP.SelectedIndex]} )";
                command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();

                connection.Close();

                MessageBox.Show("Автомобиль успешно добавлен!");
            }

            this.Close();
            adminMenuForm.fillDataGrid();
        }

        private void comboBoxMarka_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Вывод в комбобокс только связанных с маркой моделей
            comboBoxModel.Items.Clear();
            modelId.Clear();
            if (comboBoxMarka.SelectedIndex == -1)
            {
                return;
            }

            connection.Open();
            string query = $"SELECT * FROM modeli WHERE id_marka={markiId[comboBoxMarka.SelectedIndex]}";
            command = new MySqlCommand(query, connection);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                modelId.Add(dataReader[0].ToString());
                comboBoxModel.Items.Add(dataReader[1].ToString());
            }
            dataReader.Close();

            connection.Close();

            if (buttonAdd.Content == "Обновить")
            {
                comboBoxModel.SelectedIndex = modelId.IndexOf(selectedModelId);
            }
        }

        private void AddAndUpdateCarForm_Load(object sender, EventArgs e)
        {

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            adminMenuForm.fillDataGrid();
        }
    }
}

