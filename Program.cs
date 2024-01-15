using BackEnd_KanBan.Api.Sevices.AuthServices;
using BackEnd_KanBan.Api.Sevices.CardServices;
using BackEnd_KanBan.Models.BoardModels;
using BackEnd_KanBan.Repository;
using BackEnd_KanBan.Sevices.BoardServices;
using BackEnd_KanBan.Sevices.ColumnServices;
using BackEnd_KanBan.Sevices.UserServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSqlServer<ApplicationDbContext>(builder.Configuration.GetConnectionString("KanBan"));

builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IBoardServices, BoardServices>();
builder.Services.AddScoped<IColumnServices, ColumnServices>();
builder.Services.AddScoped<ICardServices, CardServices>();
builder.Services.AddScoped<ITokenServices, TokenServices>();

builder.Services.AddAuthentication(x => {
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x => {
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters {
        ValidateLifetime = true,
        ValidateAudience = false,
        ValidateIssuer = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
