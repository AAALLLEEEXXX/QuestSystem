using System;
using Common;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class HudWindow : InitMonoBehaviour<Unit>
{
    [SerializeField] 
    private Button _questsButton;

    private ReactiveCommand _onShowQuestWindow = new();

    public IObservable<Unit> OnShowQuestWindow => _onShowQuestWindow;
    
    protected override void Init()
    {
        _questsButton.OnClickAsObservable().Subscribe(_ => _onShowQuestWindow.Execute()).AddTo(Disposables);
    }
}
