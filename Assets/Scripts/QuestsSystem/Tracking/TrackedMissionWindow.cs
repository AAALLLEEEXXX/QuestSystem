using System.Collections.Generic;
using Common;
using UniRx;
using UnityEngine;

namespace QuestsSystem.Tracking
{
    public class TrackedMissionWindow : InitMonoBehaviour<Unit>
    {
        [SerializeField] 
        private MarkerMission _markerMission;
        public MarkerMission MarkerMission => _markerMission;
    
        [SerializeField]
        private RectTransform _anchor;
        public RectTransform Anchor => _anchor;

        [SerializeField]
        private List<RectTransform> _constraintPolygon;

        private List<Vector2> _localConstraint;
        public List<Vector2> LocalConstraint => _localConstraint;

        protected override void Init()
        {
            UpdateLocalConstraint();
        }

        private void UpdateLocalConstraint()
        {
            _localConstraint = _constraintPolygon.ConvertAll(arg =>
            {
                var localPosition = arg.position;
                var localPositionXY = new Vector2(localPosition.x, localPosition.y);
                return localPositionXY;
            });
        }
    }
}
