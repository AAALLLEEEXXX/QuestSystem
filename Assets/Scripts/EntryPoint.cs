using Common;
using Player;
using QuestsSystem.Quests;
using QuestsSystem.Tracking;
using UniRx;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] 
    private WorldUiCanvas _worldUiCanvas;
    
    [SerializeField] 
    private QuestConfigurator _questConfigurator;
    
    [SerializeField] 
    private PlayerView _playerView;
    
    [SerializeField] 
    private PrefabPool _prefabPool;

    private void Start()
    {
        _questConfigurator.Init(Unit.Default);
        
        var missionPointTracker = new MissionPointTracker(_playerView.Camera, _worldUiCanvas.TrackedMissionWindow, _prefabPool);
        _worldUiCanvas.Init(new WorldUiCanvas.Params(_questConfigurator, missionPointTracker, _prefabPool));
        
        _playerView.Init(new PlayerView.Params(_worldUiCanvas.MoveWidget));
    }

    private void OnDestroy()
    {
        _questConfigurator.Dispose();
        _worldUiCanvas.Dispose();
        _playerView.Dispose();
    }
}
