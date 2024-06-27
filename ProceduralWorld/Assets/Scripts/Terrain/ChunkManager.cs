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

    /// <summary>
    /// Chunk world position min limit
    /// </summary>
    public Vector3 ChunkMinLimit
    {
        get { return new Vector3(ChunkCord.x * genarator.chunkSize, 0, ChunkCord.y * genarator.chunkSize); }
    }

    /// <summary>
    /// Chunk world position max limit
    /// </summary>
    public Vector3 ChunkMaxLimit
    {
        get { return new Vector3((ChunkCord.x + 1)* genarator.chunkSize, 0, (ChunkCord.y  + 1) * genarator.chunkSize); }
    }

    /// <summary>
    /// Objects the chunk possess
    /// </summary>
    public List<List<GameObject>> chunkObjects = new List<List<GameObject>>();

    private Vector2Int chunkCord = new Vector2Int();

    // References
    [SerializeField] private TerrainGenerator genarator;
    [SerializeField] private ObjectsManager objectsMan;


    //private void Start()
    //{
    //    for (int i = 0; i < objectsMan.objects.Count; i++)
    //    {
    //        chunkObjects.Add(new List<GameObject>());
    //    }
    //}

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
