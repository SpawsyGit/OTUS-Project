using Zenject;

namespace Foundation
{
    public abstract class AbstractManager<T> : MonoInstaller
        where T : class
    {
        public override void InstallBindings()
        {
            Container.Bind<T>().FromInstance(this as T);
        }
    }
}
