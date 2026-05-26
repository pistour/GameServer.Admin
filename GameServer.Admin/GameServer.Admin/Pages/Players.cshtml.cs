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
        try
        {
            Players = await _apiClient.GetPlayersAsync();

            if (Players == null)
            {
                ErrorMessage = "Server nevrátil žádná data.";
                return;
            }

            // Filtrace z předchozího dne
            if (!string.IsNullOrWhiteSpace(SearchQuery))
            {
                Players = Players.Where(p => p.Nickname.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrEmpty(StatusFilter) && StatusFilter != "all")
            {
                if (StatusFilter == "online") Players = Players.Where(p => p.IsOnline && !p.IsBanned).ToList();
                else if (StatusFilter == "offline") Players = Players.Where(p => !p.IsOnline && !p.IsBanned).ToList();
                else if (StatusFilter == "banned") Players = Players.Where(p => p.IsBanned).ToList();
            }
        }
        catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            ErrorMessage = "⛔ Špatný API klíč! Zkontrolujte konfiguraci.";
        }
        catch (Exception)
        {
            ErrorMessage = "🔌 API je momentálně nedostupné. Zkuste to prosím později.";
        }
    }
}