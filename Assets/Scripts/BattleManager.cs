using Sirenix.OdinInspector;
using System;
using System.Linq;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
	public static BattleManager Instance { get; private set; }
	private void Awake() => Instance = this;

	private bool skipFrame = true;

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
		if (skipFrame)
		{
			skipFrame = false;
			return;
		}
		if (Input.GetKeyDown(KeyCode.Space))
		{
			OnProgression?.Invoke();
		}
		if (IsHeroesDead())
		{
			Start();
			return;
		}
		if (CanProceed())
		{
			Proceed();
		}
		FeedCreatures(Time.deltaTime);
	}

	public void FeedCreatures(float deltaT)
	{
		CreatureMap.Heroes.ForEach(h => h.Feed(deltaT));
		CreatureMap.Foes.ForEach(h => h.Feed(deltaT));
	}

	public bool CanProceed() => CreatureMap.Foes.Any(c => c.Creature.State != AnimationState.Dying) && (CreatureMap.Heroes.All(c => c.Creature.State != AnimationState.Walk) && CreatureMap.Heroes.All(c => (c.Creature.PositionInMap.x + 1) < CreatureMap.Foes.Where(c => c.Creature.State != AnimationState.Dying).Min(f => f.Creature.PositionInMap.x)));

	public bool IsHeroesDead() => CreatureMap.Heroes.All(c => c.Creature.Health <= 0);

}
