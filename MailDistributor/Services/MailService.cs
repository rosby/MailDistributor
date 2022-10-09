using System;
using System.Net;
using System.Net.Mail;
using MailDistributor.Controllers.Models;
using Microsoft.Extensions.Configuration;
using MailDistributor.Helpers;
using MailDistributor.Services.Models;
using MailDistributor.Database.Tables;
using MailDistributor.Database;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

namespace MailDistributor.Services
{
    /// <summary>
    /// Сервис для работы с почтой через SMTP протокол
    /// </summary>
	public class MailService : BaseService
    {
		private SmtpSettings _smtpSettings;

		public MailService(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider)
		{
			_smtpSettings = SmtpSettings.GetFromConfiguration(configuration);
		}

        /// <summary>
        /// Отправить email сообщение
        /// </summary>
        /// <param name="model">Модель входящих данных</param>
        /// <returns></returns>
		public async Task SendAsync(MailPostRequestApiModel model)
		{
			var mailsForSend = model.Recipients.ToDictionary(recipient => new MailMessage(_smtpSettings.Login,
																						  recipient,
																						  model.Subject,
																						  model.Body),
															 recipient => new Mail(_smtpSettings.Login,
																				   recipient,
																				   model.Subject,
																				   model.Body));

            await SendMailAsync(mailsForSend);
            await StoreResultAsync(mailsForSend.Select(item => item.Value).ToList());
        }

        /// <summary>
        /// Получение истории отправленных email сообщений
        /// </summary>
        /// <param name="stoppingToken">Токен прерывания операции</param>
        /// <returns></returns>
        public async IAsyncEnumerable<Mail> GetHistoryAsync([EnumeratorCancellation] CancellationToken stoppingToken)
        {
            await using var context = GetContext<PostgreDbContext>();

            await foreach (var mail in context.Mails.AsNoTracking().AsAsyncEnumerable().WithCancellation(stoppingToken))
                yield return mail;
        }

        /// <summary>
        /// Отправить e-mail с помощью smtp сервера
        /// </summary>
        /// <param name="mails">Письма для отправки</param>
        /// <returns></returns>
        private async Task SendMailAsync(Dictionary<MailMessage, Mail> mails)
        {
            using var smptClient = new SmtpClient();
            smptClient.LoadSettings(_smtpSettings);

            foreach (KeyValuePair<MailMessage, Mail> mail in mails)
            {
                try
                {
                    await smptClient.SendMailAsync(mail.Key);
                    mail.Value.Result = "OK";
                }
                catch (Exception ex)
                {
                    mail.Value.Result = "Failed";
                    mail.Value.FailedMessage = ex.Message;
                }
                mail.Value.SendDate = DateTime.Now;
            }

        }

        /// <summary>
        /// Сохранение результата отправления письм 
        /// </summary>
        /// <param name="mails">Отправленные письма</param>
        /// <returns></returns>
        private async Task StoreResultAsync(IEnumerable<Mail> mails)
        {
            await using var context = GetContext<PostgreDbContext>();

            context.AddRange(mails);
            await context.SaveChangesAsync();
        }
    }
}

