using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using front.Services;

var builder = WebApplication.CreateBuilder(args);


string server = "https://localhost:7029";  // altera a porta se mudar, mas com essa string não precisa alterar nos demais locais ****

// if (builder.Environment.IsProduction())
// {
//     server = "link da nuvem";
// }

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton(
    provider => new UserService(server));  //criado o servico com base no servico global, pode ser usado em toda a aplicação ****

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
