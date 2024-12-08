using Lab12Task2.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    if (!context.Companies.Any())
    {
        context.Companies.AddRange(
            new Company { Name = "Компанія 1", Address = "Київ", EmployeesCount = 50 },
            new Company { Name = "Компанія 2", Address = "Львів", EmployeesCount = 30 },
            new Company { Name = "Компанія 3", Address = "Одеса", EmployeesCount = 100 },
            new Company { Name = "Компанія 4", Address = "Харків", EmployeesCount = 20 },
            new Company { Name = "Компанія 5", Address = "Дніпро", EmployeesCount = 70 }
        );
        context.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=CompaniesController}/{action=Index}/{id?}");

app.Run();
