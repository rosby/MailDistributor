using System;
namespace MailDistributor.Helpers
{
	public static class ConfigurationExtensions
	{
        /// <summary>
        /// Получить логин для подключения к smtp серверу
        /// </summary>
        /// <param name="configuration">Конфигурация проекта</param>
        /// <returns></returns>
        public static string GetSmtpLogin(this IConfiguration configuration)
        {
            return configuration.GetRequiredSection("SmtpClient")["Login"];
        }

        /// <summary>
        /// Получить пароль для подключения к smtp серверу
        /// </summary>
        /// <param name="configuration">Конфигурация проекта</param>
        /// <returns></returns>
        public static string GetSmtpPassword(this IConfiguration configuration)
        {
            return configuration.GetRequiredSection("SmtpClient")["Password"];
        }

        /// <summary>
        /// Получить адрес для подключения к smtp серверу
        /// </summary>
        /// <param name="configuration">Конфигурация проекта</param>
        /// <returns></returns>
        public static string GetSmtpServer(this IConfiguration configuration)
        {
            return configuration.GetRequiredSection("SmtpClient")["Server"];
        }

        /// <summary>
        /// Получить порт для подключения к smtp серверу
        /// </summary>
        /// <param name="configuration">Конфигурация проекта</param>
        /// <returns></returns>
        public static string GetSmtpPort(this IConfiguration configuration)
        {
            return configuration.GetRequiredSection("SmtpClient")["Port"];
        }

        /// <summary>
        /// Получить настройку использования ssl шифрования для подключения к smtp сервера
        /// </summary>
        /// <param name="configuration">Конфигурация проекта</param>
        /// <returns></returns>
        public static string GetSmtpEnableSsl(this IConfiguration configuration)
        {
            return configuration.GetRequiredSection("SmtpClient")["EnableSsl"];
        }

        /// <summary>
        /// Получить значение длительности таймаута для отправки сообщений через smtp протокол
        /// </summary>
        /// <param name="configuration">Конфигурация проекта</param>
        /// <returns></returns>
        public static string GetSmtpTimeout(this IConfiguration configuration)
        {
            return configuration.GetRequiredSection("SmtpClient")["Timeout"];
        }
    }
	
}

