using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static DialogueSystem.Core.DialogueEnums;

namespace DialogueSystem.SO
{
    [CreateAssetMenu(fileName = "DialogueGroup", menuName = "OneStory/DialogSystem/DialogueGroup", order = 2)]
    public class DialogueGroupSO : ScriptableObject
    {
        [SerializeField] private StatusDialogueGroup statusGroup;
        [SerializeField] private DialogueSO[] dialoguesSO;

        public StatusDialogueGroup StatusGroup => statusGroup;
        public DialogueSO[] Dialogues => dialoguesSO;

        public DialogueSO GetDialogue(string speaker) =>
            dialoguesSO.FirstOrDefault(n => n.Speaker == speaker);

        public DialogueSO GetDialogue(int index) =>
            dialoguesSO[index];

        public List<string> GetSpeakers()
        {
            List<string> speakers = new List<string>();

            foreach (DialogueSO dialogue in Dialogues)
            {
                speakers.Add(dialogue.Speaker);
            }

            return speakers;
        }
    }
}
