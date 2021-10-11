using System.Collections.Generic;
using System.Threading.Tasks;
using Foundation;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Foundation
{
    public class WindowManager : AbstractManager<IWindowManager>, IWindowManager
    {
        #region Fields

        private WindowsRegistry registry;

        private readonly Stack<IWindow> _windows = new Stack<IWindow>();

        private Transform _root;


        #endregion

        #region Unity handlers

        public override void OnAwake()
        {
            registry = Resources.Load<WindowsRegistry>("Windows/Windows_Registry");
            if (registry == null)
                DebugOnly.Warn("Cannot find registry file!");
            registry.Sync();

            UnityEngine.SceneManagement.SceneManager.activeSceneChanged += OnActiveSceneChanged;
        }
        #endregion

        #region Methods


        private void OnActiveSceneChanged(Scene current, Scene next)
        {
            while (_windows.Count > 0)
                Destroy(_windows.Pop().WindowObject);

            var uiRoot = GameObject.FindWithTag("UIRoot");

            if (uiRoot is null)
            {
                DebugOnly.Warn("Cannot find [UIRoot] tagged GameObject");
                return;
            }

            _root = uiRoot.transform;
        }

        public IWindow OpenWindow(string windowId, object data = null)
        {
            var window = CreateWindow(windowId);

            return window is null ? null : OpenWindow(window, data);
        }

        private IWindow OpenWindow(IWindow window, object data = null)
        {
            _windows.Push(window);

            DoOpen(window, data);

            return window;
        }

        public void CloseWindow()
        {
            if (_windows.Count > 0)
                CloseWindow(_windows.Peek());
        }


        public void CloseWindow(IWindow window)
        {
            if (!Has(window))
                return;

            CloseChildWindows(window);

            window.Close();
        }

        private void CloseChildWindows(IWindow window)
        {
            if (!Has(window))
                return;

            while (_windows.Count > 0)
            {
                var top = _windows.Pop();

                if (top == window)
                    break;

                DoClose(top);
            }
        }

        private void DoOpen(IWindow window, object data = null)
        {
            window.OnClosed += obj =>
            {
                Destroy(window.WindowObject);
                CloseChildWindows(window);
            };
            window.Open(data);
        }

        private void DoClose(IWindow window)
        {
            window.Close();
        }

        private IWindow CreateWindow(string windowId)
        {
            var prefab = GetWindowPrefab(windowId);

            if (prefab is null)
                return null;

            var window = prefab.Create(_root);
            window.WindowObject.name = prefab.WindowObject.name;
            window.WindowObject.SetActive(false);

            return window;
        }
        private IWindow GetWindowPrefab(string windowId)
        {

            var path = registry.Resolve(windowId);

            if (path is null)
            {
                DebugOnly.Warn(string.Format("Window with id {0} has not been found in windows registry, check window id or update windows registry", windowId));
                return null;
            }

            var prefab = Resources.Load<Window>(path);
            if (prefab is null)
            {
                DebugOnly.Warn(string.Format("Window prefab with id {0} has not been found in registry folder by path {1}", windowId, path));
                return null;
            }

            return prefab;
        }

        private bool Has(IWindow window)
        {
            return _windows.Contains(window);
        }
        #endregion
    }
}

