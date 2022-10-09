using System;
using MailDistributor.Controllers.Models;
using MailDistributor.Database.Tables;
using MailDistributor.Services;
using Microsoft.AspNetCore.Mvc;

namespace MailDistributor.Controllers
{
	[Controller]
	[Route("api/mails")]
	public class MailsController : ControllerBase
	{
		private MailService _mailService;

		public MailsController(MailService mailService)
		{
			_mailService = mailService;
        }


		[HttpPost]
		public async Task Post([FromBody] MailPostRequestApiModel model, CancellationToken stoppingToken)
		{
			await _mailService.SendAsync(model);
		}

		[HttpGet]
		public IAsyncEnumerable<Mail> Get(CancellationToken stoppingToken)
		{
			return _mailService.GetHistoryAsync(stoppingToken);
		}
	}
}

