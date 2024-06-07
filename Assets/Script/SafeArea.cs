using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeArea : MonoBehaviour
{
    Vector2 Area;
    void Start()
    {
        Area = new Vector2(Screen.safeArea.y, 0);
        transform.GetComponent<RectTransform>().offsetMax = -Area;
        transform.GetComponent<RectTransform>().offsetMin = Area;
    }
}
