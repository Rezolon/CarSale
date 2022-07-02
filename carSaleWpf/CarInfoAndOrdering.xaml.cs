using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace carSaleWpf
{
    public class listViewOptions
    {
        public string id { get; set; }
        public bool isChecked { get; set; }
        public string name { get; set; }
        public string price { get; set; }
    }

    /// <summary>
    /// Логика взаимодействия для CarInfoAndOrdering.xaml
    /// </summary>
    public partial class CarInfoAndOrdering : Window
    {
        MySqlConnection connection = new MySqlConnection("Server=localhost;UserId=root;Password=1234;Database=carsale");
        MySqlCommand command;
        MySqlDataReader dataReader;
        MySqlDataAdapter dataAdapter;
        DataTable dataTable;
        List<string> typeComplectId = new List<string>();
        List<listViewOptions> listOptions = new List<listViewOptions>();
        string carId;
        string markaId;
        string idComplect;
        int originflPrice;
        CarListForm carListForm;

        public CarInfoAndOrdering(string idCar, string idComplect, string idMarki, CarListForm carListForm)
        {
            
            InitializeComponent();
            this.carListForm = carListForm;
            this.carId = idCar;
            this.markaId = idMarki;
            this.idComplect = idComplect;
            connection.Open();
            string query = "SELECT marki.marki AS 'Марка', modeli.name AS 'Модель', kyzov.name_kyzov AS 'Кузов',`car(nalichiye)`.`№kyzova`, privod.name_privod AS 'Привод', mest AS 'Кол-во мест', `dvigatel'`.`name_dvigatel'` AS 'ДВС', `ob\"yem dvigatelya`.`name_Ob\"yem dvigatelya` AS 'Объем ДВС', `car(nalichiye)`.`№dvigatel`, `loshadinyye sily` AS 'Л.С.', kpp.name_KPP AS 'КПП',`car(nalichiye)`.release AS 'Выпуск', `car(nalichiye)`.color AS 'Цвет', modeli.`price` AS 'Цена', modeli.photo  FROM complektacii, modeli, kyzov, privod, `dvigatel'`, `ob\"yem dvigatelya`, kpp, marki, `car(nalichiye)` " +
                $"WHERE `car(nalichiye)`.id_model=modeli.id_model AND complektacii.id_kyzov=kyzov.id_kyzov AND complektacii.id_privod=privod.id_privod AND complektacii.`id_dvigatel'`=`dvigatel'`.`id_dvigatel'` AND complektacii.`id_Ob\"yem dvigatelya` =`ob\"yem dvigatelya`.`id_Ob\"yem dvigatelya` AND complektacii.id_KPP=kpp.id_KPP AND marki.id_marki = modeli.id_marka AND complektacii.id_car=`car(nalichiye)`.id_car  AND `car(nalichiye)`.id_car = {carId} AND complektacii.id_complektacii = {idComplect}";
            command = new MySqlCommand(query, connection);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                labelMarka.Content = dataReader[0].ToString();
                labelModel.Content = dataReader[1].ToString();
                labelKuzov.Content = dataReader[2].ToString();
                labelNumberKuzov.Content = dataReader[3].ToString();
                labelPrivod.Content = dataReader[4].ToString();
                labelMest.Content = dataReader[5].ToString();
                labelDvs.Content = dataReader[6].ToString();
                labelVDvs.Content = dataReader[7].ToString();
                labelNumberDvs.Content = dataReader[8].ToString();
                labelLs.Content = dataReader[9].ToString();
                labelKpp.Content = dataReader[10].ToString();
                labelDate.Content = dataReader[11].ToString();
                labelColor.Content = dataReader[12].ToString();
                labelPrice.Content = dataReader[13].ToString();
                this.originflPrice = Convert.ToInt32(dataReader[13].ToString());
                if(dataReader[14].ToString() != "")
                    pictureBoxCarImage.Source = ImageSourceFromBitmap(new Bitmap(dataReader[14].ToString()));
            }
            dataReader.Close();

            query = "SELECT * FROM dop_options";
            command = new MySqlCommand(query, connection);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                listOptions.Add(new listViewOptions() { id = dataReader[0].ToString(), isChecked = false, name = dataReader[1].ToString(), price= dataReader[2].ToString() }) ;
            }
            dataReader.Close();
            listViewDopOptions.ItemsSource = listOptions;

            connection.Close();

        }

      
        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject([In] IntPtr hObject);

        public ImageSource ImageSourceFromBitmap(Bitmap bmp)
        {
            var handle = bmp.GetHbitmap();
            try
            {
                return Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            finally { DeleteObject(handle); }
        }

        private void listViewDopOptions_ItemCheck(object sender, RoutedEventArgs e)
        {
            List<listViewOptions> listOptionsChecked = listOptions.FindAll(ch => ch.isChecked == true);
            int dopPrice = 0;
            for (int i = 0; i < listOptionsChecked.Count; i++)
            {
                dopPrice += Convert.ToInt32(listOptionsChecked[i].price);
            }
            labelPrice.Content = (originflPrice + dopPrice).ToString();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonDrop_Click(object sender, EventArgs e)
        {
           
            listOptions.FindAll(ch => ch.isChecked == true).ForEach(dropChecked => dropChecked.isChecked=false);

            listViewDopOptions.ItemsSource = null;
            listViewDopOptions.ItemsSource = listOptions;
            labelPrice.Content = originflPrice.ToString();
        }

        private void buttonCreateOrder_Click(object sender, EventArgs e)
        {
            
            
            List<string> dopOptionsId = new List<string>();
            listOptions.FindAll(ch => ch.isChecked == true).ForEach(ch => dopOptionsId.Add(ch.id));

            RegistrationForm registrationForm = new RegistrationForm(carId, idComplect, labelPrice.Content.ToString(), dopOptionsId, this);
            registrationForm.ShowDialog();
        }

        public void closeFormAndUpdateTable()
        {
            this.Close();
            carListForm.fillDataGRidCar();
        }
    }
}
