﻿using UnityEngine;
using UnityEngine.EventSystems;

public abstract class DragAndDropSlot : MonoBehaviour
{
	[HideInInspector] public DragableItem Item { get; set; }

	public void SnapOnDrop(PointerEventData eventData)
	{
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
