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


    /// <summary>
    /// Creates new PosObject to relate a postion on board to Object transform
    /// </summary>
    /// <param name="NewPos">Position on <see cref="Board"/> of the object</param>
    /// <param name="NewName">Name of the object that is being stored</param>
    /// <param name="Obj">Transform of the object on the <see cref="Board"/></param>
    public PosObject(Vector2Int NewPos, string NewName, Transform Obj = null)
    {
        Name = NewName;
        Position = NewPos;
        if (Obj != null)
        {
            Object = Obj;
        }
    }


}
