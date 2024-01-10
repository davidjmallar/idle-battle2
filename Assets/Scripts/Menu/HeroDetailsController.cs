using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class HeroDetailsController : MonoBehaviour
{
	[Required] public Image Avatar;
	public void SelectHero(Creature creature)
	{
		Avatar.sprite = SpriteManager.Instance.GetAvatar32(creature.Data.SpriteId);
	}
}
