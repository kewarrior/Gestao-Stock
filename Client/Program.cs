using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Client;
using Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

string enderecoDoServidorWebAPI = "https://localhost:7015";


builder.Services.AddScoped<CarrinhoService>();
builder.Services.AddScoped(sp => new HttpClient
{ 
    BaseAddress = new Uri(enderecoDoServidorWebAPI) 
});



await builder.Build().RunAsync();
