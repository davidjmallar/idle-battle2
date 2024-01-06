using UnityEngine.UI;

public class HeroAvatarInFormationController : DragableItem
{
	public Image AvatarImage;
	public Creature Creature { get; private set; }

	public void Setup(Creature data)
	{
		Creature = data;
		var s = SpriteManager.Instance.GetAnimationFrame(data.Data.SpriteId, 0);
		if (s != null)
		{

			AvatarImage.sprite = s;
		}
	}
}
