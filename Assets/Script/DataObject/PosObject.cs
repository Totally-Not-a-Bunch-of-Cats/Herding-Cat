/** @Author Damian Link */
using UnityEngine;

/// <summary>
/// Object holding reference of a position on the board and the transform that relates to the object
/// </summary>
[System.Serializable]
public class PosObject
{
    public string Name = "";
    [SerializeField] public Vector2Int Position;
    [SerializeField] public Transform Object;
    [SerializeField] public ItemAdjPanel ItemAdjObject;
    [SerializeField] public bool Sleeping = false;
    [SerializeField] public Tile Tile;

    /// <summary>
    /// Creates new PosObject to relate a postion on board to Object transform
    /// </summary>
    /// <param name="NewPos">Position on Board<see cref="Board"/> of the object</param>
    /// <param name="NewName">Name of the object that is being stored</param>
    /// <param name="Obj">Transform of the object<see cref="Board"/></param>
    public PosObject(Vector2Int NewPos, string NewName, Tile NewTile, Transform Obj = null)
    {
        Name = NewName;
        Position = NewPos;
        Tile = NewTile;
        if (Obj != null)
        {
            Object = Obj;
        }
    }

    /// <summary>
    /// Creates new PosObject to relate a postion on board to Object transform
    /// </summary>
    /// <param name="oldPosObjectV2">Position on Board</param>
    /// <param name="oldPosObjectTrans">Transform of the object</param>
    /// <param name="oldPosObjectPanel">Refrence of Item Panel Object</param>
    /// <param name="oldPosObjectName">Name of the object that is being stored</param>
    public PosObject(Vector2Int oldPosObjectV2, Transform oldPosObjectTrans, ItemAdjPanel oldPosObjectPanel, string oldPosObjectName, Tile NewTile)
    {
        Position = oldPosObjectV2;
        Object = oldPosObjectTrans;
        Tile = NewTile;
        ItemAdjObject = oldPosObjectPanel;
        Name = oldPosObjectName;
    }

}
