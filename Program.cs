using BackEnd_KanBan.Api.Sevices.CardServices;
using BackEnd_KanBan.Models.BoardModels;
using BackEnd_KanBan.Repository;
using BackEnd_KanBan.Sevices.BoardServices;
using BackEnd_KanBan.Sevices.ColumnServices;
using BackEnd_KanBan.Sevices.UserServices;

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
