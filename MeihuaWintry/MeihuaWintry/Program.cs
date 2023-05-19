using Blazored.LocalStorage;
using MeihuaWintry;
using MeihuaWintry.Services.CaseStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

_ = builder.Services.AddMudServices();
_ = builder.Services.AddBlazoredLocalStorage();

_ = builder.Services.AddCaseStorage();

await builder.Build().RunAsync();
