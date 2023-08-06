using System.Collections.Generic;
using Common;
using Controls;
using QuestsSystem.Quests;
using QuestsSystem.Ui;
using UniRx;
using UnityEngine;

public class WorldUiCanvas : InitMonoBehaviour<WorldUiCanvas.Params>
{
    public readonly struct Params
    {
        public readonly List<IQuest> Quests;
        public readonly PrefabPool PrefabPool;

        public Params(List<IQuest> quests, PrefabPool prefabPool)
        {
            Quests = quests;
            PrefabPool = prefabPool;
        }
    }

    [SerializeField] 
    private HudWindow _hudWindow;
    
    [SerializeField] 
    private QuestWindow _questWindow;
    
    [SerializeField] 
    private MoveWidget _moveWidget;
    public MoveWidget MoveWidget => _moveWidget;

    protected override void Init()
    {
        base.Init();

        _hudWindow.Init(Unit.Default);
        _hudWindow.OnShowQuestWindow.Subscribe(_ => ShowQuestWindow()).AddTo(Disposables);
        
        _moveWidget.Init(Unit.Default);
    }

    private void ShowQuestWindow()
    {
        InputParams.PrefabPool.Get(_questWindow, 
            new QuestWindow.Params(InputParams.Quests, InputParams.PrefabPool), transform);
    }
}
