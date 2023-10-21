using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddStars : MonoBehaviour
{
  public void AddStar(int star)
    {
        GameManager.Instance._PlayerPrefsManager.SaveInt("StarCount", star);
        GameManager.Instance.StarCount += star;
    }
}