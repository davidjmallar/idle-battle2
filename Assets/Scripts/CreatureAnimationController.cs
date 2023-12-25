using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.U2D;

namespace Assets.Scripts
{
	public class CreatureAnimationController : MonoBehaviour
	{
		public enum AnimationState
		{
			Idle = 0,
			Walk = 4,
			Attack = 8,
			Dying = 16,
		}

		private Creature _creature;

		[ReadOnly]
		public AnimationState State;
		public SpriteRenderer SpriteRenderer;
		public SpriteAtlas CreatureAtlas;
		public float BaseAnimationSpeed = 0.2f;
		[ReadOnly]
		public float CurrentAnimationSpeed = 0.2f;

		private float _tNextFrame = 0.2f;
		private float _tNextPeriodicAttack = 0f;
		private float _periodicAttack = 0f;

		private int _frameIndex = 0;
		private bool _isRunning = true;

		private void Awake()
		{
			SpriteRenderer.enabled = false;
		}

		public void Setup(Creature creature)
		{
			_creature = creature;
			SpriteRenderer.enabled = true;
			SpriteRenderer.sprite = CreatureAtlas.GetSprite($"{_creature.Data.SpriteId}_0");
		}

		[Button]
		public void Attack()
		{
			State = AnimationState.Attack;
			CurrentAnimationSpeed = BaseAnimationSpeed * 0.5f;
			_frameIndex = 0;
			_tNextFrame = 0;
		}

		[Button] public void StartTestPeriodicAttack() => SetPeriodicAttack(new SpellData() { Periodicity = 1f });
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
		public void Die()
		{
			State = AnimationState.Dying;
			_frameIndex = 0;
			_tNextFrame = 0;
		}

		[Button]
		public void StartWalk()
		{
			State = AnimationState.Walk;
			_frameIndex = 0;
			_tNextFrame = 0;
		}

		[Button]
		public void StopWalk()
		{
			State = AnimationState.Idle;
			_frameIndex = 0;
			_tNextFrame = 0;
		}

		public void EndOfAnimationCycle()
		{
			if (State == AnimationState.Dying)
			{
				_isRunning = false;
			}
			else if (State == AnimationState.Walk)
			{
				_frameIndex = 0;
			}
			else
			{
				State = AnimationState.Idle;
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
				SpriteRenderer.sprite = CreatureAtlas.GetSprite($"{_creature.Data.SpriteId}_{(int)State + _frameIndex++}");
				if (State == AnimationState.Attack && _frameIndex == 2)
				{
					// Attack?
				}
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
		}
	}
}
