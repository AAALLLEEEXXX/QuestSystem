using System;
using System.Collections.Generic;
using Quests;
using Quests.Configs;
using QuestsSystem.Missions;
using UnityEngine;

namespace QuestsSystem.Quests
{
    public class QuestFactory
    {
        private readonly SerializableDictionary<string, MissionObjectViewBase> _missionObjects;

        private readonly Dictionary<QuestType, Func<List<IMission>, QuestConfig, IQuest>> _questFactories = new()
        {
            { QuestType.Common, (missionCollection, questConfig) => new Quest(missionCollection, questConfig) }
        };
        
        public QuestFactory(SerializableDictionary<string, MissionObjectViewBase> missionObjects)
        {
            _missionObjects = missionObjects;
        }
        
        public IQuest CreateQuest(QuestConfig questConfig)
        {
            var missions = new List<IMission>();

            foreach (var missionConfig in questConfig.Missions)
            {
                var mission = CreateMission(missionConfig);

                if (mission == null)
                    continue;

                missions.Add(mission);
            }

            return _questFactories[questConfig.QuestType]?.Invoke(missions, questConfig);
        }

        private IMission CreateMission(MissionConfig missionConfig)
        {
            if (_missionObjects.TryGetValue(missionConfig.MissionId, out var missionObject))
                return new Mission(missionObject);

            Debug.LogWarning($"Can't find view of quest {missionConfig.MissionId}");
            return null;
        }
    }
}
