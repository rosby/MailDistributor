using System;
using MailDistributor.Controllers.Models;
using MailDistributor.Database.Tables;
using MailDistributor.Services;
using Microsoft.AspNetCore.Mvc;

namespace MailDistributor.Controllers
{
	/// <summary>
	/// Контроллер для работы с email почтой
	/// </summary>
	[Controller]
	[Route("api/mails")]
	public class MailsController : ControllerBase
	{
		private MailService _mailService;

		public MailsController(MailService mailService)
		{
			_mailService = mailService;
        }

		/// <summary>
		/// Отправка писем по email
		/// </summary>
		/// <param name="model">Модель входящих данных</param>
		/// <returns></returns>
		[HttpPost]
		public async Task Post([FromBody] MailPostRequestApiModel model)
		{
			await _mailService.SendAsync(model);
		}

        /// <summary>
        /// Получение истории всех отправленных писем
        /// </summary>
        /// <param name="stoppingToken">Токен прерывания операции</param>
        /// <returns>Список <see cref="Mail"> объектов</returns>
        [HttpGet]
		public IAsyncEnumerable<Mail> Get(CancellationToken stoppingToken)
		{
			return _mailService.GetHistoryAsync(stoppingToken);
		}
	}
}

