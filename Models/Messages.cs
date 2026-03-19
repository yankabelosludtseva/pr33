using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pr33.Models
{
    class Messages
    {
        /// <summary> Код сообщения
        public int Id { get; set; }
        /// <summary> Отправитель
        public int UserFrom { get; set; }
        /// <summary> Получатель
        public int UserTo { get; set; }
        /// <summary> Текст сообщения
        public string Message { get; set; }
        /// <summary> Конструктор для сообщения
        public Messages(int UserFrom, int UserTo, string Message)
        {
            this.UserFrom = UserFrom;
            this.UserTo = UserTo;
            this.Message = Message;
        }
    }
}
