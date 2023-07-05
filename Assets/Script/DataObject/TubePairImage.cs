using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TubePairImage
{
    public Transform Tube1;
    public Transform Tube2;
    public Sprite Image;
    public Vector2Int Pos1;
    public Vector2Int Pos2;

    public TubePairImage(Transform Tube, PosTile Tile)
    {
        Tube1 = Tube;
        Pos2 = Tile.TubeDestination;
        Pos1 = Tile.Position;
    }

    public void SetTubeSprites()
    {
        Tube1.GetChild(0).GetComponent<SpriteRenderer>().sprite = Image;
        Tube2.GetChild(0).GetComponent<SpriteRenderer>().sprite = Image;
    }
}
