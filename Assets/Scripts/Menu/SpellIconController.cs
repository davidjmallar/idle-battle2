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

		if (creatureSpell != null)
			SpellIcon.sprite = SpriteManager.Instance.GetSpellImage(creatureSpell.SpellId);
	}
	[Button]
	public void EnableSelection()
	{
		SelectionTransform.gameObject.SetActive(true);
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
		SelectionTransform.gameObject.SetActive(_spell?.IsSelected ?? false);
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (_spell != null)
		{
			_spell.IsSelected = !_spell.IsSelected;
			_creature.SetSpells();
		}
		Debug.Log("Spell Clicked");
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
