using System;
using System.Net;
using System.Net.Mail;
using MailDistributor.Controllers.Models;
using Microsoft.Extensions.Configuration;
using MailDistributor.Helpers;
using MailDistributor.Database.Tables;
using MailDistributor.Database;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MailDistributor.Options;

namespace MailDistributor.Services
{
    /// <summary>
    /// Сервис для работы с почтой через SMTP протокол
    /// </summary>
	public class MailService
    {
		SmtpClientOption _smtpClientSettings;
        PostgreDbContext _dbContext;


		public MailService(PostgreDbContext dbContext, IOptions<SmtpClientOption> smtpClientSettings)
		{
            _smtpClientSettings = smtpClientSettings.Value;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Отправить email сообщение
        /// </summary>
        /// <param name="model">Модель входящих данных</param>
        /// <returns></returns>
		public async Task SendAsync(MailPostRequestApiModel model)
		{
            ValidateModel(model);
            var mailsForSend = PrepareData(model);
            await SendMailAsync(mailsForSend);
            await StoreResultAsync(mailsForSend);
        }



        /// <summary>
        /// Валидация входящей модели данных
        /// </summary>
        /// <param name="model">Модель входящих данных</param>
        /// <exception cref="InvalidDataException"></exception>
        private void ValidateModel(MailPostRequestApiModel model)
        {
            if (!model.Recipients.Any())
                throw new InvalidDataException("Не указано ни одного получателя письма");

            foreach (var recipient in model.Recipients)
            {
                if (!MailAddress.TryCreate(recipient, out var _))
                    throw new InvalidDataException($"Неверный формат email [{recipient}]");
            }
        }

        /// <summary>
        /// Подготовка данных к дальнейшей обработки
        /// </summary>
        /// <param name="model">Входящяя модель данных</param>
        /// <returns>Словарь, ключем которого является объект для отправки с помощью smtp, а значение объект базы данных для сохранения</returns>
        private Dictionary<MailMessage, Mail> PrepareData(MailPostRequestApiModel model)
        {
            return model.Recipients.ToDictionary(recipient => new MailMessage(_smtpClientSettings.Login,
                                                                                          recipient,
                                                                                          model.Subject,
                                                                                          model.Body),
                                                 recipient => new Mail(_smtpClientSettings.Login,
                                                                       recipient,
                                                                       model.Subject,
                                                                       model.Body));
        }

        /// <summary>
        /// Получение истории отправленных email сообщений
        /// </summary>
        /// <param name="stoppingToken">Токен прерывания операции</param>
        /// <returns></returns>
        public async IAsyncEnumerable<Mail> GetHistoryAsync([EnumeratorCancellation] CancellationToken stoppingToken)
        {
            await foreach (var mail in _dbContext.Mails.AsNoTracking().AsAsyncEnumerable().WithCancellation(stoppingToken))
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
            smptClient.LoadSettings(_smtpClientSettings);

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
        private async Task StoreResultAsync(Dictionary<MailMessage, Mail> mails)
        {
            await using var context = GetContext<PostgreDbContext>();

            context.AddRange(mails.Select(item => item.Value).ToList());
            await context.SaveChangesAsync();
        }
    }
}

