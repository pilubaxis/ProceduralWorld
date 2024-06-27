using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectInfo", menuName = "ScriptableObjects/ObjectInfo", order = 1)]
public class ObjectInfo : ScriptableObject
{
    public GameObject prefab = null;
    /// <summary>
    /// Quantity of object to instantiate in the pool on start game
    /// </summary>
    public int qntToInstantiate {
        get { return maxQntPerChunk * 9; } // 9 chunks
    }
    [Header("Quantity in Chunk")]

    /// <summary>
    /// Probability for chunk to have this object
    /// </summary>
    [SerializeField][Range(0, 1)] private float probabilityToHaveObjectPerChunk = 1;
    /// <summary>
    /// Min quanityty of this object
    /// </summary>
    [SerializeField] private int minQntPerChunk = 0;
    /// <summary>
    /// Max quantity of this object
    /// </summary>
    [SerializeField] private int maxQntPerChunk = 0;
    // Start is called before the first frame update

    [Header("Object Scale Variation")]

    /// <summary>
    /// Min scale variation of this object
    /// </summary>
    [SerializeField] private float minScale = 1.0f;
    /// <summary>
    /// Max scale variation of this object
    /// </summary>
    [SerializeField] private float maxScale = 1.0f;

    [Header("Object X Rotation Variation")]

    /// <summary>
    /// This object should rotate on X axis
    /// </summary>
    [SerializeField] bool shouldRotateX = false;
    /// <summary>
    /// The min rotation in X axis
    /// </summary>
    [SerializeField] float minRotVariationX = 0;
    /// <summary>
    /// The max rotation in X axis
    /// </summary>
    [SerializeField] float maxRotVariationX = 360;

    [Header("Object Y Rotation Variation")]

    /// <summary>
    /// This objectShould rotate on Y axis
    /// </summary>
    [SerializeField] bool shouldRotateY = false;
    /// <summary>
    /// The min rotation in Y axis
    /// </summary>
    [SerializeField] float minRotVariationY = 0;
    /// <summary>
    /// The max rotation in Y axis
    /// </summary>
    [SerializeField] float maxRotVariationY = 360;

    [Header("Object Z Rotation Variation")]

    /// <summary>
    /// This object should rotate on Z axis
    /// </summary>
    [SerializeField] bool shouldRotateZ = false;
    /// <summary>
    /// The min rotation in Z axis
    /// </summary>
    [SerializeField] float minRotVariationZ = 0;
    /// <summary>
    /// The max rotation in Z axis
    /// </summary>
    [SerializeField] float maxRotVariationZ = 360;


    /// <summary>
    /// Check if chunk shold have this object
    /// </summary>
    /// <returns></returns>
    public bool ChunkShouldHaveObject()
    {
        float rdm = Random.Range(0f, 1f);
        return rdm <= probabilityToHaveObjectPerChunk;
    }

    /// <summary>
    /// Get the quantity of this object the chunk will have
    /// </summary>
    /// <returns>Quantity</returns>
    public int QuantityToHaveInChunk()
    {
        return Random.Range(minQntPerChunk, maxQntPerChunk);
    }

    /// <summary>
    /// Get Object scale variation
    /// </summary>
    /// <returns></returns>
    public Vector3 GetObjectScaleVariation()
    {
        float scale = Random.Range(minScale, maxScale);
        return new Vector3(scale, scale, scale);
    }

    /// <summary>
    /// Get the rotation X variation for this object
    /// </summary>
    /// <returns>Random rotation value between minRotVariationX and maxRotVariationX</returns>
    public float GetObjectRotationVariationX()
    {
        return Random.Range(minRotVariationX, maxRotVariationX);
    }

    /// <summary>
    /// Get the rotation Y variation for this object
    /// </summary>
    /// <returns></returns>
    public float GetObjectRotationVariationY()
    {
        return Random.Range(minRotVariationY, maxRotVariationY);
    }

    /// <summary>
    /// Get the rotation Z variation for this object
    /// </summary>
    /// <returns>Random rotation value between minRotVariationZ and maxRotVariationZ</returns>
    public float GetObjectRotationVariationZ()
    {
        return Random.Range(minRotVariationZ, maxRotVariationZ);
    }

    /// <summary>
    /// Get the rotation variation as a Quaternion for this object
    /// </summary>
    /// <returns>A Quaternion representing the rotation variation</returns>
    public Quaternion GetObjectRotationVariation()
    {
        float rotationX = shouldRotateX ? GetObjectRotationVariationX() : 0;
        float rotationY = shouldRotateY ? GetObjectRotationVariationY() : 0;
        float rotationZ = shouldRotateZ ? GetObjectRotationVariationZ() : 0;

        return Quaternion.Euler(rotationX, rotationY, rotationZ);
    }
}
