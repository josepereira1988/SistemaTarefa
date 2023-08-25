﻿using Dominio.Interface;
using Microsoft.EntityFrameworkCore;
using Persist;
using Persist.Context;
using Persist.Persist;
using Service;
using SistemaTarefa;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Banco de dados
string conectar = DbConf.Conect();
builder.Services.AddDbContext<MyContext>(options => options.UseMySql(conectar, ServerVersion.AutoDetect(conectar)));


//// Configuração de Interface
builder.Services.AddScoped<ITarefaPersist, TarefaPersist>();
builder.Services.AddScoped<ITarefaService, TarefaService>();


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

app.UseAuthorization();

//app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Tarefas}/{action=Index}/{id?}");

DatabaseInitializer.InitializeDatabase(app);

app.Run();
