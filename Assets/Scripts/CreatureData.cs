using System.Collections.Generic;

public class CreatureData
{
	public string DisplayName;
	public string SpriteId;
	public List<CreatureSpells> Spells;
	public Buff CharacterBonus;
}

public class CreatureSpells
{
	public string SpellId;
	public int LevelRequirement;
	//public int Price; // No price, since price will be determined by the level requirement
}
