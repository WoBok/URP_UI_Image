using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Polygon : MonoBehaviour
{
    public int sideCount = 3;
    public float radius = 0.5f;
    void Start()
    {
        Render();
    }

    public void Render()
    {
        if (GetComponent<MeshRenderer>().sharedMaterial == null)
            GetComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Universal Render Pipeline/Lit"));

        var mesh = GetComponent<MeshFilter>().sharedMesh = new Mesh();
        GetComponent<MeshFilter>().sharedMesh.name = "Polygon";

        //Vertices
        var vertices = new Vector3[sideCount + 1];
        var angle = 360.0f / sideCount * Mathf.Deg2Rad;
        var currentAngle = 0.0f;
        float x;
        float z;
        for (int i = 0; i < sideCount; i++)
        {
            x = radius * Mathf.Cos(currentAngle);
            z = radius * Mathf.Sin(currentAngle);
            vertices[i] = new Vector3(x, 0, z);
            currentAngle += angle;
        }
        vertices[sideCount] = Vector3.zero;
        mesh.vertices = vertices;

        //Triangles
        var triangles = new List<int>();
        for (int i = 0; i < sideCount; i++)
        {
            triangles.Add(i);
            triangles.Add(sideCount);
            triangles.Add((i + 1) % sideCount);
        }
        mesh.triangles = triangles.ToArray();

        //uv
        currentAngle = 0;
        var uv = new Vector2[sideCount + 1];
        for (int i = 0; i < sideCount; i++)
        {
            x = Mathf.Cos(currentAngle) / 2;
            z = Mathf.Sin(currentAngle) / 2;
            uv[i] = new Vector2(x, z) + Vector2.one * 0.5f;
            currentAngle += angle;
        }
        uv[sideCount] = Vector2.one * 0.5f;
        mesh.uv = uv;

        //normals
        var normals = new Vector3[sideCount + 1];
        for (int i = 0; i < vertices.Length; i++)
            normals[i] = Vector3.up;
        mesh.normals = normals;
    }
}