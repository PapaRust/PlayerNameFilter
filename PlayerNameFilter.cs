using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Oxide.Core;
using Oxide.Core.Plugins;

namespace Oxide.Plugins
{
    [Info("PlayerNameFilter", "Hades.VIP", "1.0.2")]
    [Description("Removes blacklisted references (keywords, websites, etc.) from player names on connect")]
    public class PlayerNameFilter : RustPlugin
    {
        #region Configuration

        private class PluginConfig
        {
            public List<string> Blacklist { get; set; } = new List<string>
            {
                // Default list of blacklisted terms
                ".com",
                ".net",
                ".org",
                ".gg",
                "rustysaloon",
                "howlgg",
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
                "paranoid.gg",
                ".ru"
            };
        }

        private PluginConfig configData;

        protected override void LoadDefaultConfig()
        {
            PrintWarning("Creating a new configuration file for PlayerNameFilter...");

            // Create default config in memory
            configData = new PluginConfig
            {
                Blacklist = new List<string>
                {
                    // Default list of blacklisted terms
                    ".com",
                    ".net",
                    ".org",
                    ".gg",
                    "rustysaloon",
                    "howlgg",
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
                    "paranoid.gg",
                    ".ru"
                }
            };

            SaveConfig(configData);
        }

        protected override void LoadConfig()
        {
            base.LoadConfig();
            try
            {
                configData = Config.ReadObject<PluginConfig>();
                if (configData == null)
                {
                    PrintError("Config file is empty; creating default config...");
                    LoadDefaultConfig();
                }
            }
            catch
            {
                PrintError("Failed to read config; creating default config...");
                LoadDefaultConfig();
            }

            SaveConfig(configData);
        }

        private void SaveConfig(PluginConfig cfg) => Config.WriteObject(cfg, true);

        #endregion

        #region Hooks

        private void Init()
        {
            if (configData == null)
            {
                LoadDefaultConfig();
            }
        }

        /// <summary>
        /// This is the Rust-specific hook for when a player finishes connecting.
        /// </summary>
        /// <param name="player">The BasePlayer who connected</param>
        private void OnPlayerConnected(BasePlayer player)
        {
            if (player == null) return;

            string originalName = player.displayName;
            string filteredName = FilterName(originalName);

            if (!string.Equals(originalName, filteredName, StringComparison.OrdinalIgnoreCase))
            {
                player.displayName = filteredName;
                // The "_displayName" field is often updated to ensure immediate changes
                player._displayName = filteredName;
                player.SendNetworkUpdate();

                Puts($"Changed player name from '{originalName}' to '{filteredName}' (removed blacklisted terms).");
            }
        }

        #endregion

        #region Filtering Logic

        private string FilterName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return name;

            // For each blacklisted keyword, remove it (case-insensitive)
            foreach (var keyword in configData.Blacklist)
            {
                if (!string.IsNullOrEmpty(keyword))
                {
                    name = ReplaceCaseInsensitive(name, keyword, "");
                }
            }

            return name.Trim();
        }

        private string ReplaceCaseInsensitive(string source, string search, string replacement)
        {
            return Regex.Replace(
                source,
                Regex.Escape(search),
                replacement,
                RegexOptions.IgnoreCase
            );
        }

        #endregion
    }
}