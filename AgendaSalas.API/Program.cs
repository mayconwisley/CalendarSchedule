using AgendaSalas.API.Data;
using AgendaSalas.API.Repository;
using AgendaSalas.API.Repository.Interface;
using AgendaSalas.API.Service;
using AgendaSalas.API.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
    x.JsonSerializerOptions.PropertyNamingPolicy = null;
    x.JsonSerializerOptions.WriteIndented = true;
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    x.JsonSerializerOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(System.Text.Unicode.UnicodeRanges.All);
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var strDataBase = builder.Configuration.GetConnectionString("AgendaConect");
builder.Services.AddDbContext<AgendaContext>(cd => cd.UseSqlServer(strDataBase));
builder.Services.AddScoped<ISalaRepository, SalaRespository>();
builder.Services.AddScoped<IAgendaRepository, AgendaRepository>();
builder.Services.AddScoped<ISalaService, SalaService>();




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
