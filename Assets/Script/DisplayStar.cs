using TMPro;
using UnityEngine;

public class DisplayStar : MonoBehaviour
{
    //i hate git hub swear to DOG will cut them
    public TMP_Text Stars;
    public void OnEnable()
    {
        Stars.text = "Stars: " + GameManager.Instance.StarCount;
    }
}
