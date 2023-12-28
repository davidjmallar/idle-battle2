using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
	public static class DataService
	{
		public static CreatureData GetCreature(string creatureId)
		{
			return new CreatureData()
			{
				DisplayName = creatureId,
				SpriteId = creatureId,
			};
		}
		public static SpellData GetSpell(string spellId)
		{
			return new SpellData()
			{
				Periodicity = 1.0f
			};
		}

		public static SaveData GetSaveData()
		{
			return new SaveData()
			{
				Creatures = new List<Creature> {
						new Creature(){PositionInGroup = new Vector3Int(0, 0, 0), CreatureId = "sprite_sheet_angel_0_16x16", AvailableSpells = { "asd" } },
						new Creature(){PositionInGroup = new Vector3Int(0, -2, 0), CreatureId = "sprite_sheet_monk_0_16x16", AvailableSpells = { "asd" }},
						new Creature(){PositionInGroup = new Vector3Int(2, -1, 0), CreatureId = "sprite_sheet_paladin_0_16x16", AvailableSpells = { "asd","asd" }},
						new Creature(){PositionInGroup = new Vector3Int(1, 0, 0), CreatureId = "sprite_sheet_pikeman_0_16x16", AvailableSpells = { "asd" } },
						new Creature(){PositionInGroup = new Vector3Int(1, -2, 0), CreatureId = "sprite_sheet_swordsman_0_16x16", AvailableSpells = { "asd" } },
					},
				Gold = 1000
			};
		}

	}
}
