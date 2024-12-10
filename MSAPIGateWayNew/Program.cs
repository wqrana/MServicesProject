using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using MSAPIGateWay;

var builder = WebApplication.CreateBuilder(args);
//add ocelot to configuration
builder.Configuration.AddJsonFile("ocelot.json");
//Add authentication
var jwtSettings = new APIGatewayJWTSettings();
builder.Configuration.GetSection("APIGateWayJwtSettings").Bind(jwtSettings);
//builder.Services.AddSingleton(jwtSettings);
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
//add ocelot to service
builder.Services.AddOcelot();
//.AddCacheManager(op=> op.WithDictionaryHandle());
//add cors for running angular frontend
builder.Services.AddCors(options => options.AddPolicy("angular-frontend", policy =>
{
    policy
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()
    .WithOrigins("http://localhost:4200");
})
);
var app = builder.Build();
app.UseCors("angular-frontend");
//jwt token authentication
app.UseAuthentication();
//use ocelot to route traffic
app.UseOcelot();
app.Run();
