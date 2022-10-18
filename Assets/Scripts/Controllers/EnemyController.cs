using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static OneStory.Core.Utils.Enums;

[RequireComponent(typeof(NavMeshAgent))]
public sealed class EnemyController : CharacterController
{
    private NavMeshAgent _agent;

    private bool _isMoving;

    private PlayerController _player;

    public PlayerController Player => _player;

    public bool IsCanAttack { get; set; } = true;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        _agent = GetComponent<NavMeshAgent>();
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
        /*_isMoving = true;

        if (_isMoving)
        {
            _characterAnimator.PlayAnimation(CharacterAnimations.Walk);
        }
        else
        {
            _characterAnimator.PlayAnimation(CharacterAnimations.Idle);
        }*/
    }

    protected override void Attack()
    {
        _characterAnimator.PlayAnimation(CharacterAnimations.Attack, true);
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
        GetComponent<CapsuleCollider>().enabled = false;
        base.Dead();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        _characterAnimator.PlayAnimation(CharacterAnimations.Hit);
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
