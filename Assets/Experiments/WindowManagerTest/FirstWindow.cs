using Zenject;
using Foundation;
using UnityEngine;

public class FirstWindow : MonoBehaviour
{
    [Inject] IWindowManager _windowManager;
    public void Click()
    {
        _windowManager.OpenWindow("SecondWindow");
    }
}
