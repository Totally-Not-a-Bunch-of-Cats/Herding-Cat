using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingButton : MonoBehaviour
{
    public GameObject Image;
    bool Fliped = false;
    public void Click()
    {
        if(!Fliped)
        {
            gameObject.transform.localPosition += new Vector3(210, 0, 0);
            Image.transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
            Fliped = true;
        }
        else
        {
            gameObject.transform.localPosition += new Vector3(-210, 0, 0);
            Image.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
            Fliped = false;
        }
    }
}
