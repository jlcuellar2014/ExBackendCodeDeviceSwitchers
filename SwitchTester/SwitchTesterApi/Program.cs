using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SwitchTesterApi.Models.Contexts;
using SwitchTesterApi.Services;
using SwitchTesterApi.Settings;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var jwt = new JwtConfiguration
{
    Issuer = builder.Configuration["JwtConfiguration:Issuer"] ?? string.Empty,
    Audience = builder.Configuration["JwtConfiguration:Audience"] ?? string.Empty,
    SecretKey = builder.Configuration["JwtConfiguration:SecretKey"] ?? string.Empty
};

builder.Services.Configure<JwtConfiguration>(options => { 
    options.Issuer = jwt.Issuer;
    options.Audience = jwt.Audience;
    options.SecretKey = jwt.SecretKey;
    options.HoursLife = double.Parse(builder.Configuration["JwtConfiguration:HoursLife"] ?? string.Empty);
});

builder.Services.AddScoped<ISwitchTesterContext,  SwitchTesterContext>();
builder.Services.AddScoped<ISecurityContext, SecurityContext>();

builder.Services.AddScoped<IDevicesService, DevicesService>();
builder.Services.AddScoped<ISwitchesService, SwitchesService>();
builder.Services.AddScoped<ISecurityService, SecurityService>();
builder.Services.AddScoped<IUsersService, UsersService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.SecretKey));

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwt.Issuer,
            ValidAudience = jwt.Audience,
            IssuerSigningKey = issuerSigningKey
        };
    });

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
