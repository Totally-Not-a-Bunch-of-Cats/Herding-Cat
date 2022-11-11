/** @Author Zachary Boehm */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for all <see cref="Board"/> tiles
/// </summary>
[System.Serializable]
public abstract class Tile : ScriptableObject
{
    [SerializeField] private GameObject Prefab;

    /// <summary>
    /// Gets the image that shows the Tile
    /// </summary>
    /// <returns>The sprite of the Tile</returns>
    public GameObject GetPrefab(){
        return Prefab;
    }

    /// <summary>
    /// Check if this object is of a certain type.
    /// </summary>
    /// <typeparam name="T">The <see cref="Tile"/> type to check against this object.</typeparam>
    /// <returns>True if this object is the same type.</returns>
    public bool Is<T>() where T : Tile
    {
        if(GetType() != null)
        {
            return typeof(T).Equals(GetType());
        }
        return false;
    }
}
