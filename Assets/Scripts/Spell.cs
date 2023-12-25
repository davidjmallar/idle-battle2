using System;

namespace Assets.Scripts
{
	public class Spell
	{
		public SpellData SpellData { get; set; }
		public string SpellId;

		public DateTime LastTimeTicked;
		public DateTime TimeUsed;
		public void Feed(float deltaT)
		{

		}

	}
}
