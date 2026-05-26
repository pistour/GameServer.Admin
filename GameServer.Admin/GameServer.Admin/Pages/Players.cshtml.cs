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
        Players = await _apiClient.GetPlayersAsync();

        if (Players == null)
        {
            ErrorMessage = "Nepodařilo se načíst seznam hráčů. API neodpovídá.";
            return;
        }

        if (!string.IsNullOrWhiteSpace(SearchQuery))
        {
            Players = Players
                .Where(p => p.Nickname.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

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