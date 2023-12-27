using System;

namespace Assets.Scripts
{
	public static class CreatureEventHub
	{
		public static Action<Creature> OnCreatureDied { get; set; }
		public static Action<Creature> OnCreatureDisappeared { get; set; }
		public static Action<Creature, SpellData> OnCreatureCastedSpell { get; set; }
		public static Action<Creature, SpellData> OnCreatureLandedSpell { get; set; }
	}
}
