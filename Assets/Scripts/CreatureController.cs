using UnityEngine;

namespace Assets.Scripts
{
	public class CreatureController : MonoBehaviour
	{
		public CreatureAnimationController AnimationController;
		private Creature _creature;
		public void Setup(Creature creature, bool isEnemy = false)
		{
			_creature = creature;
			transform.localScale = new Vector3(isEnemy ? -1 : 1, 1, 1);
			AnimationController.Setup(creature);
		}

		public void Feed(float deltaT)
		{
			AnimationController.Feed(deltaT);
			_creature.Feed(deltaT);
		}
	}
}
