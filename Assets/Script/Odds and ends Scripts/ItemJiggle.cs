using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemJiggle : MonoBehaviour
{
    private void OnEnable()
    {
        transform.Rotate(0.0f, 0.0f, 25f, Space.World);
        StartCoroutine(Wait());
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(.25f);
        transform.Rotate(0.0f, 0.0f, -50f, Space.World);
        StartCoroutine(Wait2());
    }
    IEnumerator Wait2()
    {
        yield return new WaitForSeconds(.25f);
        transform.Rotate(0.0f, 0.0f, 50f, Space.World);
        StartCoroutine(Wait3());
    }
    IEnumerator Wait3()
    {
        yield return new WaitForSeconds(.25f);
        transform.Rotate(0.0f, 0.0f, -25f, Space.World);
    }
}
