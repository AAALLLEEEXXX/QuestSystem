using System;
using UnityEngine;

namespace QuestsSystem.Missions
{
    public abstract class MissionObjectViewBase : MonoBehaviour , IMissionObject
    {
        [SerializeField]
        private Transform _pointTracking;
        
        public Vector3 Position => _pointTracking != null ? _pointTracking.position : transform.position;
        public IObservable<IMission> OnActivateObject { get; }
        
        public virtual void StateComplete()
        {
        }

        public virtual void StateActive()
        {
        }
    }
}
