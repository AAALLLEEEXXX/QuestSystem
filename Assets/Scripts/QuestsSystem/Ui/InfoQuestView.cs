using System.Collections.Generic;
using Common;
using Quests.Configs;
using TMPro;
using UnityEngine;

namespace QuestsSystem.Ui
{
    public class InfoQuestView : InitMonoBehaviour<InfoQuestView.Params>
    {
        public readonly struct Params
        {
            public readonly QuestConfig QuestConfig;
            public readonly PrefabPool PrefabPool;

            public Params(QuestConfig questConfig, PrefabPool prefabPool)
            {
                QuestConfig = questConfig;
                PrefabPool = prefabPool;
            }
        }
    
        [SerializeField] 
        private TMP_Text _titleQuest;
    
        [SerializeField] 
        private TMP_Text _descriptionQuest;

        [SerializeField] 
        private RectTransform _missionsMountRoot;
    
        [SerializeField] 
        private MissionSlotView _missionSlotView;

        private readonly List<MissionSlotView> _slots = new();

        protected override void Init()
        {
            _titleQuest.text = InputParams.QuestConfig.Title;
            _descriptionQuest.text = InputParams.QuestConfig.Description;

            foreach (var missionConfig in InputParams.QuestConfig.Missions)
            {
                var missionSlot = InputParams.PrefabPool.Get(_missionSlotView, new MissionSlotView.Params(missionConfig.Title),
                    _missionsMountRoot);
            
                _slots.Add(missionSlot);
            }
        }

        public override void Dispose()
        {
            foreach (var slot in _slots)
                slot.Dispose();
        
            _slots.Clear();
        
            base.Dispose();
        }
    }
}
