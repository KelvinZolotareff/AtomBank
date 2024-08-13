using AtomBank.Data;
using AtomBank.Services;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

var builder = WebApplication.CreateBuilder(args);

// Adiciona o DbContext ao contêiner de serviços
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=AtomBank;Integrated Security=True;Trust Server Certificate=True")
    );

// Adiciona serviços ao contêiner
builder.Services.AddControllersWithViews();
builder.Services.AddAuthorization(); // Adiciona o serviço de autorização
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

app.UseAuthorization(); // Certifique-se de que o middleware de autorização está registrado

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
