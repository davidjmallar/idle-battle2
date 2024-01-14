using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
	public static class DataService
	{

		private static SaveData _saveData = new SaveData()
		{
			Creatures = new List<Creature> {
						new Creature(){PositionInGroup = new Vector3Int(0, 0, 0), CreatureId = "angel", AvailableSpells = new List<CreatureSpell>() { GetDummySpell(0),GetDummySpell(2),GetDummySpell(3), }, IsHero = true },
						new Creature(){PositionInGroup = new Vector3Int(0, -2, 0), CreatureId = "monk", AvailableSpells = new List<CreatureSpell>() { GetDummySpell(1),GetDummySpell(5), }, IsHero = true},
						//new Creature(){PositionInGroup = new Vector3Int(2, -1, 0), CreatureId = "paladin", AvailableSpells = { "asd","asd" }, IsHero = true},
						//new Creature(){PositionInGroup = new Vector3Int(1, 0, 0), CreatureId = "pikeman", AvailableSpells = { "asd" }, IsHero = true },
						//new Creature(){PositionInGroup = new Vector3Int(1, -2, 0), CreatureId = "swordsman", AvailableSpells = { "asd" }, IsHero = true },
					},
			Gold = 1000
		};

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
				SpellId = spellId,
				Periodicity = 1.0f,
				SpellIconId = spellId,
			};
		}

		public static SaveData SaveData
		{
			get => _saveData;
			set => _saveData = value;
		}

		public static CreatureSpell GetDummySpell(int i)
		{
			return new CreatureSpell()
			{
				IsAvailable = true,
				IsSelected = true,
				Order = i,
				SpellId = $"{i}",
			};
		}

	}
}
