﻿using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpellIconController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
	public static Action<SpellIconController> OnIconClicked;
	[Required] public Transform SelectionTransform;
	[Required] public Transform ActivatedTransform;
	[Required] public Transform AvailibityTransform;
	[Required] public Image SpellIcon;

	private Creature _creature;
	private CreatureSpell _spell;
	private Action<CreatureSpell> _onSelected;

	public void Setup(Creature creature, CreatureSpell creatureSpell, Action<CreatureSpell> onSelected)
	{
		_onSelected = onSelected;
		_creature = creature;
		_spell = creatureSpell;
		OnIconClicked += OnClickedPropagationCallback;

		if (creatureSpell != null)
			SpellIcon.sprite = SpriteManager.Instance.GetSpellImage(creatureSpell.SpellId);
	}
	private void OnDestroy()
	{
		OnIconClicked -= OnClickedPropagationCallback;
	}
	private void OnClickedPropagationCallback(SpellIconController controller)
	{
		SelectionTransform.gameObject.SetActive(controller == this);
	}

	[Button]
	public void ActivateSelection()
	{
		ActivatedTransform.gameObject.SetActive(true);
	}
	[Button]
	public void EnableLock()
	{
		AvailibityTransform.gameObject.SetActive(true);
	}
	private void Update()
	{
		UpdateModel();
	}

	public void UpdateModel()
	{
		AvailibityTransform.gameObject.SetActive(!(_spell?.IsAvailable ?? false));
		ActivatedTransform.gameObject.SetActive(_spell?.IsActivated ?? false);
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (_spell != null)
		{
			_onSelected?.Invoke(_spell);
			OnIconClicked?.Invoke(this);
			//_spell.IsActivated = !_spell.IsActivated;
			_creature.SetSpells();
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		//throw new System.NotImplementedException();
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		//throw new System.NotImplementedException();
	}
}
