using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        GameManager.Instance._screenResizeManager.RescaleItem(gameObject);
    }
}
