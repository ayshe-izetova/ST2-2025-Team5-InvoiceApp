using Microsoft.EntityFrameworkCore;
using ST2_2025_Team5_InvoiceApp.Services;
using ST2_2025_Team5_InvoiceApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Design Pattern: Dependency Injection
// Register MVC controllers and views
builder.Services.AddControllersWithViews();

// Register database context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions =>
        {
            sqlOptions.CommandTimeout(60); // 60 секунди
            sqlOptions.EnableRetryOnFailure(5);
        }
    ));


// Register custom services (DAO, Facade, Singleton)
builder.Services.AddScoped<InvoiceDAO>();
builder.Services.AddScoped<InvoiceFacade>();
builder.Services.AddSingleton<LogService>();

// Optional: LLM intelligent search integration
//builder.Services.Configure<LlmOptions>(builder.Configuration.GetSection("Llm"));
//builder.Services.AddHttpClient<LlmClient>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseStatusCodePages();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
