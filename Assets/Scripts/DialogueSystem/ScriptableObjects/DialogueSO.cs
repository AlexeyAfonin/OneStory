using DialogueSystem.Interfaces;
using DialogueSystem.Structures;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem.SO
{
    [CreateAssetMenu(fileName = "Dialogue", menuName = "OneStory/DialogSystem/Dialogue", order = 1)]
    public class DialogueSO : ScriptableObject, IDialogue
    {
        [SerializeField] private string speaker;
        [SerializeField] private Replica[] replicas;

        public string Speaker { get => speaker; set => speaker = value; }
        public Replica[] Replicas { get => replicas; set => replicas = value; }
    }
}
