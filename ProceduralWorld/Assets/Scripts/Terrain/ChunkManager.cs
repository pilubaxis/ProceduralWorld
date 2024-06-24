using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    public Vector2Int ChunkCord {
        get { return chunkCord; }
        set {
            SetChunkPosition(value);
            chunkCord = value; 
        }
    }
    private Vector2Int chunkCord = new Vector2Int();

    // References
    [SerializeField] private TerrainGenerator genarator;

    /// <summary>
    /// Set the Chunk Size
    /// </summary>
    /// <param name="size"></param>
    public void SetChunkSize(int size)
    {
        Terrain terrain = GetComponent<Terrain>();
        if (terrain != null)
        {
            terrain.terrainData.size = new Vector3(size, terrain.terrainData.size.y, size);
        }
    }

    /// <summary>
    /// Update the chunk position in the scene based on it coridates
    /// </summary>
    public void SetChunkPosition(Vector2Int cord)
    {
        int chunkSize = genarator.chunkSize;

        transform.position = new Vector3(cord.x * chunkSize, 0, cord.y * chunkSize);
    }
}
