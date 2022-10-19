using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OneStory.Interfaces;
using OneStory.Configs;
using OneStory.Core.Utils;
using static OneStory.Core.Utils.Enums;
using DialogueSystem.SO;

[RequireComponent(typeof(CharacterAnimator))]
public class CharacterController : MonoBehaviour, ICharacter
{
    [Header("Configs")]
    [SerializeField] protected CharacterConfig config;
    [SerializeField] protected DialogueContainerSO dialogue;    
    [Header("Animator")]
    [SerializeField] protected Animator animator;
    [Header("Radiuses")]
    [SerializeField] protected TriggerZone triggerZone;

    protected CharacterType _type;
    protected string _name;
    protected int _health;
    protected int _maxHealth;
    protected int _damage;
    protected float _speed;

    protected CharacterAnimator _characterAnimator;

    public CharacterType Type => _type;
    public string Name => _name;
    public int Health => _health;
    public int MaxHealth => _maxHealth;
    public int Damage => _damage;
    public float Speed => _speed;

    public CharacterState State { get; set; }
    public bool IsMoving { get; set; }
    public bool IsDead { get; set; }
    public bool IsAttacking { get; set; }
    public bool IsCanInteract { get; set; }
    public bool IsInteracting { get; set; }
    public bool IsWaitingEndAnimation { get; set; }


    protected virtual void Awake()
    {
        Initialize();
    }

    protected virtual void Start()
    {
        _characterAnimator = GetComponent<CharacterAnimator>();
        _characterAnimator.Init(animator);
    }

    protected virtual void Update() 
    {
        if (IsDead)
        {
            Dead();
        }
    }

    protected virtual void FixedUpdate() { }

    protected virtual void Initialize()
    {
        _type = config.Type;
        _name = config.Name;
        _health = config.Health;
        _damage = config.Damage;
        _speed = config.Speed;
        _maxHealth = _health;
    }

    protected virtual void Attack() { }

    public virtual IEnumerator IEAttack() { return null; }

    protected virtual void Move() { }

    protected virtual void Rotate() { }

    protected virtual void Rotate(Vector3 direction) { }

    protected virtual void Dead() 
    {
        enabled = false;
        State = CharacterState.Dead;
    }

    public virtual void TakeDamage(int damage)
    {
        _health -= damage;
        IsDead = _health <= 0;
    }

    protected virtual void OnTriggerEnter(Collider other) { }

    protected virtual void OnTriggerStay(Collider other) { }

    protected virtual void OnTriggerExit(Collider other) { }
}
