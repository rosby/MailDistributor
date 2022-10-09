using MailDistributor.Database;
using MailDistributor.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);


IConfiguration configuration = new ConfigurationBuilder().SetBasePath(builder.Environment.ContentRootPath)
                                                         .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                                         .Build();


builder.Services.AddDbContext<PostgreDbContext>(option =>
{
    option.UseNpgsql(configuration.GetConnectionString("Postgre"));
});

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.AddControllers();
builder.Services.AddTransient<MailService>();

var app = builder.Build();

app.MapControllers();

app.Run();

