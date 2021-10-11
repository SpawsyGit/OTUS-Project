using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using DG.Tweening;
using Zenject;

namespace Foundation
{
    public sealed class LoadingScreen : AbstractBehaviour, IOnBeginSceneLoad, IOnEndSceneLoad
    {
        [Inject] ISceneManager manager;
        public Image image;

        protected override void OnEnable()
        {
            base.OnEnable();
            Observe(manager.OnBeginSceneLoad);
            Observe(manager.OnEndSceneLoad);
        }

        async Task IOnBeginSceneLoad.Do()
        {
            await image.DOFade(1.0f, 1.0f).AsyncWaitForCompletion();
        }

        async Task IOnEndSceneLoad.Do()
        {
            await image.DOFade(0.0f, 1.0f).AsyncWaitForCompletion();
        }
    }
}
