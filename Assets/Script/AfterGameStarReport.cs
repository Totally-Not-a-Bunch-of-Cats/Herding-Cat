using TMPro;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class AfterGameStarReport : MonoBehaviour
{
    public TMP_Text ItemsUsed;
    public TMP_Text RoundsElapsed;
    public List<Image> Stars;

    // Start is called before the first frame update
    public void OnEnable()
    {
        ItemsUsed.text = "Items Used:  " + GameManager.Instance._matchManager.ItemsUsed + " Target Items:  " + GameManager.Instance._matchManager.CurrentLevel.TargetItems;
        RoundsElapsed.text = "Rounds Passed:  " + GameManager.Instance._matchManager.RoundsPlayed + " Target Rounds:  " + GameManager.Instance._matchManager.CurrentLevel.TargetRounds;

        // Activates stars(Sets the stars on the Win UI to the won amount)
        for (int i = 0; i < GameManager.Instance._matchManager.CurrentLevel.StarsEarned; i++)
        {
            Stars[i].color = Color.white;
        }
    }

}
