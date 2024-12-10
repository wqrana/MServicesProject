using Microsoft.AspNetCore.Authentication.JwtBearer;
using MSAuthenticationAPI.Model;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MSAuthenticationAPI.DAL;
using Microsoft.EntityFrameworkCore;
using SharedMessageBroker;
using MSAuthenticationAPI.Helper;

var builder = WebApplication.CreateBuilder(args);
//jwt settings 
var jwtSettings = new JWTSettings();
builder.Configuration.GetSection("JwtSettings").Bind(jwtSettings);
builder.Services.AddSingleton(jwtSettings);
// add jwt authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))


    };
});

builder.Services.AddAuthorization();
// Add services to the container.
//registering dbcontext
//var provider = builder.Services.BuildServiceProvider();
//var config = provider.GetRequiredService<IConfiguration>(); // Make sure to call BuildServiceProvider()>
//builder.Services.AddDbContext<ApplicationUserDBContext>(options =>
//           options.UseSqlServer(config.GetConnectionString("UserDBConnection"),
//            o => o.EnableRetryOnFailure()));

builder.Services.AddDbContext<ApplicationUserDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("UserDBConnection")));
builder.Services.AddControllers();//(o=>o.Filters.Add<CustomLog>());
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
