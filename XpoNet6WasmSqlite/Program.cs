using Blazored.LocalStorage;
using DevExpress.Xpo.DB;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using XpoNet6WasmSqlite;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddBlazoredLocalStorage();



//builder.Services.AddXpoDefaultUnitOfWork(true, options =>
//            options
//                .UseConnectionString("XpoProvider=SQLite;Data Source=mydb.db")
//                // Remove this line if the database already exists.
//                .UseAutoCreationOption(AutoCreateOption.DatabaseAndSchema)
//                // Pass all of your persistent object types to this method.
//                .UseEntityTypes(new Type[] { typeof(User) })); 
   
await builder.Build().RunAsync();
