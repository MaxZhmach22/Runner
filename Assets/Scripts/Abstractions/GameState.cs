using System;
using UnityEngine;


namespace Runner
{
    internal abstract class GameState : IDisposable
    {
        #region Methods

        public abstract void Update();

        public virtual void Start() { }

        public virtual void Dispose() { }

        public virtual void OnTriggerEnter(Collider other) { }

        #endregion
    }
}