using Microsoft.EntityFrameworkCore;
using ProdutoProntoDigital.Data;
using ProdutoProntoDigital.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register the ApplicationDbContext with the service container
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlServerOptions => sqlServerOptions.EnableRetryOnFailure()
    ));

// Services
builder.Services.AddScoped<ProdutoService>();
builder.Services.AddScoped<CategoriaServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Update the default route to point to Produto/Index
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Produto}/{action=Index}/{id?}");

app.Run();
