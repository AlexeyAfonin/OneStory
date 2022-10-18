using QuestSystem.SO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static DialogueSystem.Core.DialogueEnums;

namespace DialogueSystem.SO
{
    [CreateAssetMenu(fileName = "DialogueContainer", menuName = "DialogSystem/DialogueContainer", order = 3)]
    public sealed class DialogueContainerSO : ScriptableObject
    {
        [SerializeField] private string nameContainer;
        [SerializeField] private DialogueGroupSO[] dialoguesGroups;
        [SerializeField] private bool isQuestDialogue;
        [SerializeField] private QuestSO quest;
        
        /// <summary>
        /// Название диалога
        /// </summary>
        public string NameContainer => nameContainer;

        /// <summary>
        /// Части диалога
        /// </summary>
        public DialogueGroupSO[] DialoguesGroups => dialoguesGroups;

        /// <summary>
        /// Если true - то в процессе диалога, будет выдан квест
        /// </summary>
        public bool IsQuestDialogue => isQuestDialogue;

        /// <summary>
        /// Квест, который назначается игроку, в процессе диалога
        /// </summary>
        public QuestSO Quest => quest;

        /// <summary>
        /// Возврщает группу диалога (progress-part) по состоянию прогресса
        /// </summary>
        /// <param name="type">Состояние прогресса</param>
        /// <returns>Группа диалога данной части</returns>
        public DialogueGroupSO GetGroup(StatusDialogueGroup type) =>
            dialoguesGroups.FirstOrDefault(g => g.StatusGroup.Equals(type));
    }
}
