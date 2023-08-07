using System;
using UniRx;
using UnityEngine;

namespace QuestsSystem.Missions
{
    public interface IMissionObject
    {
        Vector3 Position { get; }
        IObservable<Unit> OnActivateObject { get; }
        void StateComplete();
        void StateActive();
    }
}
