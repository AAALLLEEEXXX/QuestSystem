using Common;
using Controls;
using QuestsSystem.Quests;
using QuestsSystem.Tracking;
using QuestsSystem.Ui;
using UniRx;
using UnityEngine;

public class WorldUiCanvas : InitMonoBehaviour<WorldUiCanvas.Params>
{
    public readonly struct Params
    {
        public readonly QuestConfigurator QuestConfigurator;
        public readonly MissionPointTracker MissionPointTracker;
        public readonly PrefabPool PrefabPool;

        public Params(QuestConfigurator questConfigurator, MissionPointTracker missionPointTracker, PrefabPool prefabPool)
        {
            QuestConfigurator = questConfigurator;
            MissionPointTracker = missionPointTracker;
            PrefabPool = prefabPool;
        }
    }

    [SerializeField] 
    private HudWindow _hudWindow;
    
    [SerializeField] 
    private QuestWindow _questWindow;
    
    [SerializeField] 
    private TrackedMissionWindow _trackedMissionWindow;
    public TrackedMissionWindow TrackedMissionWindow => _trackedMissionWindow;

    [SerializeField] 
    private MoveWidget _moveWidget;
    public MoveWidget MoveWidget => _moveWidget;

    protected override void Init()
    {
        _hudWindow.Init(Unit.Default);
        _hudWindow.OnShowQuestWindow.Subscribe(_ => ShowQuestWindow()).AddTo(Disposables);
        
        _moveWidget.Init(Unit.Default);

        InitTrackingQuest();
    }

    private void InitTrackingQuest()
    {
        _trackedMissionWindow.Init(Unit.Default);

        TrackingQuest(InputParams.QuestConfigurator.FirstNotDoneQuest);
        
        InputParams.QuestConfigurator.OnQuestCompleted
            .Subscribe(_ => TrackingQuest(InputParams.QuestConfigurator.FirstNotDoneQuest)).AddTo(Disposables);
    }

    public override void Dispose()
    {
        InputParams.MissionPointTracker?.Dispose();
        
        base.Dispose();
    }

    private void ShowQuestWindow()
    {
        var instanceQuestWindow = InputParams.PrefabPool.Get(_questWindow, 
            new QuestWindow.Params(InputParams.QuestConfigurator.Quests, InputParams.PrefabPool), transform);

        instanceQuestWindow.OnTrackingQuest.Subscribe(TrackingQuest).AddTo(instanceQuestWindow.Disposables);
    }

    private void TrackingQuest(IQuest quest)
    {
        InputParams.MissionPointTracker.TrackedQuest(quest);
    }
}
