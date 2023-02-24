using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Holds reference to children of gameobject and sets up buttons for the adjustment object
/// </summary>
public class ItemAdjPanel : MonoBehaviour, IDragHandler
{
    public GameObject HighLightObject;
    public Image ItemImage;
    public Button DeleteButton;
    public Button HighlightButton;
    public int num;
    public Canvas Canvas;

    /// <summary>
    /// Adds listeners and sets up script
    /// </summary>
    private void Start()
    {
        num = GameManager.Instance._matchManager.GameBoard.Items.Count - 1;
        GameManager.Instance._matchManager.GameBoard.Items[num].ItemAdjObject = this;
        DeleteButton.onClick.AddListener(() => DeleteItem());
        HighlightButton.onClick.AddListener(() => HighlightItem());
        Canvas = GameObject.Find("GUI").GetComponent<Canvas>();

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

    /// <summary>
    /// calls highlight if num is valid
    /// </summary>
    public void HighlightItem()
    {
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

    public void OnDrag(PointerEventData data)
    {
        Debug.Log(Canvas.scaleFactor);
        transform.localPosition += new Vector3(0, data.delta.y/Canvas.scaleFactor, 0);
    }

}
