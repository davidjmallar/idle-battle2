using Assets.Scripts;
using Sirenix.Serialization;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Creature
{
	[OdinSerialize] public string CreatureId { get; set; }
	[OdinSerialize] public int Level { get; set; }
	[OdinSerialize] public Vector3Int PositionInGroup { get; set; }
	[OdinSerialize] public int Strength { get; set; }
	[OdinSerialize] public int Agility { get; set; }
	[OdinSerialize] public int Speed { get; set; }
	[OdinSerialize]

	public List<string> AvailableSpells { get; set; } = new List<string>();
	public AnimationState State;

	public CreatureData Data => DataService.GetCreature(CreatureId);
	public Vector3Int PositionInMap { get; set; }
	public bool InTheTeam { get; set; }
	public float Threat { get; set; }
	public Creature Target { get; set; }
	public double Health { get; set; }
	public double MaxHealth => 5;

	public List<PeriodicAttack> PeriodicAttacks { get; } = new List<PeriodicAttack>();

	public List<SpellData> SpellDatas => AvailableSpells.Select(s => DataService.GetSpell(s)).ToList();

	public Creature()
	{
		Health = MaxHealth;
	}

	public void SetSpells()
	{
		PeriodicAttacks.Clear();
		PeriodicAttacks.AddRange(SpellDatas.Select(s => new PeriodicAttack() { NextTimeToHit = s.Periodicity, SpellData = s }));
	}

	public void Feed(float deltaT)
	{
		if (AttackManager.HasTarget(this) && State != AnimationState.Walk && State != AnimationState.Dying)
			foreach (PeriodicAttack attack in PeriodicAttacks)
			{
				attack.NextTimeToHit -= deltaT;
				if (attack.NextTimeToHit < 0)
				{
					CreatureEventHub.OnCreatureCastedSpell?.Invoke(this, attack.SpellData);
					attack.NextTimeToHit = attack.SpellData.Periodicity;
				}
			}
	}

	public void AttackThis(Creature attacker, int attackDamage)
	{
		Health -= attackDamage;
		if (Health <= 0)
		{
			Health = 0;
			CreatureEventHub.OnCreatureDied?.Invoke(this);
		}
	}

}

public class PeriodicAttack
{
	public SpellData SpellData;
	public float NextTimeToHit;
	public bool HasAnimation = true;
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
