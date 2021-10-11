namespace Foundation
{
    public interface ISceneManager
    {
        ObserverList<IOnBeginSceneLoad> OnBeginSceneLoad { get; }
        ObserverList<IOnSceneLoadProgress> OnSceneLoadProgress { get; }
        ObserverList<IOnEndSceneLoad> OnEndSceneLoad { get; }

        void LoadSceneAsync(string sceneName);
    }
}
