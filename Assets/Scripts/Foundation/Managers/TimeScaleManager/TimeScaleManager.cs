using UnityEngine;
using System.Collections.Generic;
using Zenject;

namespace Foundation
{
    public sealed class TimeScaleManager : AbstractManager<ITimeScaleManager>, ITimeScaleManager
    {
        List<TimeScaleHandle> handles = new List<TimeScaleHandle>(10);

        void Awake()
        {
            Time.timeScale = 1.0f;
        }

        void UpdateTimeScale()
        {
            float scale = 1.0f;
            foreach (var handle in handles)
                scale *= handle.Scale;
            Time.timeScale = scale;
        }

        public TimeScaleHandle BeginTimeScale(float scale)
        {
            var handle = new TimeScaleHandle(scale);
            handles.Add(handle);
            UpdateTimeScale();
            return handle;
        }

        public void EndTimeScale(TimeScaleHandle handle)
        {
            handles.Remove(handle);
            UpdateTimeScale();
        }
    }
}
