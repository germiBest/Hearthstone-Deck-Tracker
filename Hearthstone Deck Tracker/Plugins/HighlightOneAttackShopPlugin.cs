using System;
using System.Linq;
using System.Windows.Controls;
using Hearthstone_Deck_Tracker.Hearthstone;
using Hearthstone_Deck_Tracker.Hearthstone.Entities;
using Hearthstone_Deck_Tracker.Plugins;

namespace Hearthstone_Deck_Tracker.Plugins
{
	public class HighlightOneAttackShopPlugin : IPlugin
	{
		public string Name => "Highlight 1 Attack Shop Cards";
		public string Description => "Highlights Battlegrounds shop cards with 1 attack.";
		public string ButtonText => "No Action";
		public string Author => "AI";
		public Version Version => new Version(1, 0, 0);
		public MenuItem MenuItem => null;

		public void OnLoad() { }
		public void OnUnload() { }
		public void OnButtonPress() { }

		public void OnUpdate()
		{
			// Only run if in Battlegrounds and shop is open
			if (!Core.Game.IsBattlegroundsMatch)
				return;

			// Try to get shop entities (offered minions)
			var player = Core.Game.Player;
			if (player == null)
				return;

			// OfferedEntityIds is used for choices, including shop
			foreach (var entity in player.OfferedEntities)
			{
				if (entity.IsMinion && entity.Attack == 1)
				{
					// Print debug info to console
					Console.WriteLine($"[HighlightOneAttackShopPlugin] 1-attack shop card detected: {entity.LocalizedName} (ID: {entity.CardId})");
					// Easiest way: set a tag or property for highlight (if supported)
					entity.Tags[HearthDb.Enums.GameTag.TAG_SCRIPT_DATA_NUM_1] = 9999; // Arbitrary value to mark
				}
			}
		}
	}
} 