using AtomBank.Data;
using AtomBank.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using static System.Runtime.InteropServices.JavaScript.JSType;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>             
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);


builder.Services.AddControllersWithViews();
builder.Services.AddAuthorization(); 
builder.Services.AddTransient<TransactionService>();

var app = builder.Build();

// Configura o pipeline de requisições HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
