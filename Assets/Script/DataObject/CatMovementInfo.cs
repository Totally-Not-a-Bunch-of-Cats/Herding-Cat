using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMovementInfo
{
    public int Index;
    public Vector2Int? Destination;
    public int Distance;
    public bool Used;


    public CatMovementInfo(int Index = 0, int Distance = 0, Vector2Int? Destination = null)
    {
        this.Index = Index;
        this.Destination = Vector2Int.zero;
        this.Distance = Distance;
        this.Used = false;
    }
    public CatMovementInfo(int Index = 0, int Distance = 0)
    {
        this.Index = Index;
        this.Destination = null;
        this.Distance = Distance;
        this.Used = false;
    }

    public CatMovementInfo()
    {
        this.Index = 0;
        this.Destination = null;
        this.Distance = 0;
        this.Used = false;
    }
}
