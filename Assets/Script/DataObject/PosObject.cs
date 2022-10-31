using UnityEngine;

[System.Serializable]
public class PosObject
{
    public string Name = "";
    [SerializeField] public Vector2Int Position;
    [SerializeField] public Transform Object;

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
