using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LabyrinthCube : MonoBehaviour
{
#if UNITY_EDITOR
    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Floor(pos.x);
        pos.y = Mathf.Floor(pos.y);
        pos.z = Mathf.Floor(pos.z);
        transform.position = pos;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(1, 1, 1));
    }
#endif
}
