using TMPro;
using UnityEngine;

public class CreatureController : MonoBehaviour
{
	public CreatureAnimationController AnimationController;
	public TextMeshProUGUI HealthText;
	public Creature Creature { get; private set; }
	public void Setup(Creature creature, bool isEnemy = false)
	{
		Creature = creature;
		AnimationController.SpriteRenderer.transform.localScale = new Vector3(isEnemy ? -1 : 1, 1, 1);
		AnimationController.Setup(creature);
	}

	public void Feed(float deltaT)
	{
		AnimationController.Feed(deltaT);
		Creature.Feed(deltaT);
		HealthText.text = $"{Creature.Health}";
	}
}
