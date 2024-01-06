using UnityEngine;
using UnityEngine.EventSystems;

public class HeroSlot : DragAndDropSlot, IDropHandler
{
	public Vector3Int Position;

	//public Creature Hero => Item.GetComponent<HeroAvatarInFormationController>().Creature; // TODO

	public void OnDrop(PointerEventData eventData)
	{
		SnapOnDrop(eventData);
		//Hero.PositionInGroup = Position; // TODO
	}
}