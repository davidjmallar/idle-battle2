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
	[OdinSerialize] public int StrengthLevel { get; set; }
	[OdinSerialize] public int AgilityLevel { get; set; }
	[OdinSerialize] public int SpeedLevel { get; set; }
	[OdinSerialize] public List<string> AvailableSpells { get; set; } = new List<string>();

	public AnimationState State;
	public CreatureData Data => DataService.GetCreature(CreatureId);
	public Vector3Int PositionInMap { get; set; }
	public float Threat { get; set; }
	public double Health { get; set; }
	public AggregatedStats AggregatedStats { get; set; } = new AggregatedStats();
	public List<PeriodicAttack> PeriodicAttacks { get; } = new List<PeriodicAttack>();
	public List<SpellData> SpellDatas => AvailableSpells.ConvertAll(s => DataService.GetSpell(s));
	public bool IsHero { get; set; }

	public Creature()
	{
		Health = AggregatedStats.MaxHealth;
	}

	public void SetSpells()
	{
		PeriodicAttacks.Clear();
		PeriodicAttacks.AddRange(SpellDatas.Select(s => new PeriodicAttack() { NextTimeToHit = Random.Range(0f, s.Periodicity), SpellData = s }));
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

	public void AttackThis(Creature attacker, SpellData attackingSpell)
	{
		Health -= 2;
		CreatureEventHub.OnCreatureHurt?.Invoke(this, 2 + "");
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

public class SaveData
{
	public List<Creature> Creatures { get; set; }
	public List<Creature> CreaturePool { get; set; }
	public double Gold;
}

public class AggregatedStats
{
	public double MaxHealth { get; set; } = 15;
	public double SpeedMultiplier { get; set; }
	public double ThreatMultiplier { get; set; }
	public int StrengthLevel { get; set; }
	public int AgilityLevel { get; set; }
	public int SpeedLevel { get; set; }
}

public class Buff
{
	public double MaxHealth { get; set; }
	public double MaxHealthPercent { get; set; }
	public double SpeedMultiplier { get; set; }
	public double SpeedMultiplierPercent { get; set; }
	public double ThreatMultiplier { get; set; }
	public double ThreatMultiplierPercent { get; set; }
	public int StrengthLevel { get; set; }
	public int StrengthLevelPercent { get; set; }
	public int AgilityLevel { get; set; }
	public int AgilityLevelPercent { get; set; }
	public int SpeedLevel { get; set; }
	public int SpeedLevelPercent { get; set; }
}
