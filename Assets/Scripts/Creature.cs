using Assets.Scripts;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Creature
{
	[OdinSerialize] public string CreatureId { get; set; }
	[OdinSerialize] public int Level { get; set; } = 1;
	[OdinSerialize] public Vector3Int PositionInGroup { get; set; }
	[OdinSerialize] public int MightLevel { get; set; } = 1;
	[OdinSerialize] public DateTime? MightLevelLearning { get; set; }
	[OdinSerialize] public int AgilityLevel { get; set; } = 1;
	[OdinSerialize] public DateTime? AgilityLevelLearning { get; set; }
	[OdinSerialize] public int FocusLevel { get; set; } = 1;
	[OdinSerialize] public DateTime? FocusLevelLearning { get; set; }
	[OdinSerialize] public List<CreatureSpell> AvailableSpells { get; set; } = new List<CreatureSpell>();

	public AnimationState State;
	public CreatureData Data => DataService.GetCreature(CreatureId);
	public Vector3Int PositionInMap { get; set; }
	public float Threat { get; set; }
	public double Health { get; set; }
	public AggregatedStats AggregatedStats { get; set; } = new AggregatedStats();
	public List<PeriodicAttack> PeriodicAttacks { get; } = new List<PeriodicAttack>();
	public bool IsHero { get; set; }

	public Creature()
	{
		Health = AggregatedStats.MaxHealth;
		this.DoAggregation();
	}

	public void SetSpells()
	{
		PeriodicAttacks.Clear();
		var newAttacks = AvailableSpells.Where(s => s.IsSelected).Select(s => new PeriodicAttack() { NextTimeToHit = UnityEngine.Random.Range(0f, s.Data.Periodicity), SpellData = s.Data });
		Debug.Log($"Added {newAttacks.Count()} to the creature {CreatureId}");
		PeriodicAttacks.AddRange(newAttacks);
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
	public double SpeedMultiplier { get; set; } = 1;
	public double ThreatMultiplier { get; set; } = 1;
	public int MightLevel { get; set; }
	public int AgilityLevel { get; set; }
	public int FocusLevel { get; set; }
}

public class Buff
{
	public double MaxHealth { get; set; }
	public double MaxHealthPercent { get; set; }
	public double SpeedMultiplier { get; set; }
	public double SpeedMultiplierPercent { get; set; }
	public double ThreatMultiplier { get; set; }
	public double ThreatMultiplierPercent { get; set; }
	public int MightLevel { get; set; }
	public int MightLevelPercent { get; set; }
	public int AgilityLevel { get; set; }
	public int AgilityLevelPercent { get; set; }
	public int FocusLevel { get; set; }
	public int FocusLevelPercent { get; set; }
}

[System.Serializable]
public class CreatureSpell
{
	[OdinSerialize] public string SpellId { get; set; }
	[OdinSerialize] public bool IsAvailable { get; set; }
	[OdinSerialize] public bool IsSelected { get; set; }
	[OdinSerialize] public int Order { get; set; }
	public SpellData Data => DataService.GetSpell(SpellId);
}
