using System.Windows.Controls;
using System.Windows.Media.Imaging;
using pr33.Classes.Common;
using pr33.Models;

namespace pr33.Items
{
    public partial class Message : UserControl
    {
        public Message(Messages Messages, Users UserFrom)
        {
            InitializeComponent();

            // Конвертируем изображение пользователя из массива байт в BitmapImage
            if (UserFrom.Photo != null && UserFrom.Photo.Length > 0)
            {
                imgUser.Source = BitMapFromArrayByte.LoadImage(UserFrom.Photo);
            }
            else
            {
                imgUser.Source = new BitmapImage(new Uri("/Images/ic-user.png", UriKind.Relative));
            }

            // Получаем ФИО
            FIO.Content = UserFrom.ToFIO();
            // Отображаем изображение
            tbMessage.Text = Messages.Message;
        }
    }
}