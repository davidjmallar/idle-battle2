using Sirenix.OdinInspector;
using System;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
	[Required]
	public CreatureSpawner CreatureSpawner;
	[Required]
	public TileSpawnerManager TileSpawnerManager;
	[Required]
	public CameraController CameraController;

	public bool IsBattlePaused { get; set; } = true;

	public int ProgressionIndex { get; set; } = 0;

	public Action OnProgression { get; set; }

	[Button]
	public void Proceed()
	{
		CreatureSpawner.ProceedHeroTeam();
		CameraController.Proceed();
		TileSpawnerManager.Instance.Progress();
	}

	private void Start()
	{
		for (int i = 0; i < GlobalConstants.MapWidth + 2; i++)
		{
			TileSpawnerManager.Instance.Progress();
		}
		CreatureSpawner.SpawnDummyHeroTeam();
		CreatureSpawner.SpawnDummyEnemy();

	}
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			OnProgression?.Invoke();
		}
		if (CreatureSpawner.CanProceed())
		{
			Proceed();
		}
		CreatureSpawner.FeedCreatures(Time.deltaTime);
	}

}
