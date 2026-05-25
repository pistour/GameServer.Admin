using GameServer.Admin.Dtos;
using GameServer.Admin.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

namespace GameServer.Admin.Pages;

public class PlayersModel : PageModel
{
    private readonly IGameServerApiClient _apiClient;

    public List<PlayerDto>? Players { get; set; }
    public string? ErrorMessage { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? SearchQuery { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? StatusFilter { get; set; }

    public PlayersModel(IGameServerApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task OnGetAsync()
    {
        // 1. Stažení VŠECH hráčů z API
        Players = await _apiClient.GetPlayersAsync();

        if (Players == null)
        {
            ErrorMessage = "Nepodařilo se načíst seznam hráčů. API neodpovídá.";
            return;
        }

        // 2. FILTROVÁNÍ: Podle vyhledávacího pole (Nickname)
        if (!string.IsNullOrWhiteSpace(SearchQuery))
        {
            Players = Players
                .Where(p => p.Nickname.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        // 3. FILTROVÁNÍ: Podle vybraného stavu z roletky
        if (!string.IsNullOrEmpty(StatusFilter) && StatusFilter != "all")
        {
            if (StatusFilter == "online")
            {
                Players = Players.Where(p => p.IsOnline && !p.IsBanned).ToList();
            }
            else if (StatusFilter == "offline")
            {
                Players = Players.Where(p => !p.IsOnline && !p.IsBanned).ToList();
            }
            else if (StatusFilter == "banned")
            {
                Players = Players.Where(p => p.IsBanned).ToList();
            }
        }
    }
}