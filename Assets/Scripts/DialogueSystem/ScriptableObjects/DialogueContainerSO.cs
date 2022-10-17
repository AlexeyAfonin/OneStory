using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static DialogueSystem.Core.DialogueEnums;

namespace DialogueSystem.SO
{
    [CreateAssetMenu(fileName = "DialogueContainer", menuName = "DialogSystem/DialogueContainer", order = 3)]
    public class DialogueContainerSO : ScriptableObject
    {
        [SerializeField] private string nameContainer;
        [SerializeField] private DialogueGroupSO[] dialoguesGroups;
        [SerializeField] private bool isQuestDialogue;
        

        public string NameContainer => nameContainer;
        public DialogueGroupSO[] DialoguesGroups => dialoguesGroups;
        public bool IsQuestDialogue => isQuestDialogue;

        public DialogueGroupSO GetGroup(StateDialogueGroup type) =>
            dialoguesGroups.FirstOrDefault(g => g.TypeGroup.Equals(type));
    }
}
