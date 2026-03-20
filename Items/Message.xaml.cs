using System.Windows.Controls;
using pr33.Classes.Common;
using pr33.Models;

namespace pr33.Items
{
    /// <summary>
    /// Логика взаимодействия для Message.xaml
    /// </summary>
    public partial class Message : UserControl
    {
        public Message(Messages Messages, Users UserFrom)
        {
            InitializeComponent();
            // Конвертируем изображение пользователя из массива байт в BitmapImage
            imgUser.Source = BitmapFromArrayByte.LoadImage(UserFrom.Photo);
            // Получаем ФИО
            FIO.Content = UserFrom.ToFIO();
            // Отображаем изображение
            tbMessage.Text = Messages.Message;
        }
    }
}
