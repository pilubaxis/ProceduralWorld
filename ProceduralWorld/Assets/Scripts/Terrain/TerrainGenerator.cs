using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [Header("Chunks")]
    [SerializeField] private ChunkManager[] chunks = new ChunkManager[9]; // 3x3 grid
    public int chunkSize = 100;
    [SerializeField] private Vector2Int middleChunk = new Vector2Int(0, 0);

    [Header("Player")]
    [SerializeField] Transform playertransform = null;
    public Vector2Int playerChunkCheck = Vector2Int.zero;


    public Vector2Int PlayerCurrentChunk
    {
        get { return playerCurrentChunk; } 
        
        set {
            Vector2Int direction = value - playerCurrentChunk;
            playerCurrentChunk = value;
            UpdateChunkCoord(direction);
        }
    }
    private Vector2Int playerCurrentChunk = Vector2Int.zero;

    //refs
    private ObjectsManager objectsmanager = null;


    void Start()
    {
        objectsmanager = GetComponent<ObjectsManager>();
        SetUpChunks();
    }

    // Update is called once per frame
    void Update()
    {
        playerChunkCheck = new Vector2Int(Mathf.FloorToInt(playertransform.position.x / chunkSize), Mathf.FloorToInt(playertransform.position.z / chunkSize));

        if (PlayerCurrentChunk != playerChunkCheck)
        {
            PlayerCurrentChunk = playerChunkCheck;
        }
    }


    /// <summary>
    /// Set up the terrain with its parameters
    /// </summary>
    public void SetUpChunks()
    {
        int gridSize = 3;
        int halfGrid = gridSize / 2;

        for (int i = 0; i < chunks.Length; i++)
        {
            if (chunks[i] == null) continue;

            // Calculate the chunk's grid position in a 3x3 grid
            int row = i / gridSize;
            int col = i % gridSize;

            // Adjust position relative to the middleChunk
            Vector2Int chunkPosition = new Vector2Int(middleChunk.x + (col - halfGrid), middleChunk.y + (row - halfGrid));

            // Set the chunk's position in the world
            chunks[i].ChunkCord = new Vector2Int(chunkPosition.x, chunkPosition.y);
            chunks[i].GetComponent<HeightGenerator>().GenerateTerrain();
            objectsmanager.PopulateChunk(chunks[i]);
        }
    }

    /// <summary>
    /// Update the chunks coordinates
    /// </summary>
    /// <param name="direction"></param>
    public void UpdateChunkCoord(Vector2Int direction)
    {
        Vector2Int cordToDestroy = middleChunk - direction;

        middleChunk += direction;

        for (int i = 0; i < chunks.Length; i++)
        {
            if (direction.x != 0 && chunks[i].ChunkCord.x == cordToDestroy.x)
            {
                objectsmanager.UnpopulateChunk(chunks[i]);

                chunks[i].ChunkCord += direction * 3;
                chunks[i].GetComponent<HeightGenerator>().GenerateTerrain();

                objectsmanager.PopulateChunk(chunks[i]);
            }
            else if (direction.y != 0 && chunks[i].ChunkCord.y == cordToDestroy.y)
            {
                objectsmanager.UnpopulateChunk(chunks[i]);

                chunks[i].ChunkCord += direction * 3;
                chunks[i].GetComponent<HeightGenerator>().GenerateTerrain();

                objectsmanager.PopulateChunk(chunks[i]);
            }
        }
    }
}
