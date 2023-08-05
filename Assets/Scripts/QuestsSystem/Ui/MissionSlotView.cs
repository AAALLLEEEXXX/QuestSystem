using Common;
using TMPro;
using UnityEngine;

namespace QuestsSystem.Ui
{
    public class MissionSlotView : InitMonoBehaviour<MissionSlotView.Params>
    {
        public readonly struct Params
        {
            public readonly string TitleMission;

            public Params(string titleMission)
            {
                TitleMission = titleMission;
            }
        }
    
        [SerializeField] 
        private TMP_Text _textMission;

        protected override void Init()
        {
            _textMission.text = InputParams.TitleMission;
        }
    }
}
