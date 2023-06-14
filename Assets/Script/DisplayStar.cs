using TMPro;
using UnityEngine;

public class DisplayStar : MonoBehaviour
{
    public TMP_Text Stars;
    public void OnEnable()
    {
        Stars.text = "Stars: " + GameManager.Instance.StarCount;
    }
}
