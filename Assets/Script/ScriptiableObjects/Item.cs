using UnityEngine;

/// <summary>
/// Item data object.
/// </summary>
[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Tiles/New Item", order = 1)]
public class Item : Tile
{
    [Tooltip("-1 is entire board, else specific radius away.")]
    public int Radius = -1;
    [Tooltip("the distance the item moves the cat")]
    public int MoveDistance = 0;
    [Tooltip("Direction of item's affect.")]
    public Direction Direction = Direction.Away;
}
