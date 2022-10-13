using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for all <see cref="Board"/> tiles
/// </summary>
[System.Serializable]
public abstract class Tile : ScriptableObject
{

    [SerializeField] private Sprite Image;
    
    /// <summary>
    /// Gets the image that shows the Tile
    /// </summary>
    /// <returns>The sprite of the Tile</returns>
    public Sprite GetImage(){
        return Image;
    }

    /// <summary>
    /// Check if this object is of a certain type.
    /// </summary>
    /// <typeparam name="T">The <see cref="Tile"/> type to check against this object.</typeparam>
    /// <returns>True if this object is the same type.</returns>
    public bool Is<T>() where T : Tile
    {
        return typeof(T).Equals(GetType());
    }
}
