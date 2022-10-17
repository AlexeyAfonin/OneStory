using DialogueSystem.Interfaces;
using DialogueSystem.Structures;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem.SO
{
    [CreateAssetMenu(fileName = "Dialogue", menuName = "OneStory/DialogSystem/Dialogue", order = 1)]
    public sealed class DialogueSO : ScriptableObject, IDialogue
    {
        [SerializeField] private string speaker;
        [SerializeField] private DialogueReplicaStructure[] replicas;

        public string Speaker { get => speaker; set => speaker = value; }
        public DialogueReplicaStructure[] Replicas { get => replicas; set => replicas = value; }
    }
}
