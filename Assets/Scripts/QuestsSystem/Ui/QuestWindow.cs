using System.Collections.Generic;
using System.Linq;
using Common;
using Quests.Configs;
using QuestsSystem.Quests;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace QuestsSystem.Ui
{
    public class QuestWindow : InitMonoBehaviour<QuestWindow.Params>
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
        private RectTransform _mountRootQuestSlots;
    
        [SerializeField] 
        private QuestSlotView _questSlotView;

        [SerializeField] 
        private InfoQuestView _infoQuestView;
        
        [SerializeField] 
        private Button _closeButton;

        private List<QuestSlotView> _slots = new();

        protected override void Init()
        {
            foreach (var quest in InputParams.Quests)
            {
                var questSlot = InputParams.PrefabPool.Get(_questSlotView, new QuestSlotView.Params(quest.Config), _mountRootQuestSlots);
                questSlot.OnSelectedSlot.Subscribe(SelectedQuest).AddTo(Disposables);
            
                _slots.Add(questSlot);
            }

            _closeButton.OnClickAsObservable().Subscribe(_ => Dispose()).AddTo(Disposables);

            var firstSlot = _slots.FirstOrDefault();
        
            if (firstSlot == null)
                return;
        
            SelectedQuest(firstSlot.Config);
        }

        private void SelectedQuest(QuestConfig config)
        {
            foreach (var slot in _slots)
                slot.ChangeSelectedSlot(slot.Config.Id == config.Id);
        
            _infoQuestView.Dispose();
            _infoQuestView.Init(new InfoQuestView.Params(config, InputParams.PrefabPool));
        }

        public override void Dispose()
        {
            _infoQuestView.Dispose();
        
            foreach (var slot in _slots)
                slot.Dispose();
        
            _slots.Clear();
            
            base.Dispose();
        }
    }
}
