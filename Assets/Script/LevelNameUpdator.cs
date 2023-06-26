using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class LevelNameUpdator : MonoBehaviour
{
    public TMP_Text LevelName;
    public TMP_Text TargetRounds;
    public TMP_Text TargetItems;
    // Start is called before the first frame update
    public void NameUpdate()
    {
        LevelName.text = "Level " + GameManager.Instance._matchManager.CurrentLevel.name;
        TargetRounds.text = "Target Rounds:  " + GameManager.Instance._matchManager.CurrentLevel.TargetRounds;
        TargetItems.text = "Target Items:  " + GameManager.Instance._matchManager.CurrentLevel.TargetItems;
    }

}
