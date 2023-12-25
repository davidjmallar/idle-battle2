using Assets.Scripts;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CreatureSpawner : MonoBehaviour
{
	public CreatureController CreatureAnimationControllerTemplate;
	public Transform CreatureParent;

	private readonly List<CreatureController> _heroTeam = new List<CreatureController>();
	private readonly List<CreatureController> _foeTeam = new List<CreatureController>();
	private int _index = 3;
	[Button]
	public void SpawnDummyHeroTeam()
	{
		SpawnHeroTeam(DataService.GetSaveData().Creatures);
	}
	[Button]
	public void SpawnDummyEnemy()
	{
		SpawnEnemy(_index++);
	}
	public bool CanProceed(int index)
	{
		return _foeTeam.All(f => f.Creature.PositionInMap.x > index);
	}
	public void SpawnEnemy(int index)
	{
		var enemy = SpawnCreature(1, index);
		_foeTeam.Add(SpawnCreature(enemy, enemy.PositionInMap, isEnemy: true));
	}

	public void SpawnHeroTeam(List<Creature> Creatures)
	{
		foreach (var creature in Creatures)
		{
			_heroTeam.Add(SpawnCreature(creature, creature.PositionInGroup, isEnemy: false));
		}
	}

	private CreatureController SpawnCreature(Creature creature, Vector3Int position, bool isEnemy)
	{
		var instance = Instantiate(CreatureAnimationControllerTemplate, CreatureParent);
		instance.transform.localPosition = position;
		instance.Setup(creature, isEnemy);
		return instance;
	}
	public void FeedCreatures(float deltaT)
	{
		_heroTeam.ForEach(h => h.Feed(deltaT));
		_foeTeam.ForEach(h => h.Feed(deltaT));
	}
	public Creature SpawnCreature(int level, int index)
	{
		var enemy = new Creature();
		enemy.PositionInMap = new Vector3Int(index, Random.Range(0, -3), 0);
		enemy.CreatureId = "sprite_sheet_pikeman_0_16x16";
		return enemy;
	}
}
