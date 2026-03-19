using Microsoft.EntityFrameworkCore;
using pr33.Classes.Common;
using pr33.Models;

namespace pr33.Classes
{
    public class UsersContext : DbContext
    {
        /// <summary> Данные из БД
        public DbSet<Users> Users { get; set; }
        /// <summary> Конструктор контекста
        public UsersContext() =>
            // Проверяем подключены ли мы к БД, если не подключены, подключаемся
            Database.EnsureCreated();

        /// <summary> Конфигурация подключения
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            // Говорим что используем SQL Server со следующей конфигурацией
            optionsBuilder.UseSqlServer(Config.config);
    }
}
