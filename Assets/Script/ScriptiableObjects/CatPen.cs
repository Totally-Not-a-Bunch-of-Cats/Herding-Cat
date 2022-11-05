using UnityEngine;

/// <summary>
/// Cat data class.
/// </summary>
[CreateAssetMenu(fileName = "CatPen", menuName = "ScriptableObjects/Tiles/Cat Pen", order = 4)]
public class CatPen : Tile
{
    [Tooltip("Number of Cats in Pen.")]
    public int NumCatinPen;
}