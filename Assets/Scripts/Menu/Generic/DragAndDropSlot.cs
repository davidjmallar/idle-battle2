using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDropSlot : MonoBehaviour, IDropHandler
{
	public DragableItem Item { get; set; }

	public void OnDrop(PointerEventData eventData)
	{
		Debug.Log("Drop");
		if (transform.childCount == 0)
		{
			var dropped = eventData.pointerDrag;
			var dragableItem = dropped.GetComponent<DragableItem>();
			dragableItem.ParentAfterDrag = transform;
			if(dragableItem.Slot != null) dragableItem.Slot.Item = null;
			dragableItem.Slot = this;
			Item = dragableItem;
		}
	}
}
