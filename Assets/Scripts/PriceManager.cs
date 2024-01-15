using System;

public static class PriceManager
{
	public static double GetStatPrice(int level)
	{
		if (level <= 1) return 50;
		return Math.Ceiling(GetStatPrice(level - 1) * GlobalConstants.StatLevelPriceMultiplier);
	}
}
