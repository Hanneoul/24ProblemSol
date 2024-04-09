using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(StaticMeshGen))]
public class StaticMeshGenEditor : Editor
{
    //버튼만들기 예제
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        StaticMeshGen script = (StaticMeshGen)target;

        if (GUILayout.Button("Generate Mesh"))
        {
            script.GenerateMesh();
        }

    }
}

//메쉬만들기 예제
public class StaticMeshGen : MonoBehaviour
{
    // Start is called before the first frame update
    public void GenerateMesh()
    {
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[]
        {
            new Vector3 (0.0f, 0.0f, 0.0f),
            new Vector3 (1.0f, 0.0f, 0.0f),
            new Vector3 (1.0f, 1.0f, 0.0f),
        };
        Vector3[] normals = new Vector3[]
        {
            new Vector3 (0.0f, 0.0f, -1.0f),
            new Vector3 (0.0f, 0.0f, -1.0f),
            new Vector3 (0.0f, 0.0f, -1.0f),
        };
        int[] triangleIndices = new int[]
        {
            0,2,1,
        };

        mesh.vertices = vertices;
        mesh.normals = normals;
        mesh.triangles = triangleIndices;

        MeshFilter mf = this.AddComponent<MeshFilter>();
        mf.mesh = mesh;

        MeshRenderer mr = this.AddComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
