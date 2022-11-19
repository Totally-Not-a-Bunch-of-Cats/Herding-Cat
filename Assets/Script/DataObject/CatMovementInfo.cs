using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMovementInfo
{
    public int Index;
    public Vector2Int? Destination;
    public int Distance;


    public CatMovementInfo(int Index = 0, int Distance = 0, Vector2Int? Destination = null)
    {
        this.Index = Index;
        this.Destination = Vector2Int.zero;
        this.Distance = Distance;
    }
    public CatMovementInfo(int Index = 0, int Distance = 0)
    {
        this.Index = Index;
        this.Destination = null;
        this.Distance = Distance;
    }

    public CatMovementInfo()
    {
        Debug.Log("default con");
        this.Index = 0;
        this.Destination = null;
        this.Distance = 0;
    }
}
