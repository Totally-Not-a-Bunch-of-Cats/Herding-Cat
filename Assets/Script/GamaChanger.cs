using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GamaChanger : MonoBehaviour
{
    public GameObject[] GM;
    private void Awake()
    {
        GM = GameObject.FindGameObjectsWithTag("Game Manager");
        StartCoroutine(CheckOpacity());
    }

    /// <summary>
    /// is tied to a button click to change the color of the object
    /// </summary>
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
    /// <summary>
    /// unity problems so its down here so it has time to think
    /// </summary>
    /// <returns></returns>
    IEnumerator CheckOpacity()
    {
        yield return new WaitForSeconds(.15f);
        if (!GM[GM.Length - 1].GetComponent<GameManager>().MusicToggle)
        {
            transform.GetComponent<Image>().color = new Color(1, 1, 1, .4f);
        }
    }
}
