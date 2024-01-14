using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroDetailsController : MonoBehaviour
{
	[FoldoutGroup("Templates"), Required] public SpellIconController SpellSlot;
	[Required] public Image Avatar;
	[Required] public Transform SpellSlotsParent;

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
