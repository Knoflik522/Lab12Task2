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
            new Company { Name = "������� 1", Address = "���", EmployeesCount = 50 },
            new Company { Name = "������� 2", Address = "����", EmployeesCount = 30 },
            new Company { Name = "������� 3", Address = "�����", EmployeesCount = 100 },
            new Company { Name = "������� 4", Address = "�����", EmployeesCount = 20 },
            new Company { Name = "������� 5", Address = "�����", EmployeesCount = 70 }
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
