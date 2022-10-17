namespace QuestSystem.Interfaces
{
    public interface IQuest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int AmountTarget { get; set; }
    }
}
