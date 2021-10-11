using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Map")]
public class LabyrinthScriptableObject : ScriptableObject
{
    public int width;
    public int height;
    public bool[] map;
}
