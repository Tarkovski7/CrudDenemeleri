using System.Diagnostics;
using CrudDenemeleri.Context;
using CrudDenemeleri.MiddleWares;
using CrudDenemeleri.SeedData;
using CrudDenemeleri.Services.Concretes;
using CrudDenemeleri.Services.Interfaces;
using CrudDenemeleri.Validations;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder
    .Services.AddControllersWithViews()
    .AddFluentValidation(fv =>
    {
        fv.RegisterValidatorsFromAssemblyContaining<PersonValidator>();
    });
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddDbContext<PersonDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConStr"))
);

// builder.Services.AddFluentValidation();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        SeedDatas.Initialize(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//middleware i pipeline a eklemek
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseStatusCodePagesWithRedirects("/Error/{0}");

//HTTP isteklerini HTTPS e yönlendirir
app.UseHttpsRedirection();

//Static dosyalar(resimler , JS ,CSS) için
app.UseStaticFiles();

//İsteği uygun rotaya yönlendirir.
app.UseRouting();

//Yetkilendirme Kontrolü yapar.
app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=GetAll}/{id?}");

app.Run();
