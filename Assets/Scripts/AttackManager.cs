using System;
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
			var closestTargets = targets.Where(c => c.PositionInMap.x == targets.Min(t => t.PositionInMap.x)).ToList();
			closestTargets.ForEach(t => t.AttackThis(creature, data));
		}
		
		public static bool HasTarget(Creature creature)
		{
			return GetTarget(creature)?.Count > 0;
		}

		private static List<Creature> GetTarget(Creature creature)
		{
			return CreatureMap.Foes.Where(c => c.Creature.Health > 0).Select(c => c.Creature).ToList();
		}
	}
}
