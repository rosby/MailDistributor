using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MailDistributor.Database.Tables
{
	[Table("mail", Schema = "dbo")]
	public class Mail
	{
		[Key, Column("id")]
		public int Id { get; set; }

        [Column("source")]
        public string Source { get; set; }

        [Column("recipient")]
        public string Recipient { get; set; }

        [Column("subject")]
		public string? Subject { get; set; }

        [Column("body")]
        public string Body { get; set; }

        [Column("result")]
        public string Result { get; set; }

        [Column("failedmessage")]
        public string? FailedMessage { get; set; }

        [Column("senddate")]
        public DateTime SendDate { get; set; }

        public Mail()
        {

        }

        public Mail(string source, string recipient, string? subject, string body)
        {
            Source = source;
            Recipient = recipient;
            Subject = subject;
            Body = body;
        }
	}
}

