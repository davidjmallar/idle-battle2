using Assets.Scripts;
using Sirenix.OdinInspector;
using System;
using System.Diagnostics;
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

	private Creature _creature;

	public void SelectHero(Creature creature)
	{
		_creature = creature;
		Avatar.sprite = SpriteManager.Instance.GetAvatar32(creature.Data.SpriteId);
		SpellSlotsParent.ClearChildren();
		for (int i = 0; i < creature.AvailableSpells.Count; i++)
		{
			var spell = Instantiate(SpellSlot, SpellSlotsParent);
			spell.Setup(creature, creature.AvailableSpells[i]);
		}
		SetupButtons();
	}
	public void Update()
	{
		if (_creature == null) return;
		var mgtPrice = PriceManager.GetStatPrice(_creature.MightLevel);
		var agiPrice = PriceManager.GetStatPrice(_creature.AgilityLevel);
		var focPrice = PriceManager.GetStatPrice(_creature.FocusLevel);
		MgtLearnButton.interactable = DataService.SaveData.Gold >= mgtPrice;
		MgtLearnButtonText.text = $"{mgtPrice}";
		AgiLearnButtonText.text = $"{agiPrice}";
		FocLearnButtonText.text = $"{focPrice}";

		// Use nullable datetimes and if it reaches below utcnow then add the bonus and null it!

		if (_creature.MightLevelLearning > DateTime.UtcNow)
		{
			LearningTransform.gameObject.SetActive(true);
			LearningText.text = $"Learning Might {_creature.MightLevel}\n{(_creature.MightLevelLearning - DateTime.UtcNow).ToCompactTime()}";
		}

		else if (_creature.AgilityLevelLearning > DateTime.UtcNow)
		{
			LearningTransform.gameObject.SetActive(true);
			LearningText.text = $"Learning Agility {_creature.AgilityLevel}\n{(_creature.AgilityLevelLearning - DateTime.UtcNow).ToCompactTime()}";
		}

		else if (_creature.FocusLevelLearning > DateTime.UtcNow)
		{
			LearningTransform.gameObject.SetActive(true);
			LearningText.text = $"Learning Focus {_creature.FocusLevel}\n{(_creature.FocusLevelLearning - DateTime.UtcNow).ToCompactTime()}";
		}
		else
		{
			LearningTransform.gameObject.SetActive(false);
		}
	}

	public void SetupButtons()
	{
		MgtLearnButton.onClick.RemoveAllListeners();
		MgtLearnButton.onClick.AddListener(() =>
		{
			var price = PriceManager.GetStatPrice(_creature.MightLevel);
			if (DataService.SaveData.Gold >= price)
			{
				DataService.SaveData.Gold -= price;
				_creature.MightLevel++;
				_creature.MightLevelLearning = DateTime.UtcNow.AddSeconds(GlobalConstants.StatLearningTimeSeconds);
			}
		});
		AgiLearnButton.onClick.RemoveAllListeners();
		AgiLearnButton.onClick.AddListener(() =>
		{
			var price = PriceManager.GetStatPrice(_creature.AgilityLevel);
			if (DataService.SaveData.Gold >= price)
			{
				DataService.SaveData.Gold -= price;
				_creature.AgilityLevel++;
				_creature.AgilityLevelLearning = DateTime.UtcNow.AddSeconds(GlobalConstants.StatLearningTimeSeconds);
			}
		});
		FocLearnButton.onClick.RemoveAllListeners();
		FocLearnButton.onClick.AddListener(() =>
		{
			var price = PriceManager.GetStatPrice(_creature.FocusLevel);
			if (DataService.SaveData.Gold >= price)
			{
				DataService.SaveData.Gold -= price;
				_creature.FocusLevel++;
				_creature.FocusLevelLearning = DateTime.UtcNow.AddSeconds(GlobalConstants.StatLearningTimeSeconds);
			}
		});

	}

}
