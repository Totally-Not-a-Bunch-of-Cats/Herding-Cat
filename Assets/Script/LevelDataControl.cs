using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDataControl : MonoBehaviour
{
    /// <summary>
    /// Resets star counts to 0 and locks all levels except first one
    /// </summary>
    public void ResetLevelsToBeggining()
    {
        for (int i = 0; i < GameManager.Instance.GamelevelList.GameLevel.Count; i++)
        {
            GameManager.Instance.GamelevelList.GameLevel[i].StarsEarned = 0;
            if (i != 0)
            {
                GameManager.Instance.GamelevelList.GameLevel[i].SetUnlocked(false);
            }
        }
    }

    /// <summary>
    /// Resets star counts to 0 and locks all levels except first one
    /// </summary>
    public void ResetStars()
    {
        for (int i = 0; i < GameManager.Instance.GamelevelList.GameLevel.Count; i++)
        {
            GameManager.Instance.GamelevelList.GameLevel[i].StarsEarned = 0;
        }
    }

    /// <summary>
    /// Resets star counts to 0 and locks all levels except first one
    /// </summary>
    public void UnlockAllLeves()
    {
        for (int i = 0; i < GameManager.Instance.GamelevelList.GameLevel.Count; i++)
        {
            GameManager.Instance.GamelevelList.GameLevel[i].SetUnlocked(true);
        }
    }
}
