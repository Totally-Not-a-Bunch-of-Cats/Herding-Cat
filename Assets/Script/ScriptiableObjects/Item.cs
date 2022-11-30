using UnityEngine;

/// <summary>
/// Item data object.
/// </summary>
[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Tiles/New Item", order = 1)]
public class Item : Tile
{
    [Tooltip("Cats will be effected in the area of range.")]
    public int Radius = 0;
    [Tooltip("Causes all cats in the radius describe above to move if true")]
    public bool AllCatsinRadius = false;
    [Tooltip("the distance the item moves the cat")]
    public int MoveDistance = 0;
    [Tooltip("Direction of item's movement.")]
    public Direction Direction = Direction.Away;
    [Tooltip("A picture of the Item.")]
    public Sprite Picture;
}
