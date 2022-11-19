using UnityEngine;


/// <summary>
/// Trap data object.
/// </summary>
[CreateAssetMenu(fileName = "Trap", menuName = "ScriptableObjects/Tiles/New Trap", order = 3)]
public class Trap : Tile
{
    [Tooltip("Flag on whether this trap ends the turn/movement of a cat early.")]
    public bool EndsTurn = false;

    [Tooltip("Number of turns to skip for the cat.")]
    public int Skipped = 0;

    [Tooltip("Direction to move the cat when landing on the space.")]
    public Direction Direction = Direction.None;
}
