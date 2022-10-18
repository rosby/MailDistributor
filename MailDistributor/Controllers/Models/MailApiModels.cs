using System;
using System.ComponentModel.DataAnnotations;

namespace MailDistributor.Controllers.Models
{
	public class MailPostRequestApiModel
	{
		/// <summary>
		/// Заголовок письма
		/// </summary>
		public string? Subject { get; set; }

		/// <summary>
		/// Тело письма
		/// </summary>
		[Required]
		public string Body { get; set; }

		/// <summary>
		/// Список адресов получаетей письма
		/// </summary>
		[Required]
		public IEnumerable<string> Recipients { get; set; }
	}
}

