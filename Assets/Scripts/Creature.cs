using Assets.Scripts;
using Sirenix.Serialization;
using System.Collections.Generic;
using UnityEngine;

public class Creature
{
	[OdinSerialize] public string CreatureId { get; set; }
	[OdinSerialize] public int Level { get; set; }
	[OdinSerialize] public Vector3Int PositionInGroup { get; set; }
	[OdinSerialize] public int Strength { get; set; }
	[OdinSerialize] public int Agility { get; set; }
	[OdinSerialize] public int Speed { get; set; }
	[OdinSerialize] public List<string> AvailableSpells { get; set; }
	public CreatureData Data => DataService.GetCreature(CreatureId);
	public Vector3Int PositionInMap { get; set; }
	public bool InTheTeam { get; set; }
	public float Threat { get; set; }
	public Creature Target { get; set; }

	public List<SpellData> SpellDatas { get; set; }

	public Creature() { }

	public void Feed(float deltaT)
	{

	}

}

public class CreatureSpell
{
	public string SpellId { get; set; }
	public int MinumumLevel { get; set; }
}

public class SaveData
{
	public List<Creature> Creatures { get; set; }
	public double Gold;
}
