using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToLevelSelect : MonoBehaviour
{
    public void SwitchToLevelSelect()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            if (this.transform.GetChild(i).name == "Main Menu")
            {
                this.transform.GetChild(i).gameObject.SetActive(false);
            }
            if (this.transform.GetChild(i).name == "Level Select")
            {
                this.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
}
