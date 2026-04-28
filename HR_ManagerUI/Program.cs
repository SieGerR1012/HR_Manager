using HR_ManagerUI;
using HR_ManagerUI.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped<EmployeeService>();

builder.Services.AddScoped(sp => new HttpClient {
    // BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
    BaseAddress = new Uri("https://localhost:7263/")
});

await builder.Build().RunAsync();
