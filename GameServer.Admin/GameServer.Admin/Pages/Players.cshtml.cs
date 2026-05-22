using GameServer.Admin.Dtos;
using GameServer.Admin.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GameServer.Admin.Pages;

public class PlayersModel : PageModel
{
    private readonly IGameServerApiClient _apiClient;

    // Kolekce pro hráče a vlastnost pro chybovou hlášku
    public List<PlayerDto>? Players { get; set; }
    public string? ErrorMessage { get; set; }

    public PlayersModel(IGameServerApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task OnGetAsync()
    {
        // Načtení dat z GET /api/players
        Players = await _apiClient.GetPlayersAsync();

        // Ošetření chyby při nedostupném API
        if (Players == null)
        {
            ErrorMessage = "Nepodařilo se načíst seznam hráčů. API neodpovídá.";
        }
    }
}