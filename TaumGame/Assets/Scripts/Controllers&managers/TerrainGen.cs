using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class TerrainGen : MonoBehaviour{

    Mesh mesh;

    List<Vector3> vertices;
    List<int> triangles;
    List<Vector2> uvs;

    public Texture2D terrainMap;

    private int xSize;

    private int ySize;

    [SerializeField]
    private int heightScale = 1;
    

    void Start()
    {
        xSize = terrainMap.width;
        ySize = terrainMap.height;

        mesh = new Mesh();

        GetComponent<MeshFilter>().mesh = mesh;
        if (GetComponent<MeshCollider>() != null)
        {
            GetComponent<MeshCollider>().sharedMesh = mesh;
        }

        CreateShape();
        UpdateMesh();

        if (GetComponent<MeshCollider>() != null)
        {
            GetComponent<MeshCollider>().sharedMesh = mesh;
        }
    }

    void CreateShape()
    {
        vertices = new List<Vector3>();
        uvs = new List<Vector2>();
        triangles = new List<int>();

        int vert = 0;

        for (int i = 0, y = 0; y < ySize; y++)
        {
            for (int x = 0; x < xSize; x++)
            {
                float z = getHeight(x,y);
                vertices.Add(new Vector3(x - xSize / 2, y - ySize / 2, z));

                z = getHeight(x + 1, y);
                vertices.Add(new Vector3(x + 1 - xSize / 2, y - ySize / 2, z));

                z = getHeight(x, y + 1);
                vertices.Add(new Vector3(x - xSize / 2, y + 1 - ySize / 2, z));

                z = getHeight(x + 1, y + 1);
                vertices.Add(new Vector3(x + 1 - xSize / 2, y + 1 - ySize / 2, z));

                triangles.Add(vert + 0);
                triangles.Add(vert + 2);
                triangles.Add(vert + 1);
                triangles.Add(vert + 1);
                triangles.Add(vert + 2);
                triangles.Add(vert + 3);

                switch (getTex(x,y))
                {
                    case 0:
                        generateUVs(0,0,.5f,.5f);
                        break;
                    case 1:
                        generateUVs(.5f, 0, 1, .5f);
                        break;
                    case 2:
                        generateUVs(.5f, .5f, 1, 1);
                        break;
                    case 3:
                        generateUVs(0,.5f,.5f,1);
                        break;
                }

                vert += 4;
                i++;
            }
        }
    }

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uvs.ToArray();

        mesh.RecalculateNormals();
    }

    float getHeight(int x, int y)
    {
        return -terrainMap.GetPixel(x, y).g * heightScale;
    }

    int getTex(int x,int y)
    {
        float r = terrainMap.GetPixel(x, y).r;

        if (r > 0 && r < .5f)
        {
            return 1;
        }
        else if (r >= .5f && r < 1)
        {
            return 2;
        }
        else if (r == 1)
        {
            return 3;
        }
        else return 0;
    }

    void generateUVs(float x1, float y1, float x2, float y2)
    {
        uvs.Add(new Vector2(x1, y1));
        uvs.Add(new Vector2(x2, y1));
        uvs.Add(new Vector2(x1, y2));
        uvs.Add(new Vector2(x2, y2));
    }
}