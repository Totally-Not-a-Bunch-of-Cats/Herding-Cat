using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemNumOrder : MonoBehaviour
{
    public TMP_Text Num;
    public int PosInList;
    public int NumActiveEntries = 0;
    void LateUpdate()
    {
        PosInList = 0;
        NumActiveEntries = 0;
        for (int i = 0; i < GameManager.Instance._matchManager.GameBoard.Items.Count; i++)
        {
            if (GameManager.Instance._matchManager.GameBoard.Items[i] != null)
            {
                if (transform == GameManager.Instance._matchManager.GameBoard.Items[i].Object)
                {
                    PosInList = i;
                    break;
                }
            }
            if (GameManager.Instance._matchManager.GameBoard.Items[i] != null)
            {
                NumActiveEntries++;
            }
        }
        Num.text = (NumActiveEntries + 1).ToString();
    }
    //if (GameManager.Instance._matchManager.GameBoard.Items[PosInList] != null)
    //{
    //    if (transform == GameManager.Instance._matchManager.GameBoard.Items[PosInList].Object)
    //    {
    //        Num.text = (NumActiveEntries + 1).ToString();
    //    }
    //    else
    //    {
    //        for (int i = 0; i < GameManager.Instance._matchManager.GameBoard.Items.Count; i++)
    //        {
    //            if (transform == GameManager.Instance._matchManager.GameBoard.Items[i].Object)
    //            {
    //                PosInList = i;
    //                break;
    //            }
    //            if (GameManager.Instance._matchManager.GameBoard.Items[i] != null)
    //            {
    //                NumActiveEntries++;
    //            }
    //        }
    //        Num.text = (NumActiveEntries + 1).ToString();
    //    }
    //}
}
