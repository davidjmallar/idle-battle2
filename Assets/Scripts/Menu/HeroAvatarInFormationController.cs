using System.Collections.Generic;
using UnityEngine.UI;

public class HeroAvatarInFormationController : GameListElement<Creature>
{
	public Image AvatarImage;

	public override void Setup(Creature data)
	{
		AvatarImage.sprite = SpriteManager.Instance.GetAnimationFrame(data.Data.SpriteId, 0);
	}
}
