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

        /// <summary>
        /// Получить контекст базы данных
        /// </summary>
        /// <typeparam name="T">Тип контекста базы наследуемый от <see cref="DbContext"></typeparam>
        /// <returns>Контекст базы данных наследуемый от <see cref="DbContext"></returns>
        protected T GetContext<T>()
			where T : DbContext
		{
			return ServiceProvider.GetRequiredService<T>();
		}
	}
}

