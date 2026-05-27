using Microsoft.EntityFrameworkCore;
using GameServer.Admin.Data;
using GameServer.Admin.Options;
using GameServer.Admin.Services;

var builder = WebApplication.CreateBuilder(args);

// Zapojení naší SQLite databáze
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=adminlogs.db"));

// Add services to the container.
builder.Services.AddRazorPages();

// Registrace konfigurace (Options pattern)
builder.Services.Configure<GameServerApiOptions>(
    builder.Configuration.GetSection("GameServerApi"));

// Registrace API klienta
builder.Services.AddHttpClient<IGameServerApiClient, GameServerApiClient>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
