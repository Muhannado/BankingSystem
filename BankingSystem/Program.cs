using BankingSystem.Common;
using BankingSystem.Data;
using BankingSystem.Managers;
using BankingSystem.Managers.Contracts;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using BankingSystem.Data.Mock;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
// Add services to the container.

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddDbContext<BankingSystemContext>(config =>
{
    config.UseInMemoryDatabase("BankingSystemMemoryDb");
});

services.AddScoped<IUsersManager, UsersManager>();
services.AddScoped<IAccountManager, AccountManager>();

services.AddValidatorsFromAssemblyContaining<IAssemblyMarker>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<BankingSystemContext>())
{
    context?.Database.EnsureCreated();
    context?.Users.AddRange(UsersMock.Users);
    context?.SaveChanges();
}

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
