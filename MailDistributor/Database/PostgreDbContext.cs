using System;
using System.Reflection;
using MailDistributor.Database.Tables;
using Microsoft.EntityFrameworkCore;

namespace MailDistributor.Database
{
	public class PostgreDbContext : DbContext
	{
		public PostgreDbContext(DbContextOptions<PostgreDbContext> options) : base(options)
		{
		}

        public DbSet<Mail> Mails { get; set; }
    }
}

