using UnityEngine;
using Foundation;
using Zenject;

public class WindowManagerTest : MonoBehaviour
{
    [Inject] IWindowManager _windowManager;

    public void Click()
    {
        _windowManager.OpenWindow("FirstWindow");
    }
}
