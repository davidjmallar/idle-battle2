using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class HeroDetailsController : MonoBehaviour
{
	[FoldoutGroup("Templates"), Required] public SpellIconController SpellSlot;
	[Required] public Image Avatar;

	[Required] public Transform SpellSlotsParent;

	[Required] public Button MgtLearnButton;
	[Required] public Text MgtLearnButtonText;
	[Required] public Button AgiLearnButton;
	[Required] public Text AgiLearnButtonText;
	[Required] public Button FocLearnButton;
	[Required] public Text FocLearnButtonText;

	[Required] public Transform LearningTransform;
	[Required] public Text LearningText;
	[Required] public Text QuickLearnButtonText;
	[Required] public Button QuickLearnButton;

	public void SelectHero(Creature creature)
	{
		Avatar.sprite = SpriteManager.Instance.GetAvatar32(creature.Data.SpriteId);
		SpellSlotsParent.ClearChildren();
		for (int i = 0; i < creature.AvailableSpells.Count; i++)
		{
			var spell = Instantiate(SpellSlot, SpellSlotsParent);
			spell.Setup(creature, creature.AvailableSpells[i]);
		}
	}
}
