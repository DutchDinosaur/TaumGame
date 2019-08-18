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

        for (int y = 0; y < ySize; y++)
        {
            for (int x = 0; x < xSize; x++)
            {
                if (terrainMap.GetPixel(x,y).b > 0)
                {
                    ////get heights of the 4 corners
                    //float[] Heights = { getHeight(x, y), getHeight(x + 1, y), getHeight(x, y + 1), getHeight(x + 1, y + 1) };
                    //float maxHeight = getMaxFloat(Heights);
                    //float minHeight = getMinFloat(Heights);

                    //float[] VertexHeights;

                    //bool[] SurroundingCliffs = getIfSurroundingPixels(x, y);

                    //List<Vector3> vertPosses = new List<Vector3>();

                    //for (int q = 0; q < 4; q++)
                    //{

                    //}

                    ////quad 0
                    //vertPosses.Add(getVertexPosInClockwiseQuad(x, y, Heights[0], 0, new Vector2(.5f, .5f), new Vector2(0, 0))); //0
                    //vertPosses.Add(getVertexPosInClockwiseQuad(x, y, Heights[0], 1, new Vector2(.5f, .5f), new Vector2(0, 0))); //1
                    //vertPosses.Add(getVertexPosInClockwiseQuad(x, y, Heights[0], 2, new Vector2(.5f, .5f), new Vector2(0, 0))); //2
                    //vertPosses.Add(getVertexPosInClockwiseQuad(x, y, Heights[0], 3, new Vector2(.5f, .5f), new Vector2(0, 0))); //3

                    //triangles.Add(vert + 0);
                    //triangles.Add(vert + 1);
                    //triangles.Add(vert + 3);
                    //triangles.Add(vert + 1);
                    //triangles.Add(vert + 2);
                    //triangles.Add(vert + 3);

                    ////quad 1
                    //vertPosses.Add(getVertexPosInClockwiseQuad(x, y, Heights[1], 0, new Vector2(.5f, .5f), new Vector2(0, .5f))); //4
                    //vertPosses.Add(getVertexPosInClockwiseQuad(x, y, Heights[1], 1, new Vector2(.5f, .5f), new Vector2(0, .5f))); //5
                    //vertPosses.Add(getVertexPosInClockwiseQuad(x, y, Heights[1], 2, new Vector2(.5f, .5f), new Vector2(0, .5f))); //6
                    //vertPosses.Add(getVertexPosInClockwiseQuad(x, y, Heights[1], 3, new Vector2(.5f, .5f), new Vector2(0, .5f))); //7

                    //triangles.Add(vert + 0 + 4);
                    //triangles.Add(vert + 1 + 4);
                    //triangles.Add(vert + 3 + 4);
                    //triangles.Add(vert + 1 + 4);
                    //triangles.Add(vert + 2 + 4);
                    //triangles.Add(vert + 3 + 4);

                    ////quad 2
                    //vertPosses.Add(getVertexPosInClockwiseQuad(x, y, Heights[2], 0, new Vector2(.5f, .5f), new Vector2(.5f, .5f))); //8
                    //vertPosses.Add(getVertexPosInClockwiseQuad(x, y, Heights[2], 1, new Vector2(.5f, .5f), new Vector2(.5f, .5f))); //9
                    //vertPosses.Add(getVertexPosInClockwiseQuad(x, y, Heights[2], 2, new Vector2(.5f, .5f), new Vector2(.5f, .5f))); //10
                    //vertPosses.Add(getVertexPosInClockwiseQuad(x, y, Heights[2], 3, new Vector2(.5f, .5f), new Vector2(.5f, .5f))); //11

                    //triangles.Add(vert + 0 + 8);
                    //triangles.Add(vert + 1 + 8);
                    //triangles.Add(vert + 3 + 8);
                    //triangles.Add(vert + 1 + 8);
                    //triangles.Add(vert + 2 + 8);
                    //triangles.Add(vert + 3 + 8);

                    ////quad 3
                    //vertPosses.Add(getVertexPosInClockwiseQuad(x, y, Heights[3], 0, new Vector2(.5f, .5f), new Vector2(.5f, 0))); //12
                    //vertPosses.Add(getVertexPosInClockwiseQuad(x, y, Heights[3], 1, new Vector2(.5f, .5f), new Vector2(.5f, 0))); //13
                    //vertPosses.Add(getVertexPosInClockwiseQuad(x, y, Heights[3], 2, new Vector2(.5f, .5f), new Vector2(.5f, 0))); //14
                    //vertPosses.Add(getVertexPosInClockwiseQuad(x, y, Heights[3], 3, new Vector2(.5f, .5f), new Vector2(.5f, 0))); //15

                    //triangles.Add(vert + 0 + 12);
                    //triangles.Add(vert + 1 + 12);
                    //triangles.Add(vert + 3 + 12);
                    //triangles.Add(vert + 1 + 12);
                    //triangles.Add(vert + 2 + 12);
                    //triangles.Add(vert + 3 + 12);

                    //vert += 16;
                    
                }
                else
                {
                    //
                    //get heights of the 4 corners
                    float[] Heights = { getHeight(x, y), getHeight(x, y + 1), getHeight(x + 1, y + 1), getHeight(x + 1, y) };
                    //generate Vertecies
                    for (int v = 0; v < 4; v++)
                    {
                        vertices.Add(getVertexPosInClockwiseQuad(x,y,Heights[v],v,new Vector2(1,1),Vector2.zero));
                    }

                    //generate tri's
                    triangles.Add(vert + 0);
                    triangles.Add(vert + 1);
                    triangles.Add(vert + 3);
                    triangles.Add(vert + 1);
                    triangles.Add(vert + 2);
                    triangles.Add(vert + 3);

                    //generate uv's
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

    float getMaxFloat(float[] floats)
    {
        float maxFloat = floats[0];
        foreach (float f in floats)
        {
            if (maxFloat < f)
            {
                maxFloat = f;
            }
        }
        return maxFloat;
    }

    float getMinFloat(float[] floats)
    {
        float maxFloat = floats[0];
        foreach (float f in floats)
        {
            if (maxFloat > f)
            {
                maxFloat = f;
            }
        }
        return maxFloat;
    }

    bool[] getIfSurroundingPixels(int x, int y)
    {
        bool[] pixels = new bool[4];
        if (terrainMap.GetPixel(x, y + 1).b > 0)
        {
            pixels[0] = true;
        }
        if (terrainMap.GetPixel(x - 1, y).b > 0)
        {
            pixels[0] = true;
        }
        if (terrainMap.GetPixel(x, y + 1).b > 0)
        {
            pixels[0] = true;
        }
        if (terrainMap.GetPixel(x + 1, y).b > 0)
        {
            pixels[0] = true;
        }

        return pixels;
    }

    Vector3 getVertexPosInClockwiseQuad(int x, int y, float cornerHeight, int cornerIndex, Vector2 size, Vector2 offset)
    {
        Vector2 VertexPos = Vector2.zero;
        switch (cornerIndex)
        {
            case 0:
                VertexPos = new Vector2(x, y);
                break;
            case 1:
                VertexPos = new Vector2(x, y + 1);
                break;
            case 2:
                VertexPos = new Vector2(x + 1, y + 1);
                break;
            case 3:
                VertexPos = new Vector2(x + 1, y);
                break;
        }
        return new Vector3(size.x * VertexPos.x, size.y * VertexPos.y, cornerHeight) + new Vector3(offset.x - xSize / 2, offset.y - ySize / 2, 0);
    }

    void generateQuadUVs(Vector2 pos1, Vector2 pos2)
    {
        uvs.Add(new Vector2(pos1.x, pos1.y));
        uvs.Add(new Vector2(pos1.x, pos2.y));
        uvs.Add(new Vector2(pos2.x, pos2.y));
        uvs.Add(new Vector2(pos2.x, pos1.y));
    }
}