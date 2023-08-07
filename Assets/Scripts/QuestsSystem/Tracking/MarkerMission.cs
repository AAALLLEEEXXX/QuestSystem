using Common;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace QuestsSystem.Tracking
{
    public class MarkerMission : InitMonoBehaviour<Unit>
    {
        [SerializeField] 
        private RectTransform _rectTransform;
    
        [SerializeField]
        private RectTransform _directionTransform;
    
        [SerializeField] 
        private Image _pointIcon;
    
        [SerializeField] 
        private Image _arrowIcon;

        public RectTransform RectTransform => _rectTransform;

        public void ChangeState(bool isBounded)
        {
            _pointIcon.gameObject.SetActive(isBounded);
            _arrowIcon.gameObject.SetActive(!isBounded);
        }
    
        public void UpdateDirection(Vector3 direction)
        {
            _directionTransform.up = direction;
        }
    }
}
