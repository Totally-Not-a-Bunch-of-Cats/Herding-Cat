using TMPro;
using UnityEngine;

public class LockImage : MonoBehaviour
{
    public GameObject Lock;
    public TMP_Text levelString;
    private string LevelName;

    private void Start()
    {
        for (int i = 0; i < GameManager.Instance.Levels.Count; i++)
        {
            LevelName = "Level: " + GameManager.Instance.Levels[i].name;
            if (LevelName == levelString.text && GameManager.Instance.Levels[i].GetUnlocked())
            {
                Lock.SetActive(false);
                break;
            }
        }
    }
}
