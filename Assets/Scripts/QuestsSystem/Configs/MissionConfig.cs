using UnityEngine;

namespace Quests.Configs
{
    [CreateAssetMenu(fileName = "MissionConfig", menuName = "ScriptableObject/MissionConfig", order = 0)]
    public class MissionConfig : ScriptableObject
    {
        [SerializeField]
        private string _title;
        public string Title => _title;

        [SerializeField]
        private string _missionId;
        public string MissionId => _missionId;
    }
}
