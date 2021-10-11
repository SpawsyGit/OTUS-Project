using System;
using UnityEngine;

public interface IWindow
{
    GameObject WindowObject { get; }
    public Action<object> OnClosed { get; set; }
    void Open(object data = null);
    void Close();
    IWindow Create(Transform root);
}
