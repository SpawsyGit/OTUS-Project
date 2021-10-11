namespace Foundation
{
    public interface IWindowManager
    {
        IWindow OpenWindow(string windowId, object data = null);
        public void CloseWindow();
        public void CloseWindow(IWindow window);
    }
}