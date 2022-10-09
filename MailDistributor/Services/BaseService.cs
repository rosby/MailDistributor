using System;
using Microsoft.EntityFrameworkCore;

namespace MailDistributor.Services
{
	public class BaseService
	{
		protected IServiceProvider ServiceProvider { get; set; }

		public BaseService(IServiceProvider serviceProvider)
		{
			ServiceProvider = serviceProvider;
		}

		protected T GetContext<T>()
			where T : DbContext
		{
			return ServiceProvider.GetRequiredService<T>();
		}
	}
}

