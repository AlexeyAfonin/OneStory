using QuestSystem;
using QuestSystem.SO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static OneStory.Core.Utils.Enums;

[RequireComponent(typeof(EnemyVision), typeof(NavMeshAgent))]
public sealed class EnemyController : CharacterController
{
    private bool _isCountedInQuest = true;
    private bool _isWait = false;

    private NavMeshAgent _agent;
    private Transform _moveTarget;
    private PlayerController _player;

    public Transform MoveTarget
    {
        get => _moveTarget;
        set => _moveTarget = value;
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _speed;
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        if (!IsWaitingEndAnimation && !_isWait)
        {
            Move();
        }
    }

    protected override void Move()
    {
        IsMoving = _moveTarget != null;

        if (IsMoving)
        {
            _characterAnimator.PlayAnimation(CharacterAnimations.Walk);
            _agent.SetDestination(_moveTarget.position);
            audioSourceController.PlaySound(TypeSoundEffect.Walk, true);
        }
        else
        {
            _characterAnimator.PlayAnimation(CharacterAnimations.Idle);
            _agent.SetDestination(transform.position);
            audioSourceController.StopSound(TypeSoundEffect.Walk);
        }
    }

    protected override void Attack()
    {
        if (!IsWaitingEndAnimation && !_isWait)
        {
            _characterAnimator.PlayAnimation(CharacterAnimations.Attack, true);
            StartCoroutine(IEAttack());
        }
    }

    public override IEnumerator IEAttack()
    {
        yield return new WaitForSecondsRealtime(0.5f);

        if(_player != null && !IsDead && !_isWait && !IsWaitingEndAnimation)
        {
            audioSourceController.PlaySound(TypeSoundEffect.Attack);
            _player.TakeDamage(_damage);
        }

        StartCoroutine(IEWait(1.2f));
    }

    protected override void Dead()
    {
        _characterAnimator.PlayAnimation(CharacterAnimations.Dying);
        _agent = null;
        if (_isCountedInQuest)
        {
            CountInQuest(QuestsSystemController.Instance.ActiveQuest);
        }
        GetComponent<CapsuleCollider>().enabled = false;
        base.Dead();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        _characterAnimator.PlayAnimation(CharacterAnimations.Hit);
        audioSourceController.PlaySound(TypeSoundEffect.Hit);

        StartCoroutine(IEWait(1.7f));
    }
    
    private IEnumerator IEWait(float seconds)
    {
        _isWait = true;
        yield return new WaitForSecondsRealtime(seconds);
        _isWait = false;
    }

    private void CountInQuest(QuestSO quest)
    {
        if (quest != null)
        {
            QuestsSystemController.Instance.UpdateProgressQuest(quest, 1);
            _isCountedInQuest = false;
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out var player))
        {
            _player = player;
        }
    }

    protected override void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out var player))
        {
            if (!IsAttacking)
            {
                Attack();
            }
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out var player))
        {
            _player = null;
            _characterAnimator.PlayAnimation(CharacterAnimations.Idle);
        }
    }
}
