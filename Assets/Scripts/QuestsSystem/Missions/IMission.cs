using System;
using UnityEngine;

namespace QuestsSystem.Missions
{
    public interface IMission : IDisposable
    {
        bool IsCompleted { get; }
        Vector3 Point { get; }
        void Reset();
        IObservable<IMission> OnCompleted { get; }
    }
}
