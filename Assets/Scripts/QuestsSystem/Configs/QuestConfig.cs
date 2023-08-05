using System.Collections.Generic;
using UnityEngine;

namespace Quests.Configs
{
    [CreateAssetMenu(fileName = "QuestConfig", menuName = "ScriptableObject/QuestConfig", order = 0)]
    public class QuestConfig : ScriptableObject
    {
        [SerializeField]
        private int _id;
        public int Id => _id;
    
        [SerializeField]
        private QuestType _questType;
        public QuestType QuestType => _questType;

        [SerializeField]
        private string _title;
        public string Title => _title;

        [SerializeField]
        private string _description;
        public string Description => _description;

        [SerializeField]
        private List<MissionConfig> _missions;
        public List<MissionConfig> Missions => _missions;
    }
}