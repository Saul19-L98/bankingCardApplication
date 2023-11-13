using bankingApp.Restapi.Data;
using bankingApp.Restapi.Models.DTO.TransactionsHistoryDTOs;
using bankingApp.Restapi.Repository.AccountBalanceRepository;
using bankingApp.Restapi.Repository.TransactionsHistoryRepository;
using bankingApp.Restapi.Repository.TransactionsRepository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AccountDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AccountDbConnectionString")));

builder.Services.AddScoped<IAccountBalanceRepository, AccountBalanceRepository>();
builder.Services.AddScoped<ITransactionsRepository, TransactionsRepository>();
builder.Services.AddScoped<ITransactionsHistory, TransactionsHistory>();

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
