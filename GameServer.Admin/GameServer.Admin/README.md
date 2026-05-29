# GameServer Admin Panel

## Popis projektu
Webová administrátorská aplikace pro správu a monitorování herního serveru. Aplikace umožňuje administrátorům sledovat aktuální stav serveru (Dashboard) a spravovat připojené hráče, včetně filtrování a vyhledávání. Obsahuje také lokální logování přístupů do administrace.

## Použité technologie
* **C# a ASP.NET Core (Razor Pages)** - Hlavní framework pro backend i frontend.
* **Entity Framework Core (SQLite)** - Databáze pro ukládání lokálních logů administrátora.
* **Bootstrap 5** - Responzivní design a UI komponenty.
* **HttpClient** - Pro bezpečnou komunikaci s externím herním API.

## Spuštění projektu
1. Naklonujte si repozitář do počítače.
2. Otevřete řešení (`.sln`) ve Visual Studiu.
3. V souboru `appsettings.json` zkontrolujte nebo doplňte konfiguraci (API klíč a URL).
4. Otevřete *Konzolu správce balíčků* (Package Manager Console) a spusťte příkaz `Update-Database`, čímž se vytvoří lokální SQLite databáze pro logy.
5. Spusťte projekt stisknutím `F5` nebo `Ctrl + F5`.

## Konfigurace (`appsettings.json`)
Aplikace vyžaduje pro připojení k hernímu serveru nastavení `GameServerApi` v souboru `appsettings.json`:

```json
"GameServerApi": {
  "BaseUrl": "[https://gameserver-api-a5avbmfwc0bha5c0.westeurope-01.azurewebsites.net/](https://gameserver-api-a5avbmfwc0bha5c0.westeurope-01.azurewebsites.net/)",
  "ApiKey": "gs_practice_2026_student_key"
}