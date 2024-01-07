using Sirenix.OdinInspector;
using System;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
	public static BattleManager Instance { get; private set; }
	private void Awake() => Instance = this;

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
	public void Start()
	{
		TileSpawnerManager.Restart();
		for (int i = 0; i < GlobalConstants.MapWidth + 2; i++)
		{
			TileSpawnerManager.Instance.Progress();
		}
		CameraController.ResetCamera();
		CreatureSpawner.SpawnDummyHeroTeam();
		CreatureSpawner.SpawnDummyEnemy();
		//CreatureSpawner.ProceedHeroTeam();
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
