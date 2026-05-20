using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using GameServer.Admin.Dtos;
using GameServer.Admin.Options;

namespace GameServer.Admin.Services;

public class GameServerApiClient : IGameServerApiClient
{
    private readonly HttpClient _httpClient;

    // Přes konstruktor si necháme dodat HttpClienta a naše tajné nastavení
    public GameServerApiClient(HttpClient httpClient, IOptions<GameServerApiOptions> options)
    {
        _httpClient = httpClient;

        // Nastavíme základní adresu z appsettings.json
        _httpClient.BaseAddress = new Uri(options.Value.BaseUrl);

        // Nastavíme tajnou hlavičku X-API-KEY, kterou server vyžaduje
        _httpClient.DefaultRequestHeaders.Add("X-API-KEY", options.Value.ApiKey);
    }

    public async Task<ServerStatusDto?> GetServerStatusAsync()
    {
        try
        {
            // Zkusíme stáhnout data ze specifické koncovky
            var response = await _httpClient.GetAsync("api/server/status");

            // Pokud server vrátí chybu (např. 401 kvůli špatnému klíči), hodí to výjimku
            response.EnsureSuccessStatusCode();

            // Překlopíme JSON z webu rovnou do naší přepravky
            return await response.Content.ReadFromJsonAsync<ServerStatusDto>();
        }
        catch (Exception ex)
        {
            // Zpracování chyb - dočasně vypíšeme do konzole a vrátíme null
            Console.WriteLine($"Chyba při stahování statusu: {ex.Message}");
            return null;
        }
    }

    public async Task<List<PlayerDto>?> GetPlayersAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/server/players");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<PlayerDto>>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Chyba při stahování hráčů: {ex.Message}");
            return null;
        }
    }
}