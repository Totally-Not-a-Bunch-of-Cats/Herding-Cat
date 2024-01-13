using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSafeArea : MonoBehaviour
{
    Rect Area;
    void Start()
    {
        Area = Screen.safeArea;
        Area.x = 0;
        Area.y = 0;
        transform.GetComponent<Camera>().rect = Area;
    }
}
