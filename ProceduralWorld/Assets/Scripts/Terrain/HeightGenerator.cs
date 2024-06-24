using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightGenerator : MonoBehaviour
{
    public int height = 100;
    public float scale = 20f;
    public float heightMultiplier = 10f;

    // Offset values to ensure continuity between chunks
    public float OffsetX
    {
        get { return (generator.chunkSize -1) * chunkManager.ChunkCord.y; }
    }
    public float OffsetY
    {
        get { return (generator.chunkSize - 1) * chunkManager.ChunkCord.x; }
    }

    [SerializeField] private TerrainGenerator generator;
    private ChunkManager chunkManager;

    void Awake()
    {
        chunkManager = GetComponent<ChunkManager>();
        //GenerateTerrain();
    }

    /// <summary>
    /// Generates the terrain
    /// </summary>
    public void GenerateTerrain()
    {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrainData();
    }

    /// <summary>
    /// Generates the terain data
    /// </summary>
    /// <returns>terrain data according to its coordinates</returns>
    TerrainData GenerateTerrainData()
    {
        TerrainData terrainData = new TerrainData(); // Create a new TerrainData instance
        int resolution = generator.chunkSize;
        terrainData.heightmapResolution = resolution + 1; // Heightmap resolution must be resolution + 1
        terrainData.size = new Vector3(generator.chunkSize, height, generator.chunkSize);

        terrainData.SetHeights(0, 0, GenerateHeights(resolution));
        return terrainData;
    }

    /// <summary>
    /// Generates the height array
    /// </summary>
    /// <param name="resolution"></param>
    /// <returns>height array</returns>
    float[,] GenerateHeights(int resolution)
    {
        Debug.Log("OFF SET: " + OffsetX + " " + OffsetY);
        float[,] heights = new float[resolution, resolution];
        for (int x = 0; x < resolution; x++)
        {
            for (int y = 0; y < resolution; y++)
            {
                float xCoord = (float)((x + OffsetX) / scale);
                float yCoord = (float)((y + OffsetY) / scale);

                float perlinValue = Mathf.PerlinNoise(xCoord, yCoord);
                float heightValue = perlinValue * heightMultiplier / height;
                heights[x, y] = heightValue;
            }
        }
        return heights;
    }
}
