using Microsoft.EntityFrameworkCore;
using MSCustomerAPI.DAL;
using MSCustomerAPI.Helper;
using MSCustomerAPI.Model;
using SharedMessageBroker;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(o=>o.Filters.Add<CustomLog>());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//registering dbcontext
var provider = builder.Services.BuildServiceProvider();
var config = provider.GetRequiredService<IConfiguration>(); // Make sure to call BuildServiceProvider()>
builder.Services.AddDbContext<CustomerDBContext>(options =>
           options.UseSqlServer(config.GetConnectionString("CustomerDB"),
            o => o.EnableRetryOnFailure()));
//registering repository
builder.Services.AddScoped<IGenericRespository<Customer>, CustomerRespository>();
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
