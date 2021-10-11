
using Foundation;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SceneLoadButton : MonoBehaviour
{
    public string sceneName;
    [Inject] ISceneManager sceneManager;

    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => sceneManager.LoadSceneAsync(sceneName));
    }
}
