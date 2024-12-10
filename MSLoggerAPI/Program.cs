using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using MSLoggerAPI.DAL;
using MSLoggerAPI.LogServiceHelper;
using MSLoggerAPI.Model;
using SharedMessageBroker;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<LoggerDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("LoggerDB")));
builder.Services.AddTransient<IGenericRespository<AppLogger>, LoggerRespository>();
builder.Services.AddTransient <IMessageReceiver>(o=> new MessageBroker(builder.Configuration, "Receiver"));
builder.Services.AddHostedService<LogBGHelper>();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
