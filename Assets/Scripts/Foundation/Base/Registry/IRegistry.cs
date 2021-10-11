
public interface IRegistry
{
    string Resolve(string id);
    void Sync();
}
