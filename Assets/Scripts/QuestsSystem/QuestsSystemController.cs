using Core.Utils;
using QuestSystem.Base;
using QuestSystem.SO;
using QuestSystem.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static QuestSystem.Base.QuestEnums;

namespace QuestSystem
{
    [AddComponentMenu(menuName: "QuestsSystem/Controller/QuestsSystemController", order: 1)]
    public sealed class QuestsSystemController : MonobehSingleton<QuestsSystemController>
    {
        [SerializeField] private QuestsWindow questsWindow;
        [Space(10f)]
        [SerializeField] private List<QuestState> quests;

        public List<QuestState> Quests => quests;

        public QuestSO ActiveQuest;

        public void NewQuest(QuestSO quest)
        {
            Quests.Add(new QuestState(quest, State.InProcess));
            ActiveQuest = quest;
            ShowQuestsWindow(quest);
        }

        public void UpdateProgressQuest(QuestSO quest, int amountProgress)
        {
            quest.Progress += amountProgress;
            questsWindow.UpdateProgressTarget();
            CheckStateQuest(quest);
        }

        public void CheckStateQuest(QuestSO quest)
        {
            if (quest.Progress >= quest.Target)
            {
                CompleteQuest(quest);
            }
        }

        public void CompleteQuest(QuestSO quest)
        {
            Quests.FirstOrDefault(q => q.Quest == quest).State = State.Completed;
            //HideQuestsWindow();
        }

        public State GetStateQuest(QuestSO quest)
        {
            return Quests.FirstOrDefault(q => q.Quest == quest).State;
        }

        public void ShowQuestsWindow(QuestSO quest)
        {
            questsWindow.Show();
            questsWindow.SetQuest(quest);
        }

        public void HideQuestsWindow()
        {
            questsWindow.Hide();
        }        
    }
}
