# SerialWebAPI

Ein einfaches ASP.NET Core Web API-Projekt zur Weiterleitung von API-Befehlen an eine serielle Schnittstelle über USB.

## Verwendung

1. Projekt clonen und Abhängigkeiten installieren:

```bash
dotnet restore
dotnet run
```

2. POST-Anfrage an die API senden:

```http
POST http://localhost:5000/api/command
Content-Type: application/json

{
  "command": "DEIN_BEFEHL"
}
```

## Konfiguration
Passe den COM-Port und die Baudrate in `appsettings.json` an dein Gerät an.

## JetBrains Rider & Visual Studio
Falls du Rider oder Visual Studio nutzt, kannst du das Projekt direkt über die `SerialWebAPI.sln`-Datei öffnen.