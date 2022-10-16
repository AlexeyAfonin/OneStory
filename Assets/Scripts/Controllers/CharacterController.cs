using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OneStory.Interfaces;
using OneStory.Configs;
using OneStory.Core.Utils;
using static OneStory.Core.Utils.Enums;

public class CharacterController : MonoBehaviour, ICharacter
{
    [SerializeField] private CharacterConfig config;
    [Space(20)]
    [SerializeField] private CharacterType type;
    [SerializeField] private new string name;
    [SerializeField] private int health;
    [SerializeField] private int damage;
    [SerializeField] private float speed;

    public CharacterType Type => type;
    public string Name => name;
    public int Health => health;
    public int Damage => damage;
    public float Speed => speed;

    public bool IsDead { get; protected set; }

    protected virtual void Awake()
    {
        Initialize();
    }

    protected virtual void Start() { }

    protected virtual void Update() { }

    protected virtual void FixedUpdate() { }

    protected virtual void Initialize()
    {
        type = config.Type;
        name = name.IsNullOrEmpty() ? config.Name : name;
        health = health.IsNull() ? config.Health : health;
        damage = damage.IsNull() ? config.Damage : damage;
        speed = speed.IsNull() ? config.Speed : speed;
    }

    protected virtual void Move() { }

    protected virtual void Rotate() { }

    protected virtual void Dead() { }

    public virtual void TakeDamage(int damage) 
    {
        health -= damage;
        IsDead = health <= 0;
    }
}
