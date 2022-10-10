using MailDistributor.Database;
using MailDistributor.Services;
using Microsoft.AspNetCore.Rewrite;
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

builder.Services.AddControllers();
builder.Services.AddTransient<MailService>();
builder.Services.AddSwaggerGen();
builder.Services.AddMvcCore();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

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

