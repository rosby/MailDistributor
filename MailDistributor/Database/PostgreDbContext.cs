using System;
using System.Reflection;
using MailDistributor.Database.Tables;
using Microsoft.EntityFrameworkCore;

namespace MailDistributor.Database
{
	/// <summary>
	/// Контекст базы данных
	/// </summary>
	public class PostgreDbContext : DbContext
	{
		public PostgreDbContext(DbContextOptions<PostgreDbContext> options) : base(options)
		{
		}

		/// <summary>
		/// Отправленные письма
		/// </summary>
        public DbSet<Mail> Mails { get; set; }
    }
}

