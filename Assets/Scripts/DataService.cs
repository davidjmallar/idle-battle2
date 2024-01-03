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
						new Creature(){PositionInGroup = new Vector3Int(0, 0, 0), CreatureId = "angel", AvailableSpells = { "asd" } },
						new Creature(){PositionInGroup = new Vector3Int(0, -2, 0), CreatureId = "monk", AvailableSpells = { "asd" }},
						new Creature(){PositionInGroup = new Vector3Int(2, -1, 0), CreatureId = "paladin", AvailableSpells = { "asd","asd" }},
						new Creature(){PositionInGroup = new Vector3Int(1, 0, 0), CreatureId = "pikeman", AvailableSpells = { "asd" } },
						new Creature(){PositionInGroup = new Vector3Int(1, -2, 0), CreatureId = "swordsman", AvailableSpells = { "asd" } },
					},
				Gold = 1000
			};
		}

	}
}
