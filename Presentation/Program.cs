using Application.Interface.InterfaceCommand;
using Application.Interface.InterfaceQuerys;
using Application.Interface.InterfaceService;
using Application.Service;
using Infrastructure.Command;
using Infrastructure.Data;
using Infrastructure.Querys;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Data base
var conectionString = builder.Configuration["ConnectionString"];
builder.Services.AddDbContext<MicroServiceUserContext>(options => options.UseSqlServer(conectionString));

//Inyeccion de dependencia
builder.Services.AddTransient<IUserQuery, UserQuery>();
builder.Services.AddTransient<IUserCommand, UserCommand>();
builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddTransient<IAuthService, AuthService>();

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
