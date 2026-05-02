using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using pr33.Classes;
using pr33.Models;
using System.IO;
using System.Linq;

namespace pr33.Pages
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }

        /// <summary> Изображение пользователя </summary>
        public string srcUserImage = "";

        /// <summary> Контекст пользователя </summary>
        UsersContext usersContext = new UsersContext();

        /// <summary> Выбор фотографии пользователя </summary>
        private void SelectPhoto(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Выберите фотографию.";
            openFileDialog.InitialDirectory = @"C:\";
            openFileDialog.Filter = "JPG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                imgUser.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                srcUserImage = openFileDialog.FileName;
            }
        }

        /// <summary> Проверка на заполнение полей </summary>
        public bool CheckEmpty(string Pattern, string Input)
        {
            Match m = Regex.Match(Input, Pattern);
            return m.Success;
        }

        /// <summary> Переход на страницу с диалогами </summary>
        private void Continue(object sender, RoutedEventArgs e)
        {
            if (!CheckEmpty("^[А-ЯЁ][а-я-ЯЁ]*$", Lastname.Text))
            {
                MessageBox.Show("Укажите фамилию.");
                return;
            }
            if (!CheckEmpty("^[А-ЯЁ][а-я-ЯЁ]*$", Firstname.Text))
            {
                MessageBox.Show("Укажите имя.");
                return;
            }
            if (!CheckEmpty("^[А-ЯЁ][а-я-ЯЁ]*$", Surname.Text))
            {
                MessageBox.Show("Укажите отчество.");
                return;
            }
            if (string.IsNullOrEmpty(srcUserImage))
            {
                MessageBox.Show("Выберите изображение.");
                return;
            }

            var existingUser = usersContext.Users.Where(x => x.Firstname == Firstname.Text &&
                                                              x.Lastname == Lastname.Text &&
                                                              x.Surname == Surname.Text).FirstOrDefault();

            if (existingUser != null)
            {
                MainWindow.Instance.LoginUser = existingUser;
                MainWindow.Instance.LoginUser.Photo = File.ReadAllBytes(srcUserImage);
                usersContext.SaveChanges();
            }
            else
            {
                usersContext.Users.Add(new Users(Lastname.Text, Firstname.Text, Surname.Text, File.ReadAllBytes(srcUserImage)));
                usersContext.SaveChanges();
                MainWindow.Instance.LoginUser = usersContext.Users.Where(x => x.Firstname == Firstname.Text &&
                                                                               x.Lastname == Lastname.Text &&
                                                                               x.Surname == Surname.Text).First();
            }

            MainWindow.Instance.OpenPages(new Main());
        }
    }
}