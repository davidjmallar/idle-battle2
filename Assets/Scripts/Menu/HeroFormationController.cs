using Assets.Scripts;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HeroFormationController : MonoBehaviour
{

	[Required, FoldoutGroup("Templates"), AssetsOnly]
	public HeroAvatarInFormationController AvatarTemplate;

	[Required]
	public Button AddButtonTemplate;


	private SaveData Data => DataService.GetSaveData();

	[Required]
	public Transform DropSlotParent;
	private List<DragAndDropSlot> DropSlots;

	private void OnEnable()
	{
		TryPopulate();
	}

	private void OnDisable()
	{
        Debug.Log("OnDisable");
	}
	private void OnDestroy()
	{
        Debug.Log("OnDestroy");
	}

	private void TryPopulate()
	{
		if (DropSlots == null || DropSlots.Count == 0)
		{
			DropSlots = DropSlotParent.GetComponentsInChildren<DragAndDropSlot>().ToList();
			//DropSlots.ForEach(s=>s.OnDropLanded);
		}
	}

	//private void DropLanded

}
