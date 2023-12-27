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
			targets.ForEach(t => t.AttackThis(creature, 2));
		}

		private List<Creature> GetTarget(Creature creature)
		{
			return CreatureMap.Foes.Select(c => c.Creature).ToList();
		}
	}
}
