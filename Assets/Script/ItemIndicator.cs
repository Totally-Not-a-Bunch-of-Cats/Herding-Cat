using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIndicator : MonoBehaviour
{
    public int SizeofLists;
    public List<GameObject> IndicatorsSouth;
    public List<GameObject> IndicatorsNorth;
    public List<GameObject> IndicatorsEast;
    public List<GameObject> IndicatorsWest;

    private void Start()
    {
        if(GameManager.Instance._matchManager.CurrentLevel.name == "1-1" || GameManager.Instance._matchManager.CurrentLevel.name == "1-6")
        {
            ActivateIndicators();
        }
    }

    void ActivateIndicators()
    {
        for(int i = 0; i < SizeofLists; i++)
        {
            if(i < IndicatorsWest.Count)
            {
                IndicatorsWest[i].SetActive(true);
            }
            if (i < IndicatorsNorth.Count)
            {
                IndicatorsNorth[i].SetActive(true);
            }
            if (i < IndicatorsEast.Count)
            {
                IndicatorsEast[i].SetActive(true);
            }
            if (i < IndicatorsSouth.Count)
            {
                IndicatorsSouth[i].SetActive(true);
            }
        }
    }

    void DetectindicatorDirection()
    {
        //check the gain board around the item tos ee if they exist
    }
}
