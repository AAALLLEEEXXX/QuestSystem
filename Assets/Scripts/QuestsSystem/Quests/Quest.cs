using System;
using System.Collections.Generic;
using System.Linq;
using Quests.Configs;
using QuestsSystem.Missions;
using UniRx;
using UnityEngine;

namespace QuestsSystem.Quests
{
    public class Quest : IQuest
    {
        private readonly List<IMission> _missions;
        public IReadOnlyList<IMission> Missions => _missions;

        private readonly ReactiveCommand<QuestConfig> _onQuestCompleted = new();
        public IObservable<QuestConfig> OnQuestCompleted => _onQuestCompleted;

        private readonly ReactiveCommand<IMission> _onTrackingMission = new();
        public IObservable<IMission> OnTrackingMission => _onTrackingMission;

        private QuestConfig _questConfig;
        public QuestConfig Config => _questConfig;
        
        private readonly CompositeDisposable _disposables = new();

        public bool IsDone => _missions.All(value => value.IsCompleted);

        public Quest(List<IMission> missions, QuestConfig questConfig)
        {
            _questConfig = questConfig;
            _missions = missions;
            
            SubscribesOnMissions();
            ResetMission(0);
        }

        private void SubscribesOnMissions()
        {
            foreach (var mission in _missions)
                mission.OnCompleted.Subscribe(OnMissionCompleted).AddTo(_disposables);
        }

        private void OnMissionCompleted(IMission mission)
        {
            if (IsDone)
            {
                _onQuestCompleted?.Execute(_questConfig);
                Debug.Log("Common Quest done!");
            }
            else
            {
                var index = _missions.IndexOf(mission);
                ResetMission(++index);
            }
        }

        private void ResetMission(int index)
        {
            if (index < 0 || index >= _missions.Count)
                return;

            var nextMission = _missions[index];

            if (nextMission.IsCompleted)
            {
                OnMissionCompleted(nextMission);
                _onTrackingMission?.Execute(null);
            }
            else
            {
                nextMission.Reset();
                _onTrackingMission?.Execute(nextMission);
            }
        }

        public void Dispose()
        {
            _disposables.Clear();
        }
    }
}
