using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwanPaw : MonoBehaviour
{
    public GameObject Paw;
    public bool Walking = false;
    public void StarWalk()
    {
        StartCoroutine(DelayPaw());
    }

    IEnumerator DelayPaw()
    {
        yield return new WaitForSeconds(.5f);
        Instantiate(Paw);
        if(Walking == true)
        {
            StarWalk();
        }
    }
}
