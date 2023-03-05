using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Holds reference to children of gameobject and sets up buttons for the adjustment object
/// </summary>
public class ItemAdjPanel : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public GameObject HighLightObject;
    public Image ItemImage;
    public Button DeleteButton;
    public Button HighlightButton;
    public int num;
    public Canvas Canvas;
    [SerializeField] Transform Parent;

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
        Parent = transform.parent;
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

    public void OnBeginDrag(PointerEventData eventData)
    {
        //transform.SetSiblingIndex(Parent.childCount);
    }

    public void OnDrag(PointerEventData data)
    {
        transform.localPosition += new Vector3(0, data.delta.y/Canvas.scaleFactor, 0);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Snap();
    }
    public void Snap()
    {
        float Ycheck;
        int Yfinial;
        int SiblingNum = transform.GetSiblingIndex();
        Ycheck = transform.localPosition.y / 125;
        Yfinial = (int)Mathf.Round(-Ycheck);
        if(Yfinial == 0)
        {
            Debug.Log("updated order for 0" + Yfinial);
            transform.SetSiblingIndex(Yfinial);
        }
        else
        {
            Debug.Log("updated order" + Yfinial);
            transform.SetSiblingIndex(Yfinial - 1);
        }
        if(transform.GetSiblingIndex() == SiblingNum)
        {
            Debug.Log("didnt move");
            transform.localPosition = new Vector3(transform.localPosition.x, -60 + (SiblingNum * -125), transform.localPosition.z);
        }
    }
}
