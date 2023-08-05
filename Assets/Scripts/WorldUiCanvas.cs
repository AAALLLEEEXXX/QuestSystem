using System.Collections.Generic;
using Common;
using QuestsSystem.Quests;
using QuestsSystem.Ui;
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
    private QuestWindow _questWindow;

    protected override void Init()
    {
        base.Init();
        
        _questWindow.Init(new QuestWindow.Params(InputParams.Quests, InputParams.PrefabPool));
    }
}
