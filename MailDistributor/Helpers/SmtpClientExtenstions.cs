using System;
using System.Net;
using System.Net.Mail;
using MailDistributor.Services.Models;

namespace MailDistributor.Helpers
{
	public static class SmtpClientExtenstions
	{
		public static void LoadSettings(this SmtpClient smtpClient, SmtpSettings smtpSettings)
		{
			smtpClient.Host = smtpSettings.Server;
            smtpClient.Port = smtpSettings.Port;
            smtpClient.Credentials = new NetworkCredential(smtpSettings.Login, smtpSettings.Password);
			smtpClient.EnableSsl = smtpSettings.EnableSsl;
			smtpClient.Timeout = smtpSettings.Timeout;
        }
	}
}

