using Banking.Account.Command.Aplication.Features.BankAccounts.Commands.OpenAccount;
using Banking.Account.Command.Aplication.Models;
using Banking.Account.Command.Infrastucture;
using MediatR;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<KafkaSettings>(builder.Configuration.GetSection("KafkaSettings"));
builder.Services.Configure<MongoSettings>(builder.Configuration.GetSection("MongoSettings"));

//builder.Services.AddMediatR(typeof(OpenAccountsCommand).Assembly);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(OpenAccountCommandHandler).GetTypeInfo().Assembly));

builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("corsPolicy", builder => builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()
            );
});

var app = builder.Build();


// Configure the HTTP request pipeline.sd
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.UseCors("corsPolicy");

app.MapControllers();

app.Run();
