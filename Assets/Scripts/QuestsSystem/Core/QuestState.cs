using QuestSystem.SO;
using static QuestSystem.Base.QuestEnums;

namespace QuestSystem.Base
{
    [System.Serializable]
    public sealed class QuestState
    {
        public QuestSO Quest;
        public State State;

        public QuestState(QuestSO quest, State state)
        {
            Quest = quest;
            State = state;
        }
    }
}
