using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using LR2.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<ICompanyService, CompanyService>();

builder.Configuration.AddJsonFile("Config/MyInfo.json", optional: false, reloadOnChange: true);

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/info", () =>
{
    var name = app.Configuration["name"];
    var age = app.Configuration["age"];
    var height = app.Configuration["height"];
    return Results.Ok(new { Name = name, Age = age, Height = height });
});

app.Run();
