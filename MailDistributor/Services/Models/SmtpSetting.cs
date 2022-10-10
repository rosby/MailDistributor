using System;
using MailDistributor.Helpers;

namespace MailDistributor.Services.Models
{
	public class SmtpSettings
	{
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
		
		public SmtpSettings()
		{
		}

		/// <summary>
		/// Получение настроек smtp клиента из конфигурации сервиса
		/// </summary>
		/// <param name="configuration">Конфигурация сервиса</param>
		/// <returns>Объект настроек типа <see cref="SmtpSettings"></returns>
		public static SmtpSettings GetFromConfiguration(IConfiguration configuration)
		{
			return new SmtpSettings()
			{
				Login = configuration.GetSmtpLogin(),
				Password = configuration.GetSmtpPassword(),
				Server = configuration.GetSmtpServer(),
				Port = configuration.GetSmtpPort(),
                EnableSsl = configuration.GetSmtpEnableSsl(),
				Timeout = configuration.GetSmtpTimeout()
			};
        }
    }
}

