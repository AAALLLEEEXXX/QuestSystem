using System;
using System.Linq;
using Common;
using QuestsSystem.Missions;
using QuestsSystem.Quests;
using UniRx;
using UnityEngine;
using Utils;

namespace QuestsSystem.Tracking
{
    public class MissionPointTracker : IDisposable
    {
        private readonly Camera _camera;
        private readonly TrackedMissionWindow _trackedMarkerWindowView;
        private readonly PrefabPool _prefabPool;

        private MarkerMission _instanceMarker;
        private Vector3 _currentMissionPoint;
        
        private readonly CompositeDisposable _disposables = new();

        public MissionPointTracker(Camera camera, TrackedMissionWindow trackedMissionWindow, PrefabPool prefabPool)
        {
            _camera = camera;
            _trackedMarkerWindowView = trackedMissionWindow;
            _prefabPool = prefabPool;
        }

        public void Dispose()
        {
            DisposeMarker();
        }

        public void TrackedQuest(IQuest quest)
        {
            DisposeMarker();
            
            if (quest == null)
                return;
            
            var firstNotCompletedMission = quest.Missions.FirstOrDefault(mission => !mission.IsCompleted);
            TrackedMissionPoint(firstNotCompletedMission);
            
            quest.OnTrackingMission.Subscribe(TrackedMissionPoint).AddTo(_disposables);
        }

        private void TrackedMissionPoint(IMission mission)
        {
            if (mission == null)
                return;

            _currentMissionPoint = mission.Point;
            
            DisposeMarker();
            _instanceMarker = _prefabPool.Get(_trackedMarkerWindowView.MarkerMission, Unit.Default, _trackedMarkerWindowView.Anchor);
        
            Observable.EveryUpdate().Subscribe(_ => OnTick()).AddTo(_disposables);
        }

        private void DisposeMarker()
        {
            _disposables.Clear();

            if (_instanceMarker == null) 
                return;
            
            _instanceMarker.Dispose();
            _instanceMarker = null;
        }

        private void OnTick()
        {
            UpdateMarkPositionAndDirection();
        }

        private void UpdateMarkPositionAndDirection()
        {
            if (_instanceMarker == null)
                return;
        
            var markScreenSpacePosition = _camera.WorldToScreenPoint(_currentMissionPoint);
            var clampedPosition = markScreenSpacePosition;

            var isBounded = true;

            if (!GeometryUtils.IsPointInsidePolygon(markScreenSpacePosition, _trackedMarkerWindowView.LocalConstraint))
            {
                isBounded = false;
                clampedPosition = GeometryUtils.GetClosestBorderPointTo(markScreenSpacePosition, _trackedMarkerWindowView.LocalConstraint);
            }
            
            var markDirection = (markScreenSpacePosition - clampedPosition).normalized;
            _instanceMarker.UpdateDirection(markDirection);
            _instanceMarker.ChangeState(isBounded);
            
            _instanceMarker.RectTransform.position = clampedPosition;
        }
    }
}
