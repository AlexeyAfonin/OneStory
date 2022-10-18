namespace OneStory.Core.Utils
{
    public sealed class Enums
    {
        public enum CharacterType
        {
            Player = 0,
            Enemy = 1,
            NPC = 2,
        }

        public enum CharacterAnimations
        {
            Idle = 0,
            Walk = 1,
            Attack = 2,
            Hit = 3,
            Dying = 4,
        }

        public enum CharacterState
        {
            Free = 0,
            Fights = 1,
            Interacts = 2,
            Dead = 3,
        }

        public enum TypeTriggerZone
        {
            None = 0,
            Interactable = 1,
            Attack = 2,
        }

        public enum TypeEventTriggerZone
        {
            None = 0,
            Enter = 1,
            Stay = 2,
            Exit = 3,
        }
    }
}
