
using Foundation;
using UnityEngine;
using Zenject;

public class TimeScaleDebug : MonoBehaviour
{
    public float scale = 0.5f;
    [Inject] ITimeScaleManager timeScaleManager;
    TimeScaleHandle timeScaleHandle;

    void Awake()
    {
        enabled = false;
    }

    void OnEnable()
    {
        timeScaleHandle = timeScaleManager.BeginTimeScale(scale);
    }

    void OnDisable()
    {
        timeScaleManager.EndTimeScale(timeScaleHandle);
    }
}
