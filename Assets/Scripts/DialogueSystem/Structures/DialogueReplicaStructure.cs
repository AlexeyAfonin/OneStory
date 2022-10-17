using QuestSystem.SO;
using UnityEngine;
using static QuestSystem.Base.QuestEnums;

namespace DialogueSystem.Structures
{
    [System.Serializable]
    public struct DialogueReplicaStructure
    {
        [TextArea(1, 10)] 
        public string Text;
        public QuestSO Quest;
        public State StateQuest;
        public string TriggerAnimation;
    }
}
