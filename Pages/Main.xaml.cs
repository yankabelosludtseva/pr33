using pr33.Classes;
using pr33.Classes.Common;
using pr33.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace pr33.Pages
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        /// <summary> Выбранный пользовательский диалог </summary>
        public Users SelectedUser = null;

        /// <summary> Контекст для работы с пользователями </summary>
        public UsersContext usersContext = new UsersContext();

        /// <summary> Контекст для работы с сообщениями </summary>
        public MessagesContext messagesContext = new MessagesContext();

        /// <summary> Таймер для обновления сообщений </summary>
        public DispatcherTimer Timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 3) };

        public Main()
        {
            InitializeComponent();
            InitializeTimer();
            LoadUsers();
        }

        /// <summary> Инициализация таймера </summary>
        private void InitializeTimer()
        {
            // Подписываемся на событие выполнения
            Timer.Tick += Timer_Tick;
            // Запускаем таймер
            Timer.Start();
        }

        /// <summary> Загрузка пользователей </summary>
        public void LoadUsers()
        {
            // Очищаем список перед загрузкой
            ParentUsers.Children.Clear();

            // Перебираем пользователей
            foreach (Users user in usersContext.Users)
            {
                // Если пользователь не является авторизованным
                if (user.Id != MainWindow.Instance.LoginUser.Id)
                {
                    // Выводим в интерфейс
                    ParentUsers.Children.Add(new Items.User(user, this));
                }
            }
        }

        /// <summary> Обновление сообщений пользователя </summary>
        private void Timer_Tick(object sender, EventArgs e)
        {
            // Если выбран пользовательский диалог
            if (SelectedUser != null)
            {
                // Обновляем сообщения
                SelectUser(SelectedUser);
            }
        }

        /// <summary> Выбор диалога </summary>
        public void SelectUser(Users User)
        {
            // Запоминаем выбранный диалог
            SelectedUser = User;
            // Показываем чат
            Chat.Visibility = Visibility.Visible;

            // Конвертируем изображение пользователя из массива байт в BitmapImage
            if (User.Photo != null && User.Photo.Length > 0)
            {
                imgUser.Source = BitMapFromArrayByte.LoadImage(User.Photo);
            }
            else
            {
                imgUser.Source = new BitmapImage(new Uri("/Images/ic-user.png", UriKind.Relative));
            }

            // Отображаем ФИО
            FIO.Content = User.ToFIO();
            // Очищаем сообщения в диалоге
            ParentMessages.Children.Clear();
            // Перебираем сообщения
            foreach (Messages Message in messagesContext.Messages.Where(x =>
                (x.UserFrom == User.Id && x.UserTo == MainWindow.Instance.LoginUser.Id) ||
                (x.UserFrom == MainWindow.Instance.LoginUser.Id && x.UserTo == User.Id)).OrderBy(x => x.Id))
            {
                // Добавляем сообщение в диалог
                var fromUser = usersContext.Users.Where(x => x.Id == Message.UserFrom).FirstOrDefault();
                if (fromUser != null)
                {
                    ParentMessages.Children.Add(new Items.Message(Message, fromUser));
                }
            }
        }

        /// <summary> Отправка сообщения пользователю </summary>
        private void Send(object sender, KeyEventArgs e)
        {
            // Если нажата клавиша Enter
            if (e.Key == Key.Enter)
            {
                // Проверяем, выбран ли диалог и есть ли текст сообщения
                if (SelectedUser == null || string.IsNullOrWhiteSpace(Message.Text))
                    return;

                // Создаём сообщение, где отправитель мы, а получатель выбранный диалог
                Messages message = new Messages(
                    MainWindow.Instance.LoginUser.Id,
                    SelectedUser.Id,
                    Message.Text
                );
                // Добавляем сообщения в контекст
                messagesContext.Messages.Add(message);
                // Сохраняем изменения
                messagesContext.SaveChanges();
                // Добавляем сообщения на экран
                ParentMessages.Children.Add(new Items.Message(message, MainWindow.Instance.LoginUser));
                // Очищаем поле ввода
                Message.Text = "";
            }
        }
    }
}