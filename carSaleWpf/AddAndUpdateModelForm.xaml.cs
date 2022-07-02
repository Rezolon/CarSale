using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Drawing;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;
using MySql.Data.MySqlClient;
using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace carSaleWpf
{
    /// <summary>
    /// Логика взаимодействия для AddAndUpdateModelForm.xaml
    /// </summary>
    public partial class AddAndUpdateModelForm : Window
    {

        string modelId;
        MySqlConnection connection = new MySqlConnection("Server=localhost;UserId=root;Password=1234;Database=carsale");
        MySqlCommand command;
        MySqlDataReader dataReader;
        AdminMenuForm adminMenuForm;
        List<string> markiId = new List<string>();
        string currentImagePath = "";


        public AddAndUpdateModelForm(AdminMenuForm adminMenuForm)
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
            connection.Close();
        }

        public AddAndUpdateModelForm(AdminMenuForm adminMenuForm, string modelId)
        {
            InitializeComponent();
            this.modelId = modelId;
            this.adminMenuForm = adminMenuForm;
            buttonAdd.Content = "Обновить";

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

            query = $"SELECT * FROM modeli WHERE id_model={modelId}";
            command = new MySqlCommand(query, connection);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                textBoxName.Text = dataReader[1].ToString();
                numericUpDownPrice.Value = Convert.ToInt32(dataReader[2].ToString());
                comboBoxMarka.SelectedIndex = markiId.IndexOf(dataReader[3].ToString());
                pictureBoxModelImage.Source = ImageSourceFromBitmap(new Bitmap(dataReader[4].ToString()));
            }
            dataReader.Close();

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

        private void buttonLoadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog imageOpenFileDialog = new OpenFileDialog();
            
            if (imageOpenFileDialog.ShowDialog() == false)
                return;
            string newImagePath = imageOpenFileDialog.FileName;
            string newImageName = newImagePath.Split('\\')[newImagePath.Split('\\').Length - 1];
            currentImagePath = $@"carImage\{newImageName}";
           
            try
            {
                File.Copy(newImagePath, currentImagePath);
            }
            catch { }
            try
            {
                pictureBoxModelImage.Source = ImageSourceFromBitmap(new Bitmap(currentImagePath));

                MessageBox.Show("Изображение успешно загружено.");

            }
            catch
            {
                MessageBox.Show("При загрузке изображения произошла ошибка.");
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (textBoxName.Text == "" || comboBoxMarka.SelectedIndex == -1)
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }
            if (pictureBoxModelImage.Source == null)
            {
                MessageBox.Show("Загрузите фото!");
                return;
            }

            
            
            
            string query = "";
            if (buttonAdd.Content == "Обновить")
            {
                
                connection.Open();
                query = $"UPDATE modeli SET name='{textBoxName.Text}', price={numericUpDownPrice.Value}, id_marka={markiId[comboBoxMarka.SelectedIndex]}, photo='{currentImagePath.Replace(@"\", @"\\")}'" +
                    $" WHERE id_model={modelId}";
                command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();

                connection.Close();
                MessageBox.Show("Модель успешно обновлен!");
            }
            else
            {
               
                connection.Open();

                query = $"SELECT MAX(`id_model`)+1 FROM `modeli`";
                string maxId = "";
                command = new MySqlCommand(query, connection);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    maxId = dataReader[0].ToString();
                }
                dataReader.Close();

                query = $"INSERT modeli VALUE ({maxId},'{textBoxName.Text}',{numericUpDownPrice.Value}, {markiId[comboBoxMarka.SelectedIndex]}, '{currentImagePath.Replace(@"\", @"\\")}')";
                command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();

                connection.Close();

                MessageBox.Show("Модель успешно добавлен!");
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
