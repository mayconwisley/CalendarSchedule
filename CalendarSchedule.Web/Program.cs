using Blazored.SessionStorage;
using CalendarSchedule.Web;
using CalendarSchedule.Web.Service;
using CalendarSchedule.Web.Service.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents();

var baseUrl = "https://localhost:7244";

//var baseUrl = "http://192.168.0.102:5050";
//var baseUrl = "http://192.168.10.149:5050";


builder.Services.AddHttpClient("ConexaoApi", con =>
{
	con.BaseAddress = new Uri(baseUrl);
});
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IScheduleUserService, ScheduleUserService>();
builder.Services.AddScoped<IScheduleService, ScheduleService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<ITokenStorageService, TokenStorageService>();
builder.Services.AddScoped<IUserStorageService, UserStorageService>();
builder.Services.AddScoped<IUserContactService, UserContactService>();
builder.Services.AddScoped<IClientResponsibleService, ClientResponsibleService>();
builder.Services.AddScoped<IClientContactService, ClientContactService>();
builder.Services.AddSingleton<ScheduleShareService>();
builder.Services.AddSingleton<ScheduleUserShareService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode();

app.Run();
