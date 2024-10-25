using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwanPaw : MonoBehaviour
{
    public GameObject Paw;
    public bool Walking = false;
    public bool Offset = true;
    public float PawRotation = 0;
    public float PawRotationY = 0;
    void Update()
    {
        if (Walking && Offset)
        {
            Offset = false;
            StartCoroutine(DelayPaw());
        }
    }

    IEnumerator DelayPaw()
    {
        float temp = Random.Range(.10f, .22f);
        yield return new WaitForSeconds(temp);
        GameObject tempG = Instantiate(Paw, transform.position, Quaternion.identity);
        tempG.transform.Rotate(0, PawRotationY, PawRotation);
        Offset = true;
    }
}
