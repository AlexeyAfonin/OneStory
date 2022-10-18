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
        /// �������� �������
        /// </summary>
        public string NameContainer => nameContainer;

        /// <summary>
        /// ����� �������
        /// </summary>
        public DialogueGroupSO[] DialoguesGroups => dialoguesGroups;

        /// <summary>
        /// ���� true - �� � �������� �������, ����� ����� �����
        /// </summary>
        public bool IsQuestDialogue => isQuestDialogue;

        /// <summary>
        /// �����, ������� ����������� ������, � �������� �������
        /// </summary>
        public QuestSO Quest => quest;

        /// <summary>
        /// ��������� ������ ������� (progress-part) �� ��������� ���������
        /// </summary>
        /// <param name="type">��������� ���������</param>
        /// <returns>������ ������� ������ �����</returns>
        public DialogueGroupSO GetGroup(StatusDialogueGroup type) =>
            dialoguesGroups.FirstOrDefault(g => g.StatusGroup.Equals(type));
    }
}
