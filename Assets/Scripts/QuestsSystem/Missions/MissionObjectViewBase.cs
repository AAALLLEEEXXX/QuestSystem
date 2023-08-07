using System;
using UniRx;
using UnityEngine;

namespace QuestsSystem.Missions
{
    public abstract class MissionObjectViewBase : MonoBehaviour , IMissionObject
    {
        [SerializeField]
        private Transform _pointTracking;
        
        public Vector3 Position => _pointTracking != null ? _pointTracking.position : transform.position;

        protected ReactiveCommand _onActivateObject = new();
        public IObservable<Unit> OnActivateObject => _onActivateObject;
        
        public virtual void StateComplete()
        {
        }

        public virtual void StateActive()
        {
        }
    }
}
