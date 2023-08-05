using System;
using UnityEngine;

namespace QuestsSystem.Missions
{
    public interface IMissionObject
    {
        Vector3 Position { get; }
        IObservable<IMission> OnActivateObject { get; }
        void StateComplete();
        void StateActive();
    }
}
