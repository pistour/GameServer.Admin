using GameServer.Admin.Dtos;
using GameServer.Admin.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GameServer.Admin.Pages;

public class IndexModel : PageModel
{
    private readonly IGameServerApiClient _apiClient;

    // Vlastnosti přístupné pro HTML šablonu
    public ServerStatusDto? Status { get; set; }
    public string? ErrorMessage { get; set; }

    public IndexModel(IGameServerApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task OnGetAsync()
    {
        // Stažení dat z API
        Status = await _apiClient.GetServerStatusAsync();

        // Pokud se stahování nepovedlo (vrátilo null), naplníme chybovou zprávu
        if (Status == null)
        {
            ErrorMessage = "Nepodařilo se připojit k API herního serveru. Zkontrolujte připojení a API klíč.";
        }
    }
}