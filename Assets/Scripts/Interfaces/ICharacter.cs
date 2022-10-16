using static OneStory.Core.Utils.Enums;

namespace OneStory.Interfaces
{ 
    public interface ICharacter
    {
        public CharacterType Type { get; }
        public string Name { get; }
        public int Health { get; }
        public int Damage { get; }
        public float Speed { get; }
    }
}