using GameServer.Admin.Dtos;

namespace GameServer.Admin.Services;

public interface IGameServerApiClient
{
    // Slib, že klient umí stáhnout status (nebo vrátí null při chybě)
    Task<ServerStatusDto?> GetServerStatusAsync();

    // Slib, že klient umí stáhnout seznam hráčů (nebo vrátí null při chybě)
    Task<List<PlayerDto>?> GetPlayersAsync();
}