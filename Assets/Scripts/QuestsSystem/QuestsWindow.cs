using Core.Base;
using QuestSystem.SO;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace QuestSystem.UI
{
    [AddComponentMenu(menuName: "QuestsSystem/UI/QuestsWindow", order: 1)]
    public sealed class QuestsWindow : BaseWindow<QuestsWindow>
    {
        [Space(10f)]
        [SerializeField] private TextMeshProUGUI header;
        [SerializeField] private TextMeshProUGUI discription;
        [SerializeField] private TextMeshProUGUI target;

        private QuestSO _quest;

        public void SetQuest(QuestSO quest)
        {
            _quest = quest;

            header.text = $"Квест: {_quest.Name}";
            discription.text = $"Задача: {_quest.Description}";
            target.text = $"Прогресс: {_quest.Progress}/{_quest.Target}";
        }

        public void UpdateProgressTarget()
        {
            target.text = $"Прогресс: {_quest.Progress}/{_quest.Target}";
        }
    }
}
