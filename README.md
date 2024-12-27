# PlayerNameFilter

**Name:** PlayerNameFilter  
**Author:** Hades.VIP  
**Version:** 1.0.2  
**Game:** Rust  
**Description:** An Oxide/uMod plugin that automatically removes blacklisted references (e.g., website domains, gambling-related keywords) from player display names upon connection.

---

## Summary

PlayerNameFilter helps ensure that no unwanted or spammy references appear in players’ display names on your Rust server. It reads from a user-configurable blacklist of substrings (e.g., `".com"`, `"ruststake"`, `".ru"`) and removes them if they appear in any player’s name.

## Features

- **Automatic Filtering:** Substrings are removed from player names as soon as they finish connecting (`OnPlayerConnected` hook).  
- **Configurable Blacklist:** Edit the JSON config file to add or remove any terms you wish to filter out.  
- **Low Overhead:** The plugin only processes each player’s name once, keeping resource usage minimal.

## Permissions

No permissions required. All functionality runs automatically in the background.

## Configuration

Upon first load, PlayerNameFilter creates a config file named `PlayerNameFilter.json` in your server’s `oxide/config` directory with a default blacklist:

```json
{
  "Blacklist": [
    ".com",
    ".net",
    ".org",
    ".gg",
    "rustysaloon",
    "ruststake",
    "bandit.camp",
    "rustclash",
    "rustchance",
    "rustypot",
    "rustbet",
    "rustcasta",
    "rustcases",
    "banditcamp.com",
    "csgetto.games",
    "madfun.ru",
    ".ru"
  ]
}
```

- **`Blacklist`:** A list of substrings that will be removed from any player name if present (case-insensitive).

### Modifying the Config

1. Stop your server (or unload the plugin using `oxide.unload PlayerNameFilter`).
2. Open `oxide/config/PlayerNameFilter.json`.
3. Edit the `"Blacklist"` array to suit your needs.
4. Save the file.
5. Restart your server (or use `oxide.load PlayerNameFilter`).

## Commands

None. The plugin does not provide player- or admin-facing commands. All operations happen automatically.

## Localization

No language files are provided; log messages and short announcements are in English. You may edit the source if you want to customize language strings.

## Installation

1. **Download** the `PlayerNameFilter.cs` file.  
2. **Place** it in your Rust server’s `oxide/plugins` folder:
   ```
   <server_directory>/oxide/plugins/PlayerNameFilter.cs
   ```
3. **Start or restart** your server. The plugin will compile automatically, create a config file, and filter names upon player connection.

## Uninstallation

1. Stop your server or unload the plugin using `oxide.unload PlayerNameFilter`.  
2. Remove `PlayerNameFilter.cs` from the `oxide/plugins` folder.  
3. Optionally remove the config file `PlayerNameFilter.json` from `oxide/config`.

## Usage

Once installed, whenever a player connects, the plugin:

1. Checks the player’s `displayName`.
2. Removes any blacklisted substrings (case-insensitive).
3. Updates the display name server-side so all players see the filtered version.

A message is logged to your console whenever a name is changed.
