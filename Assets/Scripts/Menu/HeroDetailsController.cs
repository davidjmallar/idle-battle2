using Assets.Scripts;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.UI;

public class HeroDetailsController : MonoBehaviour
{
	[FoldoutGroup("Templates"), Required] public SpellIconController SpellSlot;
	[Required] public Image Avatar;

	[Required] public Transform SpellSlotsParent;

	[Required] public Text MgtText;
	[Required] public Text AgiText;
	[Required] public Text FocText;

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

	[Required] public Button SpellGenericButton;
	[Required] public Text SpellGenericButtonText;
	[Required] public Text SpellGenericGoldText;
	[Required] public Image SpellGenericGoldIcon;
	[Required] public Text SpellDescription;
	[Required] public Text SpellTitle;

	private Creature _creature;

	public void SelectHero(Creature creature)
	{
		_creature = creature;
		Avatar.sprite = SpriteManager.Instance.GetAvatar32(creature.Data.SpriteId);
		SpellSlotsParent.ClearChildren();
		for (int i = 0; i < creature.AvailableSpells.Count; i++)
		{
			var spell = Instantiate(SpellSlot, SpellSlotsParent);
			spell.Setup(creature, creature.AvailableSpells[i], selectedSpell =>
			{
				SpellTitle.text = selectedSpell.Data.DisplayName;

				SpellGenericButton.onClick.RemoveAllListeners();
				if (selectedSpell.IsAvailable)
				{
					SpellGenericGoldIcon.gameObject.SetActive(false);
					SpellGenericGoldText.gameObject.SetActive(false);
					SpellGenericButtonText.gameObject.SetActive(true);
					SpellGenericButtonText.text = selectedSpell.IsActivated ? "Deactivate" : "Activate";
					SpellGenericButton.onClick.AddListener(() =>
					{
						selectedSpell.IsActivated = !selectedSpell.IsActivated;
						SpellGenericButtonText.text = selectedSpell.IsActivated ? "Deactivate" : "Activate";
					});
				}
				else
				{
					SpellGenericGoldIcon.gameObject.SetActive(true);
					SpellGenericGoldText.gameObject.SetActive(true);
					SpellGenericButtonText.gameObject.SetActive(false);
					SpellGenericGoldText.text = "100";
					SpellGenericButton.onClick.AddListener(() =>
					{
						selectedSpell.IsAvailable = true;
						SpellGenericGoldIcon.gameObject.SetActive(false);
						SpellGenericGoldText.gameObject.SetActive(false);
						SpellGenericButtonText.gameObject.SetActive(true);
						SpellGenericButtonText.text = selectedSpell.IsActivated ? "Deactivate" : "Activate";
					});
				}
			});
		}
		SetupButtons();
	}
	public void Update()
	{
		float feed = Time.deltaTime;
		if (_creature == null) return;
		var mgtPrice = PriceManager.GetStatPrice(_creature.MightLevel);
		var agiPrice = PriceManager.GetStatPrice(_creature.AgilityLevel);
		var focPrice = PriceManager.GetStatPrice(_creature.FocusLevel);
		MgtLearnButton.interactable = DataService.SaveData.Gold >= mgtPrice;
		MgtLearnButtonText.text = $"{mgtPrice}";
		AgiLearnButtonText.text = $"{agiPrice}";
		FocLearnButtonText.text = $"{focPrice}";
		MgtText.text = $"MGT: {_creature.AggregatedStats.MightLevel}";
		AgiText.text = $"AGI: {_creature.AggregatedStats.AgilityLevel}";
		FocText.text = $"FOC: {_creature.AggregatedStats.FocusLevel}";

		// Use nullable datetimes and if it reaches below utcnow then add the bonus and null it!

		if (_creature.MightLevelLearning != null && _creature.MightLevelLearning > DateTime.UtcNow)
		{
			LearningTransform.gameObject.SetActive(true);
			LearningText.text = $"Learning Might {_creature.MightLevel + 1}\n{(_creature.MightLevelLearning.Value - DateTime.UtcNow).ToCompactTime()}";
		}
		else if (_creature.MightLevelLearning != null && _creature.MightLevelLearning < DateTime.UtcNow)
		{
			LearningTransform.gameObject.SetActive(false);
			_creature.MightLevelLearning = null;
			_creature.MightLevel++;
			_creature.DoAggregation();
		}
		else if (_creature.AgilityLevelLearning != null && _creature.AgilityLevelLearning > DateTime.UtcNow)
		{
			LearningTransform.gameObject.SetActive(true);
			LearningText.text = $"Learning Agility {_creature.AgilityLevel + 1}\n{(_creature.AgilityLevelLearning.Value - DateTime.UtcNow).ToCompactTime()}";
		}
		else if (_creature.AgilityLevelLearning != null && _creature.AgilityLevelLearning < DateTime.UtcNow)
		{
			LearningTransform.gameObject.SetActive(false);
			_creature.AgilityLevelLearning = null;
			_creature.AgilityLevel++;
			_creature.DoAggregation();
		}
		else if (_creature.FocusLevelLearning != null && _creature.FocusLevelLearning > DateTime.UtcNow)
		{
			LearningTransform.gameObject.SetActive(true);
			LearningText.text = $"Learning Focus {_creature.FocusLevel + 1}\n{(_creature.FocusLevelLearning.Value - DateTime.UtcNow).ToCompactTime()}";
		}
		else if (_creature.FocusLevelLearning != null && _creature.FocusLevelLearning < DateTime.UtcNow)
		{
			LearningTransform.gameObject.SetActive(false);
			_creature.FocusLevelLearning = null;
			_creature.FocusLevel++;
			_creature.DoAggregation();
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
			var price = PriceManager.GetStatPrice(_creature.Level);
			if (DataService.SaveData.Gold >= price)
			{
				DataService.SaveData.Gold -= price;
				_creature.Level++;
				_creature.MightLevelLearning = DateTime.UtcNow.AddSeconds(GlobalConstants.StatLearningTimeSeconds);
			}
		});
		AgiLearnButton.onClick.RemoveAllListeners();
		AgiLearnButton.onClick.AddListener(() =>
		{
			var price = PriceManager.GetStatPrice(_creature.Level);
			if (DataService.SaveData.Gold >= price)
			{
				DataService.SaveData.Gold -= price;
				_creature.Level++;
				_creature.AgilityLevelLearning = DateTime.UtcNow.AddSeconds(GlobalConstants.StatLearningTimeSeconds);
			}
		});
		FocLearnButton.onClick.RemoveAllListeners();
		FocLearnButton.onClick.AddListener(() =>
		{
			var price = PriceManager.GetStatPrice(_creature.Level);
			if (DataService.SaveData.Gold >= price)
			{
				DataService.SaveData.Gold -= price;
				_creature.Level++;
				_creature.FocusLevelLearning = DateTime.UtcNow.AddSeconds(GlobalConstants.StatLearningTimeSeconds);
			}
		});
	}
}
