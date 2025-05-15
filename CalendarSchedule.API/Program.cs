using CalendarSchedule.API.Data;
using CalendarSchedule.API.Repository;
using CalendarSchedule.API.Repository.Interface;
using CalendarSchedule.API.Service;
using CalendarSchedule.API.Service.Interface;
using CalendarSchedule.API.Utility;
using CalendarSchedule.API.Utility.Interface;
using CalendarSchedule.Models.Abstractions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
	options.SuppressModelStateInvalidFilter = true;
});
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
builder.Services.AddSwaggerGen(
	c =>
	{
		c.SwaggerDoc("v1", new OpenApiInfo
		{
			Title = "Agenda de Visitas a Clientes",
			Version = "v1",
			Description = "Documentação da API de Agenda",
			Contact = new OpenApiContact
			{
				Name = "Maycon Wisley",
				Url = new Uri("https://github.com/mayconwisley")
			}
		});
		c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
		{
			Name = "Authorization",
			In = ParameterLocation.Header,
			Type = SecuritySchemeType.ApiKey,
			Scheme = "Bearer"
		});
		c.AddSecurityRequirement(new OpenApiSecurityRequirement()
		{
			{
				new OpenApiSecurityScheme
				{
					Reference = new OpenApiReference
					{
						Type = ReferenceType.SecurityScheme,
						Id = "Bearer"
					},
					Scheme = "oauth2",
					Name = "Bearer",
					In = ParameterLocation.Header,
				},
				new List<string>()
			}
		});
	});
builder.Services.AddCors();

var passDatabase = Environment.GetEnvironmentVariable("SQLPassword", EnvironmentVariableTarget.Machine);
var userDB = Environment.GetEnvironmentVariable("SQLUser", EnvironmentVariableTarget.Machine);

var strDataBase = builder.Configuration
				 .GetConnectionString("ScheduleConect")!
				 .Replace("{{pass}}", passDatabase)
				 .Replace("{{userDB}}", userDB);

builder.Services.AddDbContext<ScheduleContext>(cd => cd.UseSqlServer(strDataBase, s =>
{
	s.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
}));
builder.Services.AddScoped<IScheduleRepository, ScheduleRepository>();
builder.Services.AddScoped<IScheduleService, ScheduleService>();
builder.Services.AddScoped<IScheduleUserRepository, ScheduleUserRepository>();
builder.Services.AddScoped<IScheduleUserService, ScheduleUserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserContactRepository, UserContactRepository>();
builder.Services.AddScoped<IUserContactService, UserContactService>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IClientResponsibleRepository, ClientResponsibleRepository>();
builder.Services.AddScoped<IClientResponsibleService, ClientResponsibleService>();
builder.Services.AddScoped<IClientContactRepository, ClientContactRepository>();
builder.Services.AddScoped<IClientContactService, ClientContactService>();
builder.Services.AddScoped<IEncryptionUtility, EncryptionUtility>();
builder.Services.AddScoped<IDecryptionUtility, DecryptionUtility>();
builder.Services.AddScoped<IGetTokenService, GetTokenService>();

string strKey = builder.Configuration.GetSection("JWT")["Secret"]!;
var key = Encoding.ASCII.GetBytes(strKey);
builder.Services.AddAuthentication(opt =>
{
	opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
	.AddJwtBearer(opt =>
	{
		opt.Events = new JwtBearerEvents
		{
			OnChallenge = async context =>
			{
				context.HandleResponse();
				context.Response.StatusCode = StatusCodes.Status401Unauthorized;
				context.Response.ContentType = "application/json";
				var result = Result.Failure(Error.Unauthorized("Token inválido ou expirado"));
				await context.Response.WriteAsJsonAsync(result.Error);
			},
			OnForbidden = async context =>
			{
				context.Response.StatusCode = StatusCodes.Status403Forbidden;
				context.Response.ContentType = "application/json";
				var result = Result.Failure(Error.Forbidden("Acesso negado"));
				await context.Response.WriteAsJsonAsync(result);
			}
		};

		opt.RequireHttpsMetadata = false;
		opt.SaveToken = true;
		opt.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuerSigningKey = true,
			IssuerSigningKey = new SymmetricSecurityKey(key),
			ValidateIssuer = false,
			ValidateAudience = false
		};
	});

var app = builder.Build();
app.Use(async (context, next) =>
{
	await next();

	switch (context.Response.StatusCode)
	{
		case StatusCodes.Status405MethodNotAllowed:
			context.Response.ContentType = "application/json";
			var result = Result.Failure(Error.MethodNotAllowed("Metodo não implementado ou não permitido"));
			await context.Response.WriteAsJsonAsync(result.Error);
			break;
		case StatusCodes.Status500InternalServerError:
			context.Response.ContentType = "application/json";
			var error = Result.Failure(Error.Internal("Erro interno no servidor"));
			await context.Response.WriteAsJsonAsync(error.Error);
			break;
		default:
			break;
	}
});
app.UseCors(policy => policy.WithOrigins("https://localhost:7296")
	.AllowAnyMethod()
	.AllowAnyHeader()
	.WithHeaders(HeaderNames.ContentType)
);

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Schedule v1"));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
