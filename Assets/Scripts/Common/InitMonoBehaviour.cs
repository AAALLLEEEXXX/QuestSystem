using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Common
{
    public abstract class InitMonoBehaviour<T> : MonoBehaviour, IInitializable<T>
    {
        private readonly CompositeDisposable _disposables = new();
        private Action _onDispose;
        
        protected T InputParams { get; private set; }
        public ICollection<IDisposable> Disposables => _disposables;
        
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
            _disposables.Clear();
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
