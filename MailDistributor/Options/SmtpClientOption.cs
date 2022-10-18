using System;
namespace MailDistributor.Options
{
	public class SmtpClientOption
	{

        public const string ConfigurationSection = "SmtpClient";
        /// <summary>
        /// Логин для подключения
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Пароль для подключения
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Адрес SMTP сервера 
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// Порт SMTP сервера
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Использовать ли SSL 
        /// </summary>
        public bool EnableSsl { get; set; }

        /// <summary>
        /// Длительность тайм-аута при отправке письм
        /// </summary>
        public int Timeout { get; set; }
    }
}

