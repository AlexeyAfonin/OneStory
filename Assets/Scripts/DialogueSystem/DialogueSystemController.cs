using Core.Utils;
using DialogueSystem.Core;
using DialogueSystem.SO;
using DialogueSystem.Structures;
using DialogueSystem.UI;
using QuestSystem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static DialogueSystem.Core.DialogueEnums;
using static QuestSystem.Base.QuestEnums;

namespace DialogueSystem
{
    [AddComponentMenu(menuName: "DialogueSystem/Controller/DialogueSystemContoller", order: 1)]
    public sealed class DialogueSystemController : MonobehSingleton<DialogueSystemController>
    {
        #region Fields
        [SerializeField] private DialogueWindow dialogueWindow;
        [Space(10f)]
        [SerializeField] private List<DialogueStateContainer> dialogues;

        private DialogueStateContainer _currentDialogue;
        private DialogueGroupSO _group;
        private DialogueSO[] _dialogues;
        private DialogueSO _activeDialogue;
        private DialogueReplicaStructure _activeReplica;

        private StatusDialogueGroup _activeStateDialogueGroup = StatusDialogueGroup.None;

        private int _indexActiveSpeaker = 0;
        private int _indexActiveReplica = 0;
        #endregion

        /// <summary>
        /// ��������/���������� �������� ������ �� �������-����������
        /// </summary>
        /// <param name="container">������-���������</param>
        public void Dialogue(DialogueContainerSO container)
        {
            AddDialogue(container);

            _currentDialogue = dialogues.FirstOrDefault(d => d.Container == container);

            switch (_currentDialogue.State)
            {
                case StateDialogue.NotStarted:
                    ShowDialogue(_currentDialogue.State);
                    _currentDialogue.State = StateDialogue.InProgress;
                    break;
                case StateDialogue.InProgress:
                    ShowDialogue(_currentDialogue.State);
                    break;
                case StateDialogue.Completed:
                default:
                    break;
            }
        }

        /// <summary>
        /// ��������� �������� ������ �� �������-���������� (��������� ��� ������ Completed )
        /// </summary>
        /// <param name="dialogue">�������-���������</param>
        public void CompleteDialogue(DialogueContainerSO dialogue)
        {
            _currentDialogue = dialogues.FirstOrDefault(d => d.Container == dialogue);
            _currentDialogue.State = StateDialogue.Completed;
        }

        /// <summary>
        /// ��������� ������ �� �������-���������� �� ������� � ������ �������� �������� �,
        /// ���� ��� ��� ���, ��������� � ����
        /// </summary>
        /// <param name="dialogue">������-���������</param>
        private void AddDialogue(DialogueContainerSO dialogue)
        {
            bool isNewDialogue = dialogues.FindAll(d => d.Container == dialogue).Count() < 1;

            if (isNewDialogue)
            {
                dialogues.Add(new DialogueStateContainer(dialogue, StateDialogue.NotStarted));
            }
        }

        /// <summary>
        /// ���������� ���������� ���� � ��������� ������ ������� �� ���������� ���������
        /// </summary>
        /// <param name="stateDialogue">��������� ������� </param>
        private void ShowDialogue(StateDialogue stateDialogue)
        {
            dialogueWindow.Show();

            if (stateDialogue == StateDialogue.NotStarted)
            {
                UpdateStateDialogueGroup();
            }
            DialogueProcess();
        }


        /// <summary>
        /// ��������� ��������� ��������� ������� (progress-part)
        /// </summary>
        private void UpdateStateDialogueGroup()
        {
            if (_activeStateDialogueGroup != StatusDialogueGroup.Complete)
            {
                _activeStateDialogueGroup += 1;
                _group = _currentDialogue.Container.GetGroup(_activeStateDialogueGroup);
                _dialogues = _group.Dialogues;
            }
            else
            {
                CompleteDialogue(_currentDialogue.Container);
            }
        }

        /// <summary>
        /// ��������� ������� �������
        /// </summary>
        public void DialogueProcess()
        {
            if (_currentDialogue.Container.IsQuestDialogue && 
                (_activeStateDialogueGroup == StatusDialogueGroup.Progress))  //������ ������� �������� ���������� �������
            {
                if (QuestsSystemController.Instance.CheckStateQuest(_activeReplica.Quest) == State.InProcess)
                {
                    if (_indexActiveReplica < _dialogues.Last().Replicas.Length)
                    {
                        if (_indexActiveSpeaker < _dialogues.Length)
                        {
                            UpdateVisualDialogue();
                            _indexActiveSpeaker++;
                        }
                        else
                        {
                            _indexActiveSpeaker = 0;
                            _indexActiveReplica++;
                            DialogueProcess();
                        }
                    }
                    else
                    {
                        _indexActiveReplica = 0;
                        DialogueWindow.Instance.Hide();
                    }
                }
                else if (QuestsSystemController.Instance.CheckStateQuest(_activeReplica.Quest) == State.Completed)
                {
                    UpdateStateDialogueGroup();
                    DialogueProcess();
                }
            } 
            else
            {
                if (_indexActiveReplica < _dialogues.Last().Replicas.Length)
                {
                    if (_indexActiveSpeaker < _dialogues.Length)
                    {
                        UpdateVisualDialogue();
                        _indexActiveSpeaker++;

                        if (_currentDialogue.Container.IsQuestDialogue && 
                            (_activeReplica.Quest != null) && 
                            _activeReplica.StateQuest.Equals(State.Give))
                        {
                            QuestsSystemController.Instance.NewQuest(_activeReplica.Quest);
                        }
                    }
                    else
                    {
                        _indexActiveSpeaker = 0;
                        _indexActiveReplica++;
                        DialogueProcess();
                    }
                }
                else
                {
                    _indexActiveReplica = 0;
                    UpdateStateDialogueGroup();
                    DialogueWindow.Instance.Hide();
                }
            }
        }

        /// <summary>
        /// ��������� ������� � ������� � ���������� ������
        /// </summary>
        private void UpdateVisualDialogue()
        {
            _activeDialogue = _dialogues[_indexActiveSpeaker];
            _activeReplica = _activeDialogue.Replicas[_indexActiveReplica];

            DialogueWindow.Instance.UpdateDialoguePanel(_activeDialogue.Speaker, _activeReplica.Text);
        }
    }
}
