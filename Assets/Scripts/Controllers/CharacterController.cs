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
    [SerializeField] protected Collider interactRadius;
    [Header("Points")]
    [SerializeField] protected Transform dialogueCameraViewPosition;

    protected CharacterType _type;
    protected CharacterState _state;
    protected string _name;
    protected int _health;
    protected int _damage;
    protected float _speed;

    protected CharacterAnimator _characterAnimator;

    public CharacterType Type => _type;
    public CharacterState State => _state;
    public string Name => _name;
    public int Health => _health;
    public int Damage => _damage;
    public float Speed => _speed;

    public bool IsDead { get; protected set; }
    public bool IsWaitAnim { get; set; }
    public bool IsCanInteract { get; set; }

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
    }

    protected virtual void Attack() { }

    protected virtual void Move() { }

    protected virtual void Rotate() { }

    protected virtual void Rotate(Vector3 direction) { }

    protected virtual void Dead() 
    {
        enabled = false;
        _state = CharacterState.Dead;
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
