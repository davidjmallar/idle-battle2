using Assets.Scripts;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HeroFormationController : MonoBehaviour
{

	[Required, FoldoutGroup("Templates"), AssetsOnly]
	public HeroAvatarInFormationController AvatarTemplate;

	[Required]
	public Button AddButtonTemplate;


	private SaveData Data => DataService.SaveData;

	[Required]
	public Transform DropSlotParent;
	private List<HeroSlot> DropSlots;

	private void OnEnable()
	{
		TryPopulate();
	}


	private void OnDisable()
	{
	}
	private void OnDestroy()
	{
	}

	private void TryPopulate()
	{
		if (DropSlots == null || DropSlots.Count == 0)
		{
			DropSlots = DropSlotParent.GetComponentsInChildren<HeroSlot>().ToList();
			Data.Creatures.ForEach(c => {
				var slotToPutHero = DropSlots.FirstOrDefault(s => s.Position == c.PositionInGroup);
				var heroItem = Instantiate(AvatarTemplate, slotToPutHero.transform);
				heroItem.Setup(c);
			});
		}
	}
}
