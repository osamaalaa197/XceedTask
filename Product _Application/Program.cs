using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Product__Application.Context.DbContext;
using Product__Application.Models;
using Product__Application.Repository.ProductRepository;
using Product__Application.Repository.UserRepository;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();



#region connection DateBase
//get Connection String from secret 
var connectionString = builder.Configuration.GetConnectionString("MySqlConnection");
// Add Context to connection to database 
builder.Services.AddDbContext<DataBaseContext>(options =>
{
    options.UseMySql(connectionString, new MariaDbServerVersion("8.0.34"));
});
#endregion

#region injection
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
#endregion

#region identity
builder.Services.AddIdentity<UserApp, IdentityRole>(options =>
{
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;

}).AddEntityFrameworkStores<DataBaseContext>();
#endregion




var app = builder.Build();



//Configure the HTTP request pipeline.
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
    pattern: "{controller=Product}/{action=GetAllProduct}/{id?}");

app.Run();
