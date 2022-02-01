using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using suaBaladaAqui.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

string stringConexao = builder.Configuration.GetConnectionString("ConexaoMysql");

builder.Services.AddDbContext<suaBaladaAquiContext>(options =>
    options.UseMySql(stringConexao, ServerVersion.AutoDetect(stringConexao)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseForwardedHeaders(new ForwardedHeadersOptions{
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
    });

    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=SuaBaladaAqui}/{action=Index}/{id?}");

app.Run();
