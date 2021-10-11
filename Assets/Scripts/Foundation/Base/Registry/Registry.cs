using Foundation;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public abstract class Registry : ScriptableObject, IRegistry
{
    #region Fields

    [SerializeField]
    public Object folder;

    [SerializeField]
    public List<RegInfo> registry = new List<RegInfo>();


    [SerializeField] private string searchPattern = "*";

    #endregion

    #region Methods


    public string Resolve(string id)
    {
        return registry.FirstOrDefault(reg => reg.Id == id)?.Path;
    }

    public virtual void Sync()
    {
        registry.Clear();
        //var path = AssetDatabase.GetAssetPath(folder);
        var path = "Assets/Resources/Windows/Prefabs";
        if (string.IsNullOrEmpty(path))
        {
            Debug.LogFormat("<color=red>Specify folder to scan</color>");
            return;
        }
       
        var cache = new Dictionary<string, bool>();
        var files = Directory.GetFiles(path, searchPattern, SearchOption.AllDirectories);
        var resourcesFolder = "Assets/Resources";
        foreach (var file in files)
        {
            var resourceId = Path.GetFileNameWithoutExtension(file);
            var directory = Path.GetDirectoryName(file) ?? "";

            if (path.Length < resourcesFolder.Length || !path.Substring(0, resourcesFolder.Length).Equals(resourcesFolder))
            {
                DebugOnly.Warn(string.Format("<color=red>Windows folder must be in \"{0}\", specified path: {1}</color>", resourcesFolder, path));
                return;
            }


            directory = directory.Remove(0, resourcesFolder.Length + 1);

            if (string.IsNullOrEmpty(directory))
            {
                DebugOnly.Warn(string.Format("<color=red>Invalid directory: {0}</color>", file));
                continue;
            }

            var resourcePath = Path.Combine(directory, resourceId);

            if (cache.ContainsKey(resourceId))
            {
                DebugOnly.Warn(string.Format("<color=red>Unique resource id constraint: {0} - {1}</color>", file, registry.Find(r => r.Id == resourceId).Path));
                continue;
            }

            cache.Add(resourceId, true);
            registry.Add(new RegInfo(resourceId, resourcePath));
        }
        EditorUtility.SetDirty(this);
        Debug.Log("asdasd");
        AssetDatabase.SaveAssets();
        
    }


    #endregion
}
