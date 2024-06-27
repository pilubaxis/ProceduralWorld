using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using static UnityEngine.UI.Image;

public class ObjectsManager : MonoBehaviour
{
    public List<ObjectInfo> objects = new List<ObjectInfo>();
    [SerializeField] private List<Queue<GameObject>> objectsPool = new List<Queue<GameObject>>();
    [SerializeField] private Vector3 poolLocation = Vector3.zero;
    [SerializeField] private Transform objectsParent= null;
    [SerializeField] private LayerMask groundLayer;
    void Start()
    {
        InstantiateAllGameObjects();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Initialy instatiate all objects in the pool
    /// </summary>
    public void InstantiateAllGameObjects()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            for (int j = 0; j < objects[i].qntToInstantiate; j++)
            {
                objectsPool.Add(new Queue<GameObject>());
                objectsPool[i].Enqueue( Instantiate(objects[i].prefab, poolLocation, Quaternion.identity ,objectsParent));
            }
        }
    }

    /// <summary>
    /// Populates the chunk with objects
    /// </summary>
    /// <param name="chunk"></param>
    public void PopulateChunk(ChunkManager chunk)
    {
        for (int i = 0; i < objects.Count; i++)
        {
            //Check if chunk will have this object
            if (!objects[i].ChunkShouldHaveObject()) continue;

            int objectQnt = objects[i].QuantityToHaveInChunk();
            Debug.Log("Object " + objects[i].prefab.name + " have " + objectQnt + " units in this chunk");
            
            for (int j = 0; j < objectQnt; j++)
            {
                GameObject obj = objectsPool[i].Dequeue();
                chunk.chunkObjects.Add(new List<GameObject>());
                chunk.chunkObjects[i].Add(obj);

                //Position
                float rdmX = Random.Range(chunk.ChunkMinLimit.x, chunk.ChunkMaxLimit.x);
                float rdmZ = Random.Range(chunk.ChunkMinLimit.z, chunk.ChunkMaxLimit.z);

                Debug.Log("X: " + rdmX + " | Z: " + rdmZ);

                Vector3 rdmPos = new Vector3(rdmX, 30, rdmZ);

                RaycastHit hit;

                Debug.DrawRay(rdmPos, Vector3.down * 100, Color.red, 2f);
                if (Physics.Raycast(rdmPos, Vector3.down, out hit, 100, groundLayer))
                {
                    obj.transform.position = hit.point;
                }

                //Rotation
                obj.transform.rotation = objects[i].GetObjectRotationVariation();

                //Scale
                obj.transform.localScale = objects[i].GetObjectScaleVariation();
            }
        }
    }

    /// <summary>
    /// Remove objects from chunk and put them back in the pool
    /// </summary>
    public void UnpopulateChunk(ChunkManager chunk)
    {
        for (int i = 0; i < chunk.chunkObjects.Count; i ++)
        {
            for (int j = 0; j < chunk.chunkObjects[i].Count; j++)
            {
                GameObject obj = chunk.chunkObjects[i][j];
                objectsPool[i].Enqueue(obj);
                obj.transform.position = poolLocation;
            }
            chunk.chunkObjects[i].Clear();
        }
    }
}
