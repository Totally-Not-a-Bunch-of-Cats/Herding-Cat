using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveLocation : MonoBehaviour
{
    Vector2 Position;
    void Start()
    {
        Position = transform.position;
        GameManager.Instance._ItemManager.ItemLocation(Position);
    }

}
