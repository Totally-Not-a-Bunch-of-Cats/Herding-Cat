using TMPro;
using UnityEngine;
public class AfterGameStarReport : MonoBehaviour
{
    public TMP_Text ItemsUsed;
    public TMP_Text RoundsElapsed;
    // Start is called before the first frame update
    public void OnEnable()
    {
        ItemsUsed.text = "Items Used:  " + GameManager.Instance._matchManager.ItemsUsed + " Target Items:  " + GameManager.Instance._matchManager.CurrentLevel.TargetItems;
        RoundsElapsed.text = "Rounds Passed:  " + GameManager.Instance._matchManager.RoundsPlayed + " Target Rounds:  " + GameManager.Instance._matchManager.CurrentLevel.TargetRounds;
    }
}
