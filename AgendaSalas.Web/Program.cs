using AgendaSalas.Web;
using AgendaSalas.Web.Service;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var baseUrl = "https://localhost:7244";

builder.Services.AddHttpClient("ConexaoApi", con =>
{
    con.BaseAddress = new Uri(baseUrl);
});

builder.Services.AddScoped<ISalaService, SalaService>();


await builder.Build().RunAsync();
