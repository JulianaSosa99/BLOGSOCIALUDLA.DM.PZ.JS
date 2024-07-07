using WebSocialUdla.Data;
using BloggieWebProject.Repositorio;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebSocialUdla.Repositorio;
using WebSocialUdla.Services;
using WebSocialUdla.Servicios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHttpClient<IBlogFicaService, BlogFicaService>(client =>
{
	client.BaseAddress = new Uri(builder.Configuration["ApiUrl"]); 
});

builder.Services.AddHttpClient<IBlogNodoService, BlogNodoService>(client =>
{
	client.BaseAddress = new Uri(builder.Configuration["ApiUrl"]);
});
builder.Services.AddHttpClient<ICommentService, CommentService>(client =>
{
	client.BaseAddress = new Uri(builder.Configuration["ApiUrl"]); 
});
builder.Services.AddHttpClient<ITagService, TagService>(client =>
{
	client.BaseAddress = new Uri(builder.Configuration["ApiUrl"]);
});

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BlogDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("BlogDbConnectionString")));

builder.Services.AddDbContext<AuthDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("BlogAuthDbConnectionString")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AuthDbContext>();

builder.Services.Configure<IdentityOptions>(options => 
{ 
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;

});
// Configurar las rutas de autenticación y autorización
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Cuenta/Login";
    options.AccessDeniedPath = "/Cuenta/AccesoDenegado";
});

//builder.Services.AddScoped<ITagRepositorio, TagRepositorio>();
//builder.Services.AddScoped<IBlogPostRepositorio, BlogPostResositorio>();
//builder.Services.AddScoped<IImageRepositorio, CloudinaryImageRepositorio>();
//builder.Services.AddScoped<IUserRepositorio, UserRepositorio>();
//builder.Services.AddScoped<IBlogPostLikeRepositorio, BlogPostLikeRepositorio>();
//builder.Services.AddScoped<IBlogPostCommentRepositorio, BlogPostCommentRepositorio>();


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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
