using System;
using System.Collections.Generic;
using Quests.Configs;
using QuestsSystem.Missions;

namespace QuestsSystem.Quests
{
    public interface IQuest : IDisposable
    {
        bool IsDone { get; }
        QuestConfig Config { get; }
        IReadOnlyList<IMission> Missions { get; }
        IObservable<QuestConfig> OnQuestCompleted { get; }
        IObservable<IMission> OnTrackingMission { get; }
    }
}
