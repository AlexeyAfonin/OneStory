using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DialogueSystem.SO;
using System.Linq;
using static DialogueSystem.Core.DialogueEnums;
using QuestSystem;
using static QuestSystem.Base.QuestEnums;
using DialogueSystem.Structures;
using Core.Base;

namespace DialogueSystem.UI
{
    [AddComponentMenu(menuName: "DialogueSystem/UI/DialogueWindow", order: 1)]
    public sealed class DialogueWindow : BaseWindow<DialogueWindow>
    {
        [Space(10f)]
        [SerializeField] private TextMeshProUGUI header;
        [SerializeField] private TextMeshProUGUI content;

        private StateDialogueGroup _activeStateDialogueGroup = StateDialogueGroup.None;

        private DialogueContainerSO _container;
        private DialogueGroupSO _group;
        private DialogueSO[] _dialogues;
        private DialogueSO _activeSpeaker;
        private Replica _activeReplica;

        private int _indexActiveSpeaker = 0;
        private int _indexActiveReplica = 0;

        public void StartDialogue(DialogueContainerSO container, StateDialogue stateDialogue)
        {
            _container = container;
            if (stateDialogue == StateDialogue.NotStarted)
            {
                UpdateStateDialogueGroup();
            }
            Continue();
        }

        private void UpdateStateDialogueGroup()
        {
            if (_activeStateDialogueGroup != StateDialogueGroup.Complete)
            {
                _activeStateDialogueGroup += 1;
                _group = _container.GetGroup(_activeStateDialogueGroup);
                _dialogues = _group.Dialogues;
            }
            else
            {
                Complete();
            }
        }

        public void Continue()
        {
            if (_container.IsQuestDialogue &&
                    _activeStateDialogueGroup == StateDialogueGroup.Progress)
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
                            Continue();
                        }
                    }
                    else
                    {
                        _indexActiveReplica = 0;
                        Hide();
                    }
                }
                else if (QuestsSystemController.Instance.CheckStateQuest(_activeReplica.Quest) == State.Completed)
                {
                    UpdateStateDialogueGroup();
                    Continue();
                }
            }
            else
            {
                if (_indexActiveReplica < _dialogues.Last().Replicas.Length)
                {
                    if (_indexActiveSpeaker < _dialogues.Length)
                    {
                        UpdateVisualDialogue();

                        if (_activeReplica.Quest != null && _activeReplica.StateQuest.Equals(State.Give))
                        {
                            QuestsSystemController.Instance.NewQuest(_activeReplica.Quest);
                        }

                        _indexActiveSpeaker++;
                    }
                    else
                    {
                        _indexActiveSpeaker = 0;
                        _indexActiveReplica++;
                        Continue();
                    }
                }
                else
                {
                    _indexActiveReplica = 0;
                    UpdateStateDialogueGroup();
                    Hide();
                }
            }
        }

        private void UpdateVisualDialogue()
        {
            _activeSpeaker = _dialogues[_indexActiveSpeaker];
            _activeReplica = _activeSpeaker.Replicas[_indexActiveReplica];

            header.text = _activeSpeaker.Speaker;
            content.text = _activeReplica.Text;
        }

        private void Complete()
        {
            var conteiner = DialogueSystemController.Instance.Dialogues.FirstOrDefault(d => d.Container = _container);
            conteiner.EditState(StateDialogue.Completed);
        }
    }
}
