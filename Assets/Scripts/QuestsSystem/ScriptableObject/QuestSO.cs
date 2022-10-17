using QuestSystem.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestSystem.SO
{
    [CreateAssetMenu(fileName = "Quest", menuName = "QuestSystem/Quest", order = 1)]
    public sealed class QuestSO : ScriptableObject, IQuest
    {
        [SerializeField] private new string name;
        [SerializeField, TextArea(10, 10)] private string description;
        [SerializeField] private int amountTarget;

        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public int AmountTarget { get => amountTarget; set => amountTarget = value; }
    }
}
