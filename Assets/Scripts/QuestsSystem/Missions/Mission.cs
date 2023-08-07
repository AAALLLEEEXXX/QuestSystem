using System;
using UniRx;
using UnityEngine;
using Utils;

namespace QuestsSystem.Missions
{
    public class Mission : IMission
    {
        private MissionObjectViewBase _objectView;
        private bool _active;
        
        private readonly ReactiveCommand<IMission> _onCompleted = new();
        public IObservable<IMission> OnCompleted => _onCompleted;

        public bool IsCompleted { get; private set; }
        public Vector3 Point => _objectView.Position;

        private IDisposable _completeDisposable;
        
        public Mission(MissionObjectViewBase objectView)
        {
            _objectView = objectView;
        }
    
        public void Reset()
        {
            if (_active)
                return;

            _active = true;
            IsCompleted = false;
            DisposeUtils.DisposeAndSetNull(ref _completeDisposable);
            
            _completeDisposable = _objectView.OnActivateObject.Subscribe(_ => Complete());
            _objectView.StateActive();
        }

        public void Dispose()
        {
            DisposeUtils.DisposeAndSetNull(ref _completeDisposable);
        }

        private void Complete()
        {
            if (!_active)
                return;

            _active = false;
            IsCompleted = true;
            DisposeUtils.DisposeAndSetNull(ref _completeDisposable);
            
            _objectView.StateComplete();
            _onCompleted?.Execute(this);
        }
    }
}
