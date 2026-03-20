using System.Windows.Controls;
using System.Windows.Input;
using pr33.Models;
using pr33.Pages;

namespace pr33.Items
{
    /// <summary>
    /// Логика взаимодействия для User.xaml
    /// </summary>
    public partial class User : UserControl
    {
        /// <summary> Пользователь которого отображаем
        Users user;
        /// <summary> Ссылка на главное окно
        Main main;

        public User(Users user, Main main)
        {
            InitializeComponent();
            // Запоминаем пользователя которого отображаем
            this.user = user;
            // Запоминаем ссылку на главное окно
            this.main = main;
            // Конвертируем изображение из массива байт, в BitmapImage
            imgUser.Source = BitmapFromArrayByte.LoadImage(user.Photo);
            // Присваиваем ФИО
            FIO.Content = user.ToFIO();
        }

        /// <summary> Выбор диалога
        private void SelectChat(object sender, System.Windows.Input.MouseButtonEventArgs e) =>
            // При нажатии вызываем метод выбора пользователя на
            //главном окне, передавая выбранного пользователя
            main.SelectUser(user);
    }
}
