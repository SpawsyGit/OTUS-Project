using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LabyrinthScript : MonoBehaviour
{
    public Tilemap tilemap;
    [SerializeField] int[] flags; // List<Vector2>

    void Start()
    {
        var bounds = tilemap.cellBounds;
        string str = "";
        for (int y = bounds.max.y; y >= bounds.min.y; y--) {
            for (int x = bounds.min.x; x < bounds.max.x; x++) {
                str += tilemap.GetTile(new Vector3Int(x, y, 0)) != null ? "+" : "-";
            }
            str += "\n";
        }
        Debug.Log(str);
    }

    /*
#if UNITY_EDITOR
    [UnityEditor.Callbacks.PostProcessScene(0)]
    static void OnProcessScene()
    {
        foreach (var script in FindObjectsOfType<LabyrinthScript>()) {
            script.flags = new int[w * h];
            // loop to fill flags
            DestroyImmediate(tilemap.gameObject);
        }
    }
#endif
    */
}
