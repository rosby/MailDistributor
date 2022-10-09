using System;
using MailDistributor.Helpers;

namespace MailDistributor.Services.Models
{
	public class SmtpSettings
	{
		public string Login { get; set; }

		public string Password { get; set; }

		public string Server { get; set; }

		public int Port { get; set; }

		public bool EnableSsl { get; set; }

		public int Timeout { get; set; }
		
		public SmtpSettings()
		{
		}

		public static SmtpSettings GetFromConfiguration(IConfiguration configuration)
		{
			return new SmtpSettings()
			{
				Login = configuration.GetSmtpLogin(),
				Password = configuration.GetSmtpPassword(),
				Server = configuration.GetSmtpServer(),
				Port = int.Parse(configuration.GetSmtpPort()),
                EnableSsl = bool.Parse(configuration.GetSmtpEnableSsl()),
				Timeout = int.Parse(configuration.GetSmtpTimeout())
			};
        }
    }
}

