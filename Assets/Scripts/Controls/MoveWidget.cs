using System;
using Common;
using UniRx;
using UnityEngine;

namespace Controls
{
    public class MoveWidget : InitMonoBehaviour<Unit>
    {
        [SerializeField]
        private JoystickControl _joystickControl;
        
        private readonly ReactiveCommand<Vector2> _deltaPosition = new();
        public IObservable<Vector2> DeltaPosition => _deltaPosition;
        
        protected override void Init()
        {
            _joystickControl.Init(Unit.Default);
            _joystickControl.DeltaPosition.SkipLatestValueOnSubscribe().Subscribe(deltaPosition => _deltaPosition.Execute(deltaPosition)).AddTo(Disposables);
        }
    }
}
