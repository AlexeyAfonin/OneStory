using DialogueSystem.Structures;

namespace DialogueSystem.Interfaces
{
    internal interface IDialogue
    {
        public string Speaker { get; set; }
        public DialogueReplicaStructure[] Replicas { get; set; }
    }
}
