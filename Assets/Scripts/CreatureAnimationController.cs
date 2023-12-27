using MoreMountains.Feedbacks;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public enum AnimationState
{
	Idle = 0,
	Walk = 4,
	Attack = 8,
	Dying = 16
}
public class CreatureAnimationController : MonoBehaviour
{

	private Creature _creature;

	[Required] public MMF_Player ProgressionFeedback;

	[Required] public SpriteRenderer SpriteRenderer;
	[Required] public SpriteAtlas CreatureAtlas;
	public float BaseAnimationSpeed = 0.2f;

	[ReadOnly] public float CurrentAnimationSpeed = 0.2f;

	private float _tNextFrame = 0.2f;
	private float _tAttackSpeedMultiplier = 0.5f;
	private float _frameToLandAttack = 3f;
	private float _tNextPeriodicAttack = 0f;
	private float _periodicAttack = 0f;

	private int _frameIndex = 0;
	private bool _isRunning = true;
	private List<AttackCast> _attackCasts = new List<AttackCast>();

	private void Awake()
	{
		SpriteRenderer.enabled = false;
	}

	private void OnDestroy()
	{
		CreatureEventHub.OnCreatureDied -= OnCreatureDie;
		CreatureEventHub.OnCreatureCastedSpell -= OnCreatureCastedSpell;
	}

	public void Setup(Creature creature)
	{
		_creature = creature;
		CreatureEventHub.OnCreatureDied += OnCreatureDie;
		CreatureEventHub.OnCreatureCastedSpell += OnCreatureCastedSpell;
		SpriteRenderer.enabled = true;
		SpriteRenderer.sprite = CreatureAtlas.GetSprite($"{_creature.Data.SpriteId}_0");
		ProgressionFeedback.Events.OnComplete.AddListener(() => StopWalk());
	}

	[Button]
	public void Attack()
	{
		_creature.State = AnimationState.Attack;
		CurrentAnimationSpeed = BaseAnimationSpeed * _tAttackSpeedMultiplier;
		_frameIndex = 0;
		_tNextFrame = 0;
	}

	[Button]
	public void StartTestPeriodicAttack()
	{
		SetPeriodicAttack(new SpellData() { Periodicity = 1f });
	}

	[Button]
	public void StopPeriodicAttack()
	{
		_periodicAttack = 0;
		_tNextPeriodicAttack = 0;
	}

	public void SetPeriodicAttack(SpellData spell)
	{
		_periodicAttack = spell.Periodicity;
		_tNextPeriodicAttack = _periodicAttack;
	}

	[Button]
	public void Proceed()
	{
		StartWalk();
		var positionFeedback = ProgressionFeedback.GetFeedbackOfType<MMF_Position>();
		positionFeedback.DestinationPosition = transform.localPosition + Vector3.right;
		_creature.PositionInMap = new Vector3Int(_creature.PositionInMap.x + 1, _creature.PositionInMap.y, 0);
		ProgressionFeedback.PlayFeedbacks();
	}


	[Button]
	public void Die()
	{
		_creature.State = AnimationState.Dying;
		StartCoroutine(Disapper());
		_frameIndex = 0;
		_tNextFrame = 0;
	}

	[Button]
	public void StartWalk()
	{
		_creature.State = AnimationState.Walk;
		CurrentAnimationSpeed = BaseAnimationSpeed * 0.5f;
		_frameIndex = 0;
		_tNextFrame = 0;
	}

	[Button]
	public void StopWalk()
	{
		_creature.State = AnimationState.Idle;
		_frameIndex = 0;
		_tNextFrame = 0;
	}

	public void EndOfAnimationCycle()
	{
		if (_creature.State == AnimationState.Dying)
		{
			_isRunning = false;
		}
		else if (_creature.State == AnimationState.Walk)
		{
			_frameIndex = 0;
		}
		else
		{
			_creature.State = AnimationState.Idle;
			_frameIndex = 0;
			CurrentAnimationSpeed = BaseAnimationSpeed;
		}
	}

	public void Feed(float deltaT)
	{
		if (!_isRunning) return;

		_tNextFrame -= deltaT;
		if (_tNextFrame <= 0)
		{
			_tNextFrame = CurrentAnimationSpeed;
			SpriteRenderer.sprite = CreatureAtlas.GetSprite($"{_creature.Data.SpriteId}_{(int)_creature.State + _frameIndex++}");
			if (_frameIndex > 3)
			{
				EndOfAnimationCycle();
			}
		}
		if (_periodicAttack > 0)
		{
			_tNextPeriodicAttack -= deltaT;
			if (_tNextPeriodicAttack <= 0)
			{
				Attack();
				_tNextPeriodicAttack = _periodicAttack;
			}
		}

		if (_creature.State == AnimationState.Idle)
			foreach (var cast in _attackCasts)
			{
				cast.TimeToCast -= deltaT;
				if (cast.TimeToCast <= 0)
				{
					CreatureEventHub.OnCreatureLandedSpell?.Invoke(_creature, cast.Spell);
				}
			}
		_attackCasts.RemoveAll(c => c.TimeToCast <= 0);
	}

	private void OnCreatureDie(Creature creature)
	{
		if (creature != _creature) return;
		Die();
	}
	private void OnCreatureCastedSpell(Creature creature, SpellData spell)
	{
		if (creature != _creature) return;
		_attackCasts.Add(new AttackCast(spell, BaseAnimationSpeed * _tAttackSpeedMultiplier * _frameToLandAttack));
		Attack();
	}
	IEnumerator Disapper()
	{
		yield return new WaitForSeconds(2f);
		CreatureMap.Heroes.RemoveAll(c => c.Creature == _creature);
		CreatureMap.Foes.RemoveAll(c => c.Creature == _creature);
		Destroy(gameObject);
		//CreatureEventHub.OnCreatureDisappeared?.Invoke(_creature);
	}
}

public class AttackCast
{
	public AttackCast(SpellData spell, float timeToCast)
	{
		Spell = spell;
		TimeToCast = timeToCast;
	}

	public SpellData Spell { get; private set; }
	public float TimeToCast { get; set; }

}
