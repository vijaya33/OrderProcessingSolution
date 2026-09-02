using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using OrderProcessing.Client;
using OrderProcessing.Client.Services;
var builder=WebAssemblyHostBuilder.CreateDefault(args); builder.RootComponents.Add<App>("#app"); builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped(_=>new HttpClient { BaseAddress=new Uri(builder.Configuration["ApiBaseUrl"] ?? "https://localhost:7101/") }); builder.Services.AddScoped<IOrderApiClient,OrderApiClient>(); await builder.Build().RunAsync();
