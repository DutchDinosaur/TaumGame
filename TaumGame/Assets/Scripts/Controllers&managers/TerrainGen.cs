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
                if (terrainMap.GetPixel(x,y).b > 0)
                {
                    ////get heights of the 4 corners
                    //float[] Heights = { getHeight(x, y), getHeight(x + 1, y), getHeight(x, y + 1), getHeight(x + 1, y + 1) };

                    //Vector3[] vertPosses = { new Vector3(x - xSize / 2, y - ySize / 2, Heights[0]), new Vector3(x + 1 - xSize / 2, y - ySize / 2, Heights[1]), new Vector3(x - xSize / 2, y + 1 - ySize / 2, Heights[2]), new Vector3(x + 1 - xSize / 2, y + 1 - ySize / 2, Heights[3]) };

                    //foreach (Vector3 pos in vertPosses)
                    //{
                    //    vertices.Add(pos);
                    //}


                    //vert += vertPosses.Length;
                    i++;
                }
                else
                {
                    //get heights of the 4 corners
                    //float[] Heights = { getHeight(x, y), getHeight(x + 1, y), getHeight(x, y + 1), getHeight(x + 1, y + 1) };
                    float[] Heights = { getHeight(x, y), getHeight(x, y + 1), getHeight(x + 1, y + 1), getHeight(x + 1, y) };
                    //Vector3[] vertPosses = { new Vector3(x - xSize / 2, y - ySize / 2, Heights[0]), new Vector3(x + 1 - xSize / 2, y - ySize / 2, Heights[1]), new Vector3(x - xSize / 2, y + 1 - ySize / 2, Heights[2]), new Vector3(x + 1 - xSize / 2, y + 1 - ySize / 2, Heights[3]) };

                    //foreach (Vector3 pos in vertPosses)
                    //{
                    //    vertices.Add(pos);
                    //}

                    for (int v = 0; v < 4; v++)
                    {
                        vertices.Add(getVertexPosInClockwiseQuad(x,y,Heights[v],v));
                    }

                    //float z = getHeight(x, y);
                    //vertices.Add(new Vector3(x - xSize / 2, y - ySize / 2, z));

                    //z = getHeight(x + 1, y);
                    //vertices.Add(new Vector3(x + 1 - xSize / 2, y - ySize / 2, z));

                    //z = getHeight(x, y + 1);
                    //vertices.Add(new Vector3(x - xSize / 2, y + 1 - ySize / 2, z));

                    //z = getHeight(x + 1, y + 1);
                    //vertices.Add(new Vector3(x + 1 - xSize / 2, y + 1 - ySize / 2, z));

                    triangles.Add(vert + 0);
                    triangles.Add(vert + 1);
                    triangles.Add(vert + 3);
                    triangles.Add(vert + 1);
                    triangles.Add(vert + 2);
                    triangles.Add(vert + 3);

                    switch (getTex(x, y))
                    {
                        case 0:
                            generateQuadUVs(new Vector2(0, 0), new Vector2(.5f, .5f));
                            break;
                        case 1:
                            generateQuadUVs(new Vector2(.5f, 0), new Vector2(1, .5f));
                            break;
                        case 2:
                            generateQuadUVs(new Vector2(0, .5f), new Vector2(.5f, 1));
                            break;
                        case 3:
                            generateQuadUVs(new Vector2(.5f, .5f), new Vector2(1, 1));
                            break;
                    }

                    vert += 4;
                    i++;
                }
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
            return 1; //grass 2
        }
        else if (r >= .5f && r < 1)
        {
            return 2; //rock
        }
        else if (r == 1)
        {
            return 3; //rock2
        }
        else return 0; //grass
    }

    Vector3 getVertexPosInClockwiseQuad(int x, int y, float cornerHeight, int cornerIndex)
    {
        switch (cornerIndex)
        {
            case 0:
                return new Vector3(x - xSize / 2, y - ySize / 2, cornerHeight);
            case 1:
                return new Vector3(x - xSize / 2, y + 1 - ySize / 2, cornerHeight);
            case 2:
                return new Vector3(x + 1 - xSize / 2, y + 1 - ySize / 2, cornerHeight);
            case 3:
                return new Vector3(x + 1 - xSize / 2, y - ySize / 2, cornerHeight);
        }
        return Vector3.zero;
    }

    void generateQuadUVs(Vector2 pos1, Vector2 pos2)
    {
        uvs.Add(new Vector2(pos1.x, pos1.y));
        uvs.Add(new Vector2(pos1.x, pos2.y));
        uvs.Add(new Vector2(pos2.x, pos2.y));
        uvs.Add(new Vector2(pos2.x, pos1.y));
    }
}