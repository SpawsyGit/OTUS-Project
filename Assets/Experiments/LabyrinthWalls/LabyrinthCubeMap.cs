using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabyrinthCubeMap : MonoBehaviour
{
    void Start()
    {
        var cubes = GetComponentsInChildren<LabyrinthCube>();
        foreach (var cube in cubes) {
            var pos = cube.transform.position;
            Debug.Log($"{pos.x} {pos.y} {pos.z}");
        }
    }
}
