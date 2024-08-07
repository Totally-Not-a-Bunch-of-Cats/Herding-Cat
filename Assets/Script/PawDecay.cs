using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawDecay : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(Decay());
    }
    IEnumerator Decay()
    {
        yield return new WaitForSeconds(.5f);
        Destroy(this);
    }
}
