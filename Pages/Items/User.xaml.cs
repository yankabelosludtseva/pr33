using System.Windows.Controls;
using System.Windows.Media.Imaging;
using pr33.Models;
using pr33.Pages;
using pr33.Classes.Common;

namespace pr33.Items
{
    public partial class User : UserControl
    {
        Users user;
        Main main;

        public User(Users user, Main main)
        {
            InitializeComponent();
            this.user = user;
            this.main = main;

            // Конвертируем изображение из массива байт, в BitmapImage
            if (user.Photo != null && user.Photo.Length > 0)
            {
                imgUser.Source = BitMapFromArrayByte.LoadImage(user.Photo);
            }
            else
            {
                imgUser.Source = new BitmapImage(new Uri("/Images/ic-user.png", UriKind.Relative));
            }

            // Присваиваем ФИО
            FIO.Content = user.ToFIO();
        }

        private void SelectChat(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            main.SelectUser(user);
        }
    }
}