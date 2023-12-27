using System.Collections.Generic;

namespace Assets.Scripts
{
	public static class CreatureMap
	{
		public static List<CreatureController> Heroes { get; } = new List<CreatureController>();
		public static List<CreatureController> Foes { get; } = new List<CreatureController>();
	}
}
