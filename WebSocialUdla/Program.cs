using BloggieWebProject.Data;
using BloggieWebProject.Repositorio;
using CloudinaryDotNet;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BlogDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("BlogDbConnectionString")));


builder.Services.AddScoped<ITagRepositorio, TagRepositorio>();
builder.Services.AddScoped<IBlogPostRepositorio, BlogPostResositorio>();
builder.Services.AddScoped<IImageRepositorio, CloudinaryImageRepositorio>();

var app = builder.Build();

// Configure the HTTP request pipeline. //LO PRIMERO QUE INICIA DE LA APLICACIÓN
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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
