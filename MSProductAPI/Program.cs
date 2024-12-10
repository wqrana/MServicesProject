using Microsoft.EntityFrameworkCore;
using MSAuthenticationAPI.Helper;
using MSProductAPI.DAL;
using MSProductAPI.Model;
using SharedMessageBroker;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// register db Context
builder.Services.AddDbContext<ProductDBContext>(options=> options.UseSqlServer(builder.Configuration.GetConnectionString("ProductDB"))  );
builder.Services.AddTransient<IGenericRespository<Product>,ProductRespository>();
builder.Services.AddTransient<IProductRateRespository<ProductRate>, ProductRateRespository>();
builder.Services.AddControllers(o=>o.Filters.Add<CustomLog>());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IMessageSender>(o => new MessageBroker(builder.Configuration, "Sender"));

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
