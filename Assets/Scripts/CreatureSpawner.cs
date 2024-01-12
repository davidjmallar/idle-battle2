using Assets.Scripts;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class CreatureSpawner : MonoBehaviour
{
	public static CreatureSpawner Instance { get; private set; }
	private void Awake() => Instance = this;

	public CreatureController CreatureAnimationControllerTemplate;
	public Transform CreatureParent;

	private List<CreatureController> Heroes => CreatureMap.Heroes;
	private List<CreatureController> Foes => CreatureMap.Foes;
	private int _index = 6;

	[Button]
	public void SpawnDummyHeroTeam()
	{
		SpawnHeroTeam(DataService.SaveData.Creatures);
	}
	[Button]
	public void SpawnDummyEnemy()
	{
		SpawnEnemy(_index++);
		SpawnEnemy(_index++);
		SpawnEnemy(_index++);
	}
	[Button]
	public void ProceedHeroTeam()
	{
		Debug.Log("Proceed hero team");
		Heroes.ForEach(h => h.AnimationController.Proceed());
	}
	public void SpawnEnemy(int index)
	{
		var enemy = SpawnCreature(1, index);
		Foes.Add(SpawnCreature(enemy, enemy.PositionInMap, isEnemy: true));
	}

	public void SpawnHeroTeam(List<Creature> Creatures)
	{
		_index = 6;
		CreatureMap.ResetMap();
		foreach (var creature in Creatures)
		{
			Heroes.Add(SpawnCreature(creature, creature.PositionInGroup, isEnemy: false));
			creature.State = AnimationState.Idle;
			creature.SetSpells();
		}
	}

	private CreatureController SpawnCreature(Creature creature, Vector3Int position, bool isEnemy)
	{
		var instance = Instantiate(CreatureAnimationControllerTemplate, CreatureParent);
		creature.PositionInMap = position;
		creature.Health = creature.AggregatedStats.MaxHealth;
		instance.transform.localPosition = position;
		instance.Setup(creature, isEnemy);
		return instance;
	}
	public Creature SpawnCreature(int level, int index)
	{
		var enemy = new Creature();
		enemy.PositionInMap = new Vector3Int(index, Random.Range(0, -3), 0);
		enemy.CreatureId = "pikeman";
		enemy.AvailableSpells = new List<CreatureSpell>() { new() { SpellId = "asd" } };
		enemy.IsHero = false;
		enemy.SetSpells();
		return enemy;
	}
}
