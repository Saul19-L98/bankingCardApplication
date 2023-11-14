using bankingApp.WebApp.Repositories.AccountBalanceRepository;
using bankingApp.WebApp.Repositories.TransactionsHistoryRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IAccountBalanceRepository,AccountBalanceRepository>();
builder.Services.AddScoped<ITransactionsHistoryRepository, TransactionsHistoryRepository>();

// Register HttpClient for your repository
builder.Services.AddHttpClient<IAccountBalanceRepository, AccountBalanceRepository>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7070/"); 
    client.BaseAddress = new Uri("http://localhost:5272");
});
builder.Services.AddHttpClient<ITransactionsHistoryRepository, TransactionsHistoryRepository>(client =>
{
	client.BaseAddress = new Uri("https://localhost:7070/");
	client.BaseAddress = new Uri("http://localhost:5272");
});

var app = builder.Build();

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
