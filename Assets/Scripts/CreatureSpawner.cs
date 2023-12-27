using Assets.Scripts;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CreatureSpawner : MonoBehaviour
{
	public CreatureController CreatureAnimationControllerTemplate;
	public Transform CreatureParent;

	private List<CreatureController> Heroes => CreatureMap.Heroes;
	private List<CreatureController> Foes => CreatureMap.Foes;
	private int _index = 6;
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
	[Button]
	public void ProceedHeroTeam()
	{
		Heroes.ForEach(h => h.AnimationController.Proceed());
	}
	public bool CanProceed()
	{
		return !Foes.Any() || (Heroes.All(c => c.AnimationController.State != CreatureAnimationController.AnimationState.Walk) && Heroes.All(c => (c.Creature.PositionInMap.x + 1) < Foes.Min(f => f.Creature.PositionInMap.x)));
	}
	public void SpawnEnemy(int index)
	{
		var enemy = SpawnCreature(1, index);
		Foes.Add(SpawnCreature(enemy, enemy.PositionInMap, isEnemy: true));
	}

	public void SpawnHeroTeam(List<Creature> Creatures)
	{
		foreach (var creature in Creatures)
		{
			Heroes.Add(SpawnCreature(creature, creature.PositionInGroup, isEnemy: false));
			creature.SetSpells();
		}
	}

	private CreatureController SpawnCreature(Creature creature, Vector3Int position, bool isEnemy)
	{
		var instance = Instantiate(CreatureAnimationControllerTemplate, CreatureParent);
		creature.PositionInMap = position;
		instance.transform.localPosition = position;
		instance.Setup(creature, isEnemy);
		return instance;
	}
	public void FeedCreatures(float deltaT)
	{
		Heroes.ForEach(h => h.Feed(deltaT));
		Foes.ForEach(h => h.Feed(deltaT));
	}
	public Creature SpawnCreature(int level, int index)
	{
		var enemy = new Creature();
		enemy.PositionInMap = new Vector3Int(index, Random.Range(0, -3), 0);
		enemy.CreatureId = "sprite_sheet_pikeman_0_16x16";
		return enemy;
	}
}
