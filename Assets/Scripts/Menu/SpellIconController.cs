using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpellIconController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{

	[Required] public Transform SelectionTransform;
	[Required] public Transform AvailibityTransform;
	[Required] public Image SpellIcon;

	private Creature _creature;
	private CreatureSpell _spell;

	public void Setup(Creature creature, CreatureSpell creatureSpell)
	{
		_creature = creature;
		_spell = creatureSpell;
		SpellIcon.sprite = SpriteManager.Instance.GetSpellImage(creatureSpell.SpellId);
	}

	public void UpdateModel()
	{
		AvailibityTransform.gameObject.SetActive(!_spell.IsAvailable);
		SelectionTransform.gameObject.SetActive(!_spell.IsSelected);
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		_spell.IsSelected = !_spell.IsSelected;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		throw new System.NotImplementedException();
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		throw new System.NotImplementedException();
	}
}
