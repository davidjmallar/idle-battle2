using Sirenix.OdinInspector;
using System;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
	[Required]
	public CreatureSpawner CreatureSpawner;

	public bool IsBattlePaused { get; set; } = true;

	public int ProgressionIndex { get; set; } = 0;

	public Action OnProgression { get; set; }

	private void Start()
	{
		for (int i = 0; i < GlobalConstants.MapWidth + 2; i++)
		{
			TileSpawnerManager.Instance.Progress();
		}
	}
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			OnProgression?.Invoke();
		}
		CreatureSpawner.FeedCreatures(Time.deltaTime);
	}

}
