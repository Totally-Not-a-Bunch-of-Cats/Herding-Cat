using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatCustomizationLure : MonoBehaviour
{
    public GameObject particles;
    private void OnEnable()
    {
        if(GameManager.Instance.StarCount >= 30)
        {
            particles.SetActive(true);
}
    }
}
