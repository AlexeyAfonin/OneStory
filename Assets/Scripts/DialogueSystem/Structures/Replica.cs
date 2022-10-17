using OneStory.Configs;
using UnityEngine;

namespace DialogueSystem.Structures
{
    [System.Serializable]
    public struct Replica
    {
        [TextArea(1, 10)] 
        public string Text;
        public bool IsLast;
    }
}
