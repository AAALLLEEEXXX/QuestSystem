using System;
using System.Collections.Generic;
using Common;
using Quests.Configs;
using QuestsSystem.Missions;
using UniRx;
using UnityEngine;

namespace QuestsSystem.Quests
{
    public class QuestConfigurator : InitMonoBehaviour<Unit>
    {
        [SerializeField]
        private List<QuestConfig> _questConfigs;

        [SerializeField]
        private SerializableDictionary<string, MissionObjectViewBase> _missionObjects;

        private List<IQuest> _quests = new();
        public List<IQuest> Quests => _quests;
        
        private ReactiveCommand<QuestConfig> _onQuestCompleted = new();
        public IObservable<QuestConfig> OnQuestCompleted => _onQuestCompleted;

        protected override void Init()
        {
            var questFactory = new QuestFactory(_missionObjects);
            
            foreach (var questConfig in _questConfigs)
            {
                var quest = questFactory.CreateQuest(questConfig);
                quest.OnQuestCompleted.Subscribe(QuestCompleted).AddTo(Disposables);

                _quests.Add(quest);
            }
        }

        private void QuestCompleted(QuestConfig config)
        {
            _onQuestCompleted?.Execute(config);
        }

        public override void Dispose()
        {
            foreach (var quest in _quests)
                quest.Dispose();

            _quests.Clear();
        }
    }
}
