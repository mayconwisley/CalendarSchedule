using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using ScheduleRooms.API.Data;
using ScheduleRooms.API.Repository;
using ScheduleRooms.API.Repository.Interface;
using ScheduleRooms.API.Service;
using ScheduleRooms.API.Service.Interface;
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
builder.Services.AddCors();

var passDatabase = Environment.GetEnvironmentVariable("SQLSenha", EnvironmentVariableTarget.Machine);

var strDataBase = builder.Configuration.GetConnectionString("ScheduleConect")!.Replace("{{pass}}", passDatabase);
builder.Services.AddDbContext<ScheduleContext>(cd => cd.UseSqlServer(strDataBase));
builder.Services.AddScoped<IRoomRepository, RoomRespository>();
builder.Services.AddScoped<IScheduleRepository, ScheduleRepository>();
builder.Services.AddScoped<IScheduleService, ScheduleService>();
builder.Services.AddScoped<IRoomService, RoomService>();

var app = builder.Build();

app.UseCors(policy => policy.WithOrigins("https://localhost:7296",
                                                "https://192.168.10.149:7296",
                                                "http://192.168.0.102:5051",
                                                "http://192.168.10.149:5051",
                                                "http://localhost:5051")
    .AllowAnyMethod()
    .AllowAnyHeader()
    .WithHeaders(HeaderNames.ContentType)
);
;
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
