using GameServer.Admin.Dtos;
using GameServer.Admin.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GameServer.Admin.Pages;

public class IndexModel : PageModel
{
    private readonly IGameServerApiClient _apiClient;

    public ServerStatusDto? Status { get; set; }
    public string? ErrorMessage { get; set; }

    public IndexModel(IGameServerApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task OnGetAsync()
    {
        try
        {
            Status = await _apiClient.GetServerStatusAsync();
            if (Status == null)
            {
                ErrorMessage = "Server nevrátil žádná data.";
            }
        }
        catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            ErrorMessage = "Špatný API klíč! Přístup byl odepřen.";
        }
        catch (Exception)
        {
            ErrorMessage = "API je nedostupné. Zkontrolujte připojení k serveru.";
        }
    }
}