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
    [SerializeField] private Transform moveTarget;

    private NavMeshAgent _agent;

    private bool _isMoving;
    private bool _isCountedInQuest = true;

    private PlayerController _player;

    public PlayerController Player => _player;

    public bool IsCanAttack { get; set; } = true;
    public bool IsCountedInQuest => _isCountedInQuest;

    public Transform MoveTarget
    {
        get => moveTarget;
        set => moveTarget = value;
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
        if (!IsWaitAnim)
        {
            Move();
        }
    }

    protected override void Move()
    {
        _isMoving = moveTarget != null;

        if (_isMoving)
        {
            _characterAnimator.PlayAnimation(CharacterAnimations.Walk);
            _agent.SetDestination(moveTarget.position);
        }
        else
        {
            _characterAnimator.PlayAnimation(CharacterAnimations.Idle);
            _agent.SetDestination(transform.position);
        }
    }

    protected override void Attack()
    {
        if (!IsWaitAnim)
        {
            _characterAnimator.PlayAnimation(CharacterAnimations.Attack, true);
        }
    }

    public IEnumerator IAttackPlayer()
    {
        yield return new WaitForSecondsRealtime(0.6f);

        if(_player != null && !IsDead)
        {
            _player.TakeDamage(_damage);
        }
    }

    protected override void Dead()
    {
        _characterAnimator.PlayAnimation(CharacterAnimations.Dying);
        _agent = null;
        if (IsCountedInQuest)
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
            if (IsCanAttack)
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
