using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Holds reference to children of gameobject and sets up buttons for the adjustment object
/// </summary>
public class ItemAdjPanel : MonoBehaviour
{
    public GameObject HighLightObject;
    public Image ItemImage;
    public Button DeleteButton;
    public Button HighlightButton;
    public int num;

    /// <summary>
    /// 
    /// </summary>
    private void Start()
    {
        num = GameManager.Instance._matchManager.GameBoard.Items.Count - 1;
        GameManager.Instance._matchManager.GameBoard.Items[num].ItemAdjObject = this;
        DeleteButton.onClick.AddListener(() => DeleteItem());
        HighlightButton.onClick.AddListener(() => HighlightItem());

    }

    /// <summary>
    /// Deletes Item on board
    /// </summary>
    public void DeleteItem()
    {
        if (num >= 0 && num <= GameManager.Instance._matchManager.GameBoard.Items.Count)
        {
            Destroy(GameManager.Instance._matchManager.GameBoard.Items[num].Object.gameObject);
            Destroy(GameManager.Instance._matchManager.GameBoard.Items[num].ItemAdjObject.gameObject);
            GameManager.Instance._matchManager.GameBoard.Set(GameManager.Instance._matchManager.GameBoard.Items[num].Position, null);
            GameManager.Instance._matchManager.GameBoard.Items[num] = null;
            GameManager.Instance._matchManager.ItemsUsed -= 1;
        }
        else
        {
            Debug.LogError($"Index must be between 0 and ({GameManager.Instance._matchManager.GameBoard.Items.Count}");
            throw new System.ArgumentOutOfRangeException($"Index must be between 0 and ({GameManager.Instance._matchManager.GameBoard.Items.Count}");
        }
    }

    public void HighlightItem()
    {
        //PosObject CurrentSelectedItem = GameManager.Instance._uiManager.CurrentSelectedItem;
        if (num >= 0 && num <= GameManager.Instance._matchManager.GameBoard.Items.Count)
        {
            GameManager.Instance._uiManager.HighlightItem(num);
            }
        else
        {
            Debug.LogError($"Index must be between 0 and ({GameManager.Instance._matchManager.GameBoard.Items.Count}");
            throw new System.ArgumentOutOfRangeException($"Index must be between 0 and ({GameManager.Instance._matchManager.GameBoard.Items.Count}");
        }
    }
}
