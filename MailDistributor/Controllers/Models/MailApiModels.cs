using System;
using System.ComponentModel.DataAnnotations;

namespace MailDistributor.Controllers.Models
{
	public class MailPostRequestApiModel
	{
		public string? Subject { get; set; }

		[Required]
		public string Body { get; set; }

		[Required]
		[MinLength(1)]
		public IEnumerable<string> Recipients { get; set; }
	}
}

