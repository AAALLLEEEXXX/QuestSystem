using System;
using Common;
using Quests.Configs;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace QuestsSystem.Ui
{
    public class QuestSlotView : InitMonoBehaviour<QuestSlotView.Params>
    {
        public readonly struct Params
        {
            public readonly QuestConfig Config;

            public Params(QuestConfig config)
            {
                Config = config;
            }
        }
    
        [SerializeField] 
        private TMP_Text _title;

        [SerializeField] 
        private Image _selectedBackground;
    
        [SerializeField] 
        private Button _selectedButton;

        public QuestConfig Config => InputParams.Config;
    
        private ReactiveCommand<QuestConfig> _onSelectedSlot = new();
        public IObservable<QuestConfig> OnSelectedSlot => _onSelectedSlot;

        protected override void Init()
        {
            _title.text = Config.Title;
            _selectedButton.OnClickAsObservable().Subscribe(_ => SelectedSlot()).AddTo(Disposables);
        }

        public void ChangeSelectedSlot(bool isSelected)
        {
            _selectedBackground.gameObject.SetActive(isSelected);
        }

        private void SelectedSlot()
        {
            _onSelectedSlot?.Execute(Config);
        }
    }
}
