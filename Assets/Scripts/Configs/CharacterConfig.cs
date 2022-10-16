using OneStory.Interfaces;
using UnityEngine;
using static OneStory.Core.Utils.Enums;

namespace OneStory.Configs
{
    [CreateAssetMenu(fileName = "CharacterConfig", menuName = "OneStory/Configs/Character config", order = 1)]
    public sealed class CharacterConfig : ScriptableObject, ICharacter
    {
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
    }
}
