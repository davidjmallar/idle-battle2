using Unity.VisualScripting;

public static class CreatureStatExtensions
{
	public static AggregatedStats Clone(this AggregatedStats stats)
	{
		return new AggregatedStats()
		{
			AgilityLevel = stats.AgilityLevel,
			FocusLevel = stats.FocusLevel,
			MaxHealth = stats.MaxHealth,
			MightLevel = stats.MightLevel,
			SpeedMultiplier = stats.SpeedMultiplier,
			ThreatMultiplier = stats.ThreatMultiplier,
		};
	}

	public static AggregatedStats AddBuff(this AggregatedStats stats, Buff buff)
	{
		var ret = stats.Clone();
		ret.MightLevel += buff.MightLevel;
		ret.AgilityLevel += buff.AgilityLevel;
		ret.FocusLevel += buff.FocusLevel;
		return ret;
	}
	public static AggregatedStats MultiplyBuff(this AggregatedStats stats, Buff buff)
	{
		var ret = stats.Clone();
		ret.MightLevel *= (int)((100 + buff.MightLevelPercent) / 100f);
		ret.AgilityLevel *= (int)((100 + buff.AgilityLevelPercent) / 100f);
		ret.FocusLevel *= (int)((100 + buff.FocusLevelPercent) / 100f);
		return ret;
	}
	public static void DoAggregation(this Creature creature)
	{
		creature.AggregatedStats = new AggregatedStats();
		creature.AggregatedStats.AddBuff(creature.Data.CharacterBonus);
		creature.AggregatedStats.MultiplyBuff(creature.Data.CharacterBonus);
		// TODO calculate running buffs, gear etc...
	}
}
