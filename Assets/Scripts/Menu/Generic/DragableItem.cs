using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public Transform ParentAfterDrag { get; set; }
	public DragAndDropSlot Slot { get; set; }
	public RectTransform RectTransform;

	public void Start()
	{
		RectTransform = GetComponent<RectTransform>();
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		ParentAfterDrag = transform.parent;
		transform.SetParent(transform.root);
		transform.SetAsLastSibling();
		SetAllImageRaycast(false);
	}

	public void OnDrag(PointerEventData eventData)
	{
		//transform.position = Input.touches.Length > 0 ? (Vector3)Input.touches[0].position : Input.mousePosition;
		//transform.position = Input.mousePosition;
		RectTransform.anchoredPosition += eventData.delta;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		transform.SetParent(ParentAfterDrag);
		SetAllImageRaycast(true);
	}
	public void SetAllImageRaycast(bool state)
	{
		transform.GetComponents<Image>().ForEach(x => x.raycastTarget = state);
		transform.GetComponentsInChildren<Image>().ForEach(x => x.raycastTarget = state);
	}


}
