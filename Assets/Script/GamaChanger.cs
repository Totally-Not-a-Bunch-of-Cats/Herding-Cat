using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GamaChanger : MonoBehaviour
{
    public void ButtonClick()
    {
        if(transform.GetComponent<Image>().color == new Color(1,1,1,1))
        {
            transform.GetComponent<Image>().color = new Color(1, 1, 1, .4f);
        }
        else
        {
            transform.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
    }
}
