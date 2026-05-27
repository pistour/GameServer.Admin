using GameServer.Admin.Models;
using Microsoft.EntityFrameworkCore;

namespace GameServer.Admin.Data;

public class AppDbContext : DbContext
{
    // Konstruktor, který aplikaci umožní databázi nastavit
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // Tímto říkáme: "Vytvoř v databázi tabulku jménem AdminLogs, která vypadá podle naší třídy"
    public DbSet<AdminLog> AdminLogs { get; set; }
}