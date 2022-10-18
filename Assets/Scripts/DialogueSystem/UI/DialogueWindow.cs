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

        public void UpdateDialoguePanel(string speaker, string replica)
        {
            header.text = speaker;
            content.text = replica;
        }

        public override void Show()
        {
            base.Show();
        }

        public override void Hide()
        {
            base.Hide();    
        }
    }
}
