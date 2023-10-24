using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetWarning : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.Instance._catInfoManager.Warning = GameObject.Find("Canvas/CatMenu/Warning");
    }
}
