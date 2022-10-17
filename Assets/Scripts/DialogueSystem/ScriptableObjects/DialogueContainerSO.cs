using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static OneStory.DialogueSystem.Core.DialogueEnums;

namespace OneStory.DialogueSystem.SO
{
    [CreateAssetMenu(fileName = "DialogueContainer", menuName = "OneStory/DialogSystem/DialogueContainer", order = 3)]
    public class DialogueContainerSO : ScriptableObject
    {
        [SerializeField] private string nameContainer;
        [SerializeField] private DialogueGroupSO[] dialoguesGroups;

        public string NameContainer => nameContainer;
        public DialogueGroupSO[] DialoguesGroups => dialoguesGroups;

        public DialogueGroupSO GetGroup(TypeDialogueGroup type) =>
            dialoguesGroups.FirstOrDefault(g => g.TypeGroup.Equals(type));
    }
}
