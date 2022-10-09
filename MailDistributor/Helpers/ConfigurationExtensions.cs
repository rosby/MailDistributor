using System;
namespace MailDistributor.Helpers
{
	public static class ConfigurationExtensions
	{
        public static string GetSmtpLogin(this IConfiguration configuration)
        {
            return configuration.GetRequiredSection("SmtpClient")["Login"];
        }

        public static string GetSmtpPassword(this IConfiguration configuration)
        {
            return configuration.GetRequiredSection("SmtpClient")["Password"];
        }

        public static string GetSmtpServer(this IConfiguration configuration)
        {
            return configuration.GetRequiredSection("SmtpClient")["Server"];
        }

        public static string GetSmtpPort(this IConfiguration configuration)
        {
            return configuration.GetRequiredSection("SmtpClient")["Port"];
        }

        public static string GetSmtpEnableSsl(this IConfiguration configuration)
        {
            return configuration.GetRequiredSection("SmtpClient")["EnableSsl"];
        }

        public static string GetSmtpTimeout(this IConfiguration configuration)
        {
            return configuration.GetRequiredSection("SmtpClient")["Timeout"];
        }
    }
	
}

