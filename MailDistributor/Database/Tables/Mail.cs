using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MailDistributor.Database.Tables
{
    /// <summary>
    /// Отправленное письмо
    /// </summary>
	[Table("mail", Schema = "dbo")]
	public class Mail
	{
        /// <summary>
        /// Идентификатор
        /// </summary>
		[Key, Column("id")]
		public int Id { get; set; }

        /// <summary>
        /// Адрес отправителя email 
        /// </summary>
        [Column("source")]
        public string Source { get; set; }

        /// <summary>
        /// Адрес получателя email
        /// </summary>
        [Column("recipient")]
        public string Recipient { get; set; }

        /// <summary>
        /// Заголовок письма
        /// </summary>
        [Column("subject")]
		public string? Subject { get; set; }

        /// <summary>
        /// Тело письма
        /// </summary>
        [Column("body")]
        public string Body { get; set; }

        /// <summary>
        /// Результат отправки 
        /// </summary>
        [Column("result")]
        public string Result { get; set; }

        /// <summary>
        /// Текст ошибки при отправке письма
        /// </summary>
        [Column("failedmessage")]
        public string? FailedMessage { get; set; }

        /// <summary>
        /// Дата отправки письма
        /// </summary>
        [Column("senddate")]
        public DateTime SendDate { get; set; }

        public Mail()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source">Email отправителя</param>
        /// <param name="recipient">Email получателя</param>
        /// <param name="subject">Заголовок письма</param>
        /// <param name="body">Тело письма</param>
        public Mail(string source, string recipient, string? subject, string body)
        {
            Source = source;
            Recipient = recipient;
            Subject = subject;
            Body = body;
        }
	}
}

