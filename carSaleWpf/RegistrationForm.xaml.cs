using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для RegistrationForm.xaml
    /// </summary>
    public partial class RegistrationForm : Window
    {
        MySqlConnection connection = new MySqlConnection("Server=localhost;UserId=root;Password=1234;Database=carsale");
        MySqlCommand command;
        MySqlDataReader dataReader;
        List<string> dopOptionsId = new List<string>();
        string carId;
        string idComplect;
        CarInfoAndOrdering carInfoAndOrdering;


        public RegistrationForm(string carId, string idComplect, string orderPrice, List<string> dopOptionsId, CarInfoAndOrdering carInfoAndOrdering)
        {
            InitializeComponent();
            labelPrice.Content = orderPrice;
            this.dopOptionsId = dopOptionsId;
            this.carId = carId;
            this.idComplect = idComplect;
            this.carInfoAndOrdering = carInfoAndOrdering;
            this.textBoxSurname.PreviewTextInput += new TextCompositionEventHandler(textBoxSurname_PreviewTextInput);
            this.textBoxName.PreviewTextInput += new TextCompositionEventHandler(textBoxName_PreviewTextInput);
            this.textBoxSurname.PreviewTextInput += new TextCompositionEventHandler(textBoxPatronomyc_PreviewTextInput);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonRegistration_Click(object sender, EventArgs e)
        {
            if (textBoxSurname.Text == "" || textBoxName.Text == "" || textBoxPatronomyc.Text == ""
                || maskedTextBoxPassport.IsMaskFull == false || maskedTextBoxNumber.IsMaskFull == false)
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }
          

            MessageBoxResult result = MessageBox.Show($"Вы действительно хотите оформить заказ на сумму {labelPrice.Content} руб.?", "", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                connection.Open();
                string query = $"INSERT orders (id_car, `date`, surname, `name`, patronomyc, passport, phone, price) VALUE ({carId},'{DateTime.Now.Date.ToString().Split(' ')[0]}'," +
                    $"'{textBoxSurname.Text}','{textBoxName.Text}'," +
                    $"'{textBoxPatronomyc.Text}', '{maskedTextBoxPassport.Text}', '{maskedTextBoxNumber.Text}', {labelPrice.Content})";
                command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();
                string orderId = "";
                query = "SELECT id_order FROM orders WHERE id_car=" + carId;
                command = new MySqlCommand(query, connection);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    orderId = dataReader[0].ToString();
                }
                dataReader.Close();
                for (int i = 0; i < dopOptionsId.Count; i++)
                {
                    query = $"INSERT orders_options (id_order, id_option) VALUE ({orderId},{dopOptionsId[i]}) ";
                    command = new MySqlCommand(query, connection);
                    command.ExecuteNonQuery();
                }

                query = $"UPDATE `car(nalichiye)` SET buying=1 WHERE id_car={carId}";
                command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();

                connection.Close();
                MessageBox.Show($"Заказ номер {orderId} успешно оформлен. \nДля того чтобы сформировать чек нажмите ОК.");

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
                _table.Cell(1, 1).Range.Text = "ЧЕК ЗАКАЗА №" + orderId;
                _table.Cell(2, 1).Range.Text = "Дата: " + DateTime.Now.ToString().Split(' ')[0];
                _table.Cell(3, 1).Range.Text = "Заказчик: " + textBoxSurname.Text + " " + textBoxName.Text + " " + textBoxPatronomyc.Text;
                connection.Open();
                query = "SELECT marki.marki AS 'Марка', modeli.name AS 'Модель', kyzov.name_kyzov AS 'Кузов',`car(nalichiye)`.`№kyzova`, privod.name_privod AS 'Привод', mest AS 'Кол-во мест', `dvigatel'`.`name_dvigatel'` AS 'ДВС', `ob\"yem dvigatelya`.`name_Ob\"yem dvigatelya` AS 'Объем ДВС', `car(nalichiye)`.`№dvigatel`, `loshadinyye sily` AS 'Л.С.', kpp.name_KPP AS 'КПП',`car(nalichiye)`.release AS 'Выпуск', `car(nalichiye)`.color AS 'Цвет', modeli.`price` AS 'Цена'  FROM complektacii, modeli, kyzov, privod, `dvigatel'`, `ob\"yem dvigatelya`, kpp, marki, `car(nalichiye)` " +
                    $"WHERE `car(nalichiye)`.id_model=modeli.id_model AND complektacii.id_kyzov=kyzov.id_kyzov AND complektacii.id_privod=privod.id_privod AND complektacii.`id_dvigatel'`=`dvigatel'`.`id_dvigatel'` AND complektacii.`id_Ob\"yem dvigatelya` =`ob\"yem dvigatelya`.`id_Ob\"yem dvigatelya` AND complektacii.id_KPP=kpp.id_KPP AND marki.id_marki = modeli.id_marka AND complektacii.id_car=`car(nalichiye)`.id_car  AND `car(nalichiye)`.id_car = {carId} AND complektacii.id_complektacii = {idComplect}";
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
                query = $"SELECT * FROM carsale.dop_options WHERE id_option IN (SELECT  id_option FROM orders_options WHERE id_order = {orderId});";
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


                this.Close();
                carInfoAndOrdering.closeFormAndUpdateTable();
            }


        }
        

        private void textBoxSurname_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(IsGood);
        }
      

        private void textBoxName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(IsGood);
        }

        private void textBoxPatronomyc_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(IsGood);
        }
        bool IsGood(char c)
        {
            if (c >= 'a' && c <= 'f')
                return true;
            if (c >= 'A' && c <= 'F')
                return true;
            if (c >= 'А' && c <= 'Я')
                return true;
            if (c >= 'а' && c <= 'я')
                return true;
            return false;
        }
    }
 }


