using Assets.Scripts;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class CreatureSpawner : MonoBehaviour
{
	public CreatureController CreatureAnimationControllerTemplate;
	public Transform CreatureParent;

	private readonly List<CreatureController> _heroTeam = new List<CreatureController>();
	private readonly List<CreatureController> _foeTeam = new List<CreatureController>();

	[Button]
	public void SpawnDummyHeroTeam()
	{
		SpawnHeroTeam(DataService.GetSaveData().Creatures);
	}
	public void SpawnEnemy(int index)
	{
		var enemy = new Creature();
		
		_foeTeam.Add(SpawnCreature(enemy, enemy.PositionInGroup, isEnemy: true));

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

}
