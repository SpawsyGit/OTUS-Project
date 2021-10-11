#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LabyrinthEditor : EditorWindow
{
    int width;
    int height;
    HashSet<Vector2Int> flags = new HashSet<Vector2Int>();
    LabyrinthScriptableObject obj;

    private void OnGUI()
    {
        width = EditorGUILayout.IntField("width", width);
        height = EditorGUILayout.IntField("height", height);

        for (int y = 0; y < height; y++) {
            EditorGUILayout.BeginHorizontal();
            for (int x = 0; x < width; x++) {
                bool value = flags.Contains(new Vector2Int(x, y));
                bool newValue = EditorGUILayout.Toggle(value);
                if (value != newValue) {
                    if (!newValue)
                        flags.Remove(new Vector2Int(x, y));
                    else
                        flags.Add(new Vector2Int(x, y));
                }
            }
            EditorGUILayout.EndHorizontal();
        }

        obj = (LabyrinthScriptableObject)EditorGUILayout.ObjectField(obj, typeof(LabyrinthScriptableObject), false);
        if (GUILayout.Button("Save")) {
            obj.width = width;
            obj.height = height;
            obj.map = new bool[width * height];
            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++)
                    obj.map[y * width + x] = flags.Contains(new Vector2Int(x, y));
            }
            EditorUtility.SetDirty(obj);
        }
    }

    [MenuItem("Tools/Editor")]
    static void OpenEditor()
    {
        EditorWindow.GetWindow<LabyrinthEditor>();
    }
}
#endif
