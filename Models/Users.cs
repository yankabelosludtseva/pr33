using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pr33.Models
{
    public class Users
    {
        /// <summary> Код
        public int Id { get; set; }
        /// <summary> Фамилия
        public string Lastname { get; set; }
        /// <summary> Имя пользователя
        public string Firstname { get; set; }

        /// <summary> Отчество
        public string Surname { get; set; }
        /// <summary> Фотография
        public byte[] Photo { get; set; }
        /// <summary> Конструктор для заполнения объекта
        public Users(string Lastname, string Firstname, string Surname, byte[] Photo)
        {
            this.Lastname = Lastname;
            this.Firstname = Firstname;
            this.Surname = Surname;
            this.Photo = Photo;
        }

        /// <summary> Получить ФИО пользователя
        public string ToFIO()
        {
            return $"{Lastname} {Firstname} {Surname}";
        }
    }
}
