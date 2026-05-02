using System.Windows;
using System.Windows.Controls;
using pr33.Models;
using pr33.Pages;

namespace pr33
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary> Статический экземпляр главного окна </summary>
        public static MainWindow Instance;

        /// <summary> Авторизованный пользователь </summary>
        public Users LoginUser { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Instance = this;
            // При загрузке окна открываем страницу логина
            OpenPages(new Login());
        }

        /// <summary> Открытие страницы </summary>
        public void OpenPages(Page page)
        {
            frame.Navigate(page);
        }
    }
}