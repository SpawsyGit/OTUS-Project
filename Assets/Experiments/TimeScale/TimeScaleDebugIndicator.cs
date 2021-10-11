
using UnityEngine;
using TMPro;

public class TimeScaleDebugIndicator : MonoBehaviour
{
    TextMeshProUGUI text;

    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        text.text = $"TimeScale: {Time.timeScale}";
    }
}
