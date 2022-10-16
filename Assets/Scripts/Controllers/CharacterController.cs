using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OneStory.Interfaces;
using OneStory.Configs;
using OneStory.Core.Utils;
using static OneStory.Core.Utils.Enums;

[RequireComponent(typeof(CharacterAnimator))]
public class CharacterController : MonoBehaviour, ICharacter
{
    [Header("Config")]
    [SerializeField] private CharacterConfig config;
    [Space(10f)]
    [Header("Character parameters")]
    [SerializeField] private CharacterType type;
    [SerializeField] private new string name;
    [SerializeField] private int health;
    [SerializeField] private int damage;
    [SerializeField] private float speed;
    [Space(10f)]
    [Header("Animator")]
    [SerializeField] protected Animator animator;
    [Header("Radiuses")]
    [SerializeField] protected Collider interactRadius;

    protected CharacterAnimator _characterAnimator;

    public CharacterType Type => type;
    public string Name => name;
    public int Health => health;
    public int Damage => damage;
    public float Speed => speed;

    public bool IsDead { get; protected set; }
    public bool IsWaitAnim { get; set; }

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
        type = config.Type;
        name = name.IsNullOrEmpty() ? config.Name : name;
        health = health.IsNull() ? config.Health : health;
        damage = damage.IsNull() ? config.Damage : damage;
        speed = speed.IsNull() ? config.Speed : speed;
    }

    protected virtual void Attack() { }

    protected virtual void Move() { }

    protected virtual void Rotate() { }

    protected virtual void Rotate(Vector3 direction) { }

    protected virtual void Dead() 
    {
        enabled = false;
    }

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        IsDead = health <= 0;
    }
}
