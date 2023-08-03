using System;
using UnityEngine;

namespace Common
{
    public abstract class InitMonoBehaviour<T> : MonoBehaviour, IInitializable<T>
    {
        protected T InputParams { get; private set; }

        private Action _onDispose;
    
        public Action OnDispose
        {
            get => _onDispose;
            set => _onDispose = value;
        }

        public void Init(T inputParams)
        {
            InputParams = inputParams;
            enabled = true;
            Init();
        }

        public virtual void Dispose()
        {
            InputParams = default;
            enabled = false;
        
            _onDispose?.Invoke();
            _onDispose = null;
        }

        protected virtual void Init()
        {
        }
    
        protected virtual void OnDestroy()
        {
            Dispose();
        }
    }
}
