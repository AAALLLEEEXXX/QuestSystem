using Common;
using Player;
using QuestsSystem.Quests;
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
        _worldUiCanvas.Init(new WorldUiCanvas.Params(_questConfigurator.Quests, _prefabPool));
        
        _playerView.Init(new PlayerView.Params(_worldUiCanvas.MoveWidget));
    }
}
