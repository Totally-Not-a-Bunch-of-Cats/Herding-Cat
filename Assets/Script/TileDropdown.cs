using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileDropdown : MonoBehaviour
{
    //public List<GameObject> Button

    public void SelectDropdownOption(int index)
    {
        Transform currentPane = transform.GetChild(index);
        if (currentPane.GetChild(1).gameObject.activeSelf)
        {
            currentPane.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            currentPane.GetChild(1).gameObject.SetActive(true);
        }
    }
}
