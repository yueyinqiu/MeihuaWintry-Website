using Blazored.LocalStorage;
using MeihuaWintry;
using MeihuaWintry.Services.CaseStorage;
using MeihuaWintry.Services.ZhouyiReferencing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

_ = builder.Services.AddMudServices(config => {
    config.SnackbarConfiguration.PreventDuplicates = false;
});
_ = builder.Services.AddBlazoredLocalStorage();

_ = builder.Services.AddZhouyiProvider(builder.HostEnvironment.BaseAddress);
_ = builder.Services.AddCaseStorage();

await builder.Build().RunAsync();
