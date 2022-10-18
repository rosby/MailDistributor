using System;
using System.Net;
using System.Net.Mail;
using MailDistributor.Options;

namespace MailDistributor.Helpers
{
	public static class SmtpClientExtenstions
	{
		/// <summary>
		/// Установка настроек Smtp-клиент 
		/// </summary>
		/// <param name="smtpClient"></param>
		/// <param name="smtpSettings">Настройки smtp-клиента</param>
		public static void LoadSettings(this SmtpClient smtpClient, SmtpClientOption smtpSettings)
		{
			smtpClient.Host = smtpSettings.Server;
            smtpClient.Port = smtpSettings.Port;
            smtpClient.Credentials = new NetworkCredential(smtpSettings.Login, smtpSettings.Password);
			smtpClient.EnableSsl = smtpSettings.EnableSsl;
			smtpClient.Timeout = smtpSettings.Timeout;
        }
	}
}

