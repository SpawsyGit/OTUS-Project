using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Window : MonoBehaviour, IWindow
{
    #region Fields

    [SerializeField] public Image background;

    public GameObject WindowObject => gameObject;
    public object returnOnComplete;

    #endregion

    #region Properties

    public Action<object> OnClosed { get; set; }

    #endregion

    #region Methods

    public IWindow Create(Transform root)
    {
        return Instantiate(this, root);
    }
    public virtual void Open(object data = null)
    {
        gameObject.SetActive(true);
        background.rectTransform.localScale = Vector3.zero;
        background.rectTransform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
    }


    public virtual void Close()
    {
        background.rectTransform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack)
            .OnComplete(() => OnClosed?.Invoke(returnOnComplete));// TO DO Сделать закрытие дочерних окон вперед закрытия основного окна
    }

    #endregion
}
