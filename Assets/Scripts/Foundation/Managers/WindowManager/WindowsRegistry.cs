using UnityEngine;
using Foundation;


[CreateAssetMenu(fileName = "Windows_Registry", menuName = "Manager/Windows Registry")]

public class WindowsRegistry : Registry
{
#if UNITY_EDITOR
    public override void Sync()
    {
        DebugOnly.Message(string.Format("<color=yellow>Window registry sync started, please wait for confirmation message...</color>"));
        base.Sync();

        foreach (var reg in registry)
            DebugOnly.Message(string.Format("<color=green>Added to registry: {0} - {1}</color>", reg.Id, reg.Path));

        DebugOnly.Message(string.Format("<color=green>Registry successfully synced!</color>"));
    }
#endif
}


