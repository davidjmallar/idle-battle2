using UnityEngine;
using UnityEngine.EventSystems;

public class HeroSlot : DragAndDropSlot, IDropHandler
{
	public Vector3Int Position;

	public HeroAvatarInFormationController HeroController => Item.GetComponent<HeroAvatarInFormationController>(); // TODO

	public void OnDrop(PointerEventData eventData)
	{
		SnapOnDrop(eventData);
		HeroController.Creature.PositionInGroup = Position; // TODO
		BattleManager.Instance.Start();

		// Must start manually or delayed? Not this shit again.
		//CreatureSpawner.Instance.ProceedHeroTeam();
	}
}