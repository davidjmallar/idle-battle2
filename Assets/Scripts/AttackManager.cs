using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
	public class AttackManager : MonoBehaviour
	{
		public void Start()
		{
			CreatureEventHub.OnCreatureLandedSpell += OnCreatureLandedSpell;
		}

		private void OnCreatureLandedSpell(Creature creature, SpellData data)
		{
			var targets = GetTarget(creature);
			var closestTargets = GetClosestTarget(targets);
			closestTargets.ForEach(t => t.AttackThis(creature, data));
		}

		public static bool HasTarget(Creature creature)
		{
			return GetTarget(creature)?.Count > 0;
		}

		private static List<Creature> GetTarget(Creature creature)
		{
			if (creature == null) return new List<Creature>();
			if (creature.IsHero) // This is the attacker
				return CreatureMap.Foes.Where(c => c.Creature.Health > 0).Select(c => c.Creature).ToList();
			return CreatureMap.Heroes.Where(c => c.Creature.Health > 0).Select(c => c.Creature).ToList();
		}

		private static List<Creature> GetClosestTarget(List<Creature> creature)
		{
			if (!creature.Any()) return new List<Creature>();
			if (!creature.First().IsHero) // this is target, not the attacker
				return creature.Where(c => c.PositionInMap.x == creature.Min(t => t.PositionInMap.x)).ToList();
			return creature.Where(c => c.PositionInMap.x == creature.Max(t => t.PositionInMap.x)).ToList();
		}


	}
}
