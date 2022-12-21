using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brush : MonoBehaviour
{
    public Terrain terrain;
    public float strength = 0.01f;
    int heightmapWidth;
    int heightmapHeight;
    float[,] heights;
    TerrainData terrainData;
    // Start is called before the first frame update
    void Start()
    {
        terrainData = terrain.terrainData;
        heightmapWidth = terrainData.heightmapResolution;
        heightmapHeight = terrainData.heightmapResolution;
        heights = terrainData.GetHeights(0, 0, heightmapWidth, heightmapHeight);
        
    }



    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(ray,out hit))
            {
                RaiseTerrain(hit.point);
            }
        }
        if (Input.GetMouseButton(1))
        {
            if (Physics.Raycast(ray, out hit))
            {
                LowerTerrain(hit.point);
            }
        }
    }
    void RaiseTerrain(Vector3 point)
    {
        int mouseX = (int)((point.x / terrainData.size.x) * heightmapWidth);
        int mouseZ = (int)((point.z / terrainData.size.z) * heightmapHeight);

        float[,] modifieldHeights = new float[1, 1];
        float y = heights[mouseX, mouseZ];
        y += strength + Time.deltaTime;

        if (y>terrainData.size.y)
        {
            y = terrainData.size.y;
        }
        modifieldHeights[0, 0] = y;
        heights[mouseX, mouseZ] = y;
        terrainData.SetHeights(mouseX, mouseZ, modifieldHeights);

    }
    void LowerTerrain(Vector3 point)
    {
        int mouseX = (int)((point.x / terrainData.size.x) * heightmapWidth);
        int mouseZ = (int)((point.z / terrainData.size.z) * heightmapHeight);

        float[,] modifieldHeights = new float[1, 1];
        float y = heights[mouseX, mouseZ];
        y -= strength + Time.deltaTime;

        if (y<0)
        {
            y = 0;
        }
        modifieldHeights[0, 0] = y;
        heights[mouseX, mouseZ] = y;
        terrainData.SetHeights(mouseX, mouseZ, modifieldHeights);
    }

}
