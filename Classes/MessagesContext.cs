using Microsoft.EntityFrameworkCore;
using pr33.Classes.Common;
using pr33.Models;

namespace pr33.Classes
{
    public class MessagesContext : DbContext
    {
        /// <summary> Данные из БД
        public DbSet<Messages> Messages { get; set; }

        /// <summary> Конструктор контекста
        public MessagesContext() =>
            Database.EnsureCreated();

        /// <summary> Конфигурация подключения
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            // Говорим что используем SQL Server со следующей конфигурацией
            optionsBuilder.UseSqlServer(Config.config);
    }
}