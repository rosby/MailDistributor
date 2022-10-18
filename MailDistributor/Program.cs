using MailDistributor.Database;
using MailDistributor.Options;
using MailDistributor.Services;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);




#region Configuration

IConfiguration configuration = new ConfigurationBuilder().SetBasePath(builder.Environment.ContentRootPath)
                                                         .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                                         .Build();

var a = builder.Configuration;
builder.Services.Configure<SmtpClientOption>(configuration.GetSection(SmtpClientOption.ConfigurationSection));
#endregion

#region DbContext
builder.Services.AddDbContext<PostgreDbContext>(option =>
{
    option.UseNpgsql(configuration.GetConnectionString("Postgre"));
});
#endregion

#region Services
builder.Services.AddControllers();
builder.Services.AddTransient<MailService>();
builder.Services.AddSwaggerGen();
builder.Services.AddMvcCore();

#endregion

#region AppBehavior

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

#endregion

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "MailDistributor");
});

RewriteOptions redirections = new();
redirections.AddRedirect("^$", "swagger");
app.UseRewriter(redirections);

app.MapControllers();
app.Run();

