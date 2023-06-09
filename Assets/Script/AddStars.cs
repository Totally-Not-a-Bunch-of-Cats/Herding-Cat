using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddStars : MonoBehaviour
{
  public void AddStar(int star)
    {
        GameManager.Instance.CurrentStars += star;
    }
}
