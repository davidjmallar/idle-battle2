using UnityEngine;

namespace Assets.Scripts
{
	public class CreatureController : MonoBehaviour
	{
		public CreatureAnimationController AnimationController;
		public Creature Creature { get; private set; }
		public void Setup(Creature creature, bool isEnemy = false)
		{
			Creature = creature;
			transform.localScale = new Vector3(isEnemy ? -1 : 1, 1, 1);
			AnimationController.Setup(creature);
		}

		public void Feed(float deltaT)
		{
			AnimationController.Feed(deltaT);
			Creature.Feed(deltaT);
		}
	}
}
