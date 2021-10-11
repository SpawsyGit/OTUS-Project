using System.Collections.Generic;
using UnityEngine;

namespace Foundation
{
    public abstract class AbstractBehaviour : MonoBehaviour
    {
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Observables

        List<ObserverHandle> observerHandles;

        protected ObserverHandle AllocObserverHandle()
        {
            var handle = new ObserverHandle();
            AddObserverHandle(handle);
            return handle;
        }

        protected void AddObserverHandle(ObserverHandle handle)
        {
            if (observerHandles == null)
                observerHandles = new List<ObserverHandle>();
            observerHandles.Add(handle);
        }

        protected void Observe<T>(IObserverList<T> observable)
        {
            ObserverHandle handle = null;
            observable.Add(ref handle, this);
            AddObserverHandle(handle);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Unity callbacks

        protected virtual void OnEnable()
        {
        }

        protected virtual void OnDisable()
        {
            foreach (var handle in observerHandles)
                handle.Dispose();
        }
    }
}
