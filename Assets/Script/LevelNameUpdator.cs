using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class LevelNameUpdator : MonoBehaviour
{
    public TMP_Text Text;
    // Start is called before the first frame update
    public void NameUpdate()
    {
        Text.text = "Level " + GameManager.Instance._matchManager.CurrentLevel.name;
    }
}
