using GameServer.Admin.Dtos;
using GameServer.Admin.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
// 1. Tady jsou ty dva nové odkazy na naši databázi:
using GameServer.Admin.Data;
using GameServer.Admin.Models;

namespace GameServer.Admin.Pages;

public class IndexModel : PageModel
{
    private readonly IGameServerApiClient _apiClient;
    // 2. Tady si připravíme místo pro náš databázový mozek:
    private readonly AppDbContext _dbContext;

    public ServerStatusDto? Status { get; set; }
    public string? ErrorMessage { get; set; }

    // 3. Tady je ten upravený konstruktor (v závorce nově přijímá i AppDbContext):
    public IndexModel(IGameServerApiClient apiClient, AppDbContext dbContext)
    {
        _apiClient = apiClient;
        _dbContext = dbContext; // Uložení do paměti stránky
    }

    public async Task OnGetAsync()
    {
        try
        {
            // 4. A tady hned na začátku try bloku provedeme ten ZÁPIS do deníčku:
            _dbContext.AdminLogs.Add(new AdminLog { ActionPath = "Dashboard (Index)" });
            await _dbContext.SaveChangesAsync();

            // ... (tady normálně pokračuje stahování Status = await _apiClient.GetServerStatusAsync(); atd.)