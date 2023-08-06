using Common;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Controls
{
    public class JoystickControl : InitMonoBehaviour<Unit>, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        private const float XThreshold = 0.5f;
        private const float YThreshold = 0.5f;

        [SerializeField]
        private RectTransform _areaRectTransform;

        [SerializeField]
        private RectTransform _padRectTransform;

        private Vector2 _normalizedPosition;
        private Vector2 _padStartPosition;
        private Vector2 _padCurrentPosition;
        
        private float Radius => _areaRectTransform.rect.size.x / 2;

        private readonly ReactiveProperty<Vector2> _deltaPosition = new();
        public IReadOnlyReactiveProperty<Vector2> DeltaPosition => _deltaPosition;

        protected override void Init()
        {
            _padStartPosition = _padRectTransform.localPosition;
            _deltaPosition.Value = Vector2.zero;
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnEndDrag();
        }

        public void OnDrag(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_areaRectTransform,
                eventData.position, eventData.enterEventCamera, out var localPoint);

            _padCurrentPosition.x = Mathf.Clamp(localPoint.x, -Radius, Radius);
            _padCurrentPosition.y = Mathf.Clamp(localPoint.y, -Radius, Radius);

            _padRectTransform.localPosition = _padCurrentPosition;

            OffsetFromOriginalPosition();
        }

        private void OnEndDrag()
        {
            _padRectTransform.localPosition = _padStartPosition;

            _normalizedPosition = Vector2.zero;
            _deltaPosition.Value = Vector2.zero;
        }

        private void OffsetFromOriginalPosition()
        {
            var localPosition = _padRectTransform.localPosition;
            
            var deltaX = Mathf.Clamp(localPosition.x / Radius, -1.0f, 1.0f);
            _normalizedPosition.x = Mathf.Abs(deltaX) > XThreshold ? deltaX : 0;

            var deltaY = Mathf.Clamp(localPosition.y / Radius, -1.0f, 1.0f);
            _normalizedPosition.y = Mathf.Abs(deltaY) > YThreshold ? deltaY : 0;

            _deltaPosition.Value = _normalizedPosition;
        }
    }
}
