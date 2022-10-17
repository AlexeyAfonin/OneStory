using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DialogueSystem.SO;
using System.Linq;
using static DialogueSystem.Core.DialogueEnums;

namespace DialogueSystem.UI
{
    [AddComponentMenu(menuName: "DialogueSystem/UI/DialogueWindow", order: 1)]
    public class DialogueWindow : MonoBehaviour
    {
        [Header("GUI")]
        [SerializeField] private TextMeshProUGUI header;
        [SerializeField] private TextMeshProUGUI content;

        private TypeDialogueGroup activeTypeDialogueGroup = TypeDialogueGroup.None;

        private DialogueContainerSO _container;
        private DialogueGroupSO _group;
        private DialogueSO[] _dialogues;

        private int _indexActiveSpeaker = 0;
        private int _indexActiveReplica = 0;

        public void StartDialogue(DialogueContainerSO container, StateDialogue stateDialogue)
        {
            _container = container;
            if (stateDialogue == StateDialogue.NotStarted)
            {
                UpdateStateDialogue();
            }
            Continue();
        }

        private void UpdateStateDialogue()
        {
            if (activeTypeDialogueGroup != TypeDialogueGroup.Complete)
            {
                activeTypeDialogueGroup += 1;
                _group = _container.GetGroup(activeTypeDialogueGroup);
                _dialogues = _group.Dialogues;
            }
            else
            {
                Complete();
            }
        }

        public void Continue()
        {
            if (_indexActiveReplica < _dialogues.Last().Replicas.Length)
            {
                if (_indexActiveSpeaker < _dialogues.Length)
                {
                    header.text = _dialogues[_indexActiveSpeaker].Speaker;
                    content.text = _dialogues[_indexActiveSpeaker].Replicas[_indexActiveReplica].Text;

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
                UpdateStateDialogue();
                DialogueSystemController.Instance.HideWindow();
            }
        }

        private void Complete()
        {
            var conteiner = DialogueSystemController.Instance.Dialogues.FirstOrDefault(d => d.Container = _container);
            conteiner.EditState(StateDialogue.Completed);
        }
    }
}
