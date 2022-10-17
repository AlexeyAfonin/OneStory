using DialogueSystem.Structures;

namespace DialogueSystem.Interfaces
{
    internal interface IDialogue
    {
        public string Speaker { get; set; }
        public Replica[] Replicas { get; set; }
    }
}
