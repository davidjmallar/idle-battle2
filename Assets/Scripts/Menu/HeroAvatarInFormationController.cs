using UnityEngine.UI;

public class HeroAvatarInFormationController : DragableItem
{
	public Image AvatarImage;
	public Creature Creature { get; private set; }

	public void Setup(Creature data, ToggleGroup toggleGroup)
	{
		Creature = data;
		var toggle = GetComponent<Toggle>();
		toggle.group = toggleGroup;
		toggle.onValueChanged.AddListener((s) =>
		{
			if (s)
			{
				HeroMenuController.Instance.DetailsController.SelectHero(data);
			}
		});

		var s = SpriteManager.Instance.GetAnimationFrame(data.Data.SpriteId, 0);
		if (s != null)
		{

			AvatarImage.sprite = s;
		}
	}
}
