using System;

[Serializable]
public class RegInfo
{
    public string Id;
    public string Path;

    public RegInfo(string id, string path)
    {
        Id = id;
        Path = path;
    }
}