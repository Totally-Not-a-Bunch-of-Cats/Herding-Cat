using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Holds reference to children of gameobject and sets up buttons for the adjustment object
/// </summary>
public class ItemAdjPanel : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public GameObject HighLightObject;
    public Image ItemImage;
    public Button DeleteButton;
    public Button HighlightButton;
    public int num;
    public Canvas Canvas;
    [SerializeField] private Transform Parent;
    [SerializeField] private GameObject HoverObject;

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
        HoverObject = Parent.parent.GetChild(1).gameObject;
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

    /// <summary>
    /// Drags of the item adjust panel entry
    /// </summary>
    /// <param name="data"></param>
    public void OnDrag(PointerEventData data)
    {
        if (!HoverObject.activeSelf)
        {
            HoverObject.SetActive(true);
            HoverObject.transform.GetChild(1).GetComponent<Image>().sprite = ItemImage.sprite;
        }

        transform.localPosition += new Vector3(0, data.delta.y/Canvas.scaleFactor, 0);
        HoverObject.transform.position = transform.position;
    }

    /// <summary>
    /// Stops the dragging of the item adjust panel entry
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        HoverObject.SetActive(false);
        Snap();
    }

    /// <summary>
    /// Snaps the item adjust panel entry into its new place
    /// </summary>
    public void Snap()
    {
        float Ycheck;
        int Yfinial;
        int SiblingNum = transform.GetSiblingIndex();
        Ycheck = transform.localPosition.y / 125;
        Yfinial = (int)Mathf.Round(-Ycheck);
        if(Yfinial == 0)
        {
            //Debug.Log("updated order for 0" + Yfinial);
            transform.SetSiblingIndex(Yfinial);
        }
        else
        {
            //Debug.Log("updated order" + Yfinial);
            transform.SetSiblingIndex(Yfinial - 1);
        }
        if(transform.GetSiblingIndex() == SiblingNum)
        {
            //Debug.Log("didnt move");
            transform.localPosition = new Vector3(transform.localPosition.x, -60 + (SiblingNum * -125), transform.localPosition.z);
        }
        ReorderItems();
    }

    /// <summary>
    /// Reorders the item list to match the changed item adjust panel entries
    /// </summary>
    void ReorderItems()
    {
        List<PosObject> OldItems = new List<PosObject>();
        for (int i = 0; i < GameManager.Instance._matchManager.GameBoard.Items.Count; i++)
        {
            if (num > transform.GetSiblingIndex())
            {
                if (i < transform.GetSiblingIndex())
                {
                    // dont move the ones above the ones we moved
                    OldItems.Add(GameManager.Instance._matchManager.GameBoard.Items[i]);
                }
                else if (i == transform.GetSiblingIndex())
                {
                    //placing the one we moved where it should go
                    OldItems.Add(GameManager.Instance._matchManager.GameBoard.Items[num]);
                }
                else
                {
                    //this one catches if the last one is diff
                    if (i == Parent.childCount - 1 && GameManager.Instance._matchManager.GameBoard.Items[i].ItemAdjObject.num != num)
                    {
                        OldItems.Add(GameManager.Instance._matchManager.GameBoard.Items[i]);
                    }
                    //this one catches if the second to last one was moved and the last one is the same last one
                    else if (i > num)
                    {
                        OldItems.Add(GameManager.Instance._matchManager.GameBoard.Items[i]);
                    }
                    else
                    {
                        OldItems.Add(GameManager.Instance._matchManager.GameBoard.Items[i - 1]);
                    }
                }
            }
            else
            {
                if (i < num || i > transform.GetSiblingIndex())
                {
                    // dont move the ones above the ones we moved
                    OldItems.Add(GameManager.Instance._matchManager.GameBoard.Items[i]);
                }
                else if (i == transform.GetSiblingIndex())
                {
                    //placing the one we moved where it should go
                    OldItems.Add(GameManager.Instance._matchManager.GameBoard.Items[num]);
                }
                else
                {
                    OldItems.Add(GameManager.Instance._matchManager.GameBoard.Items[i + 1]);
                }
            }
        }
        GameManager.Instance._matchManager.GameBoard.Items = OldItems;
        for (int i = 0; i < GameManager.Instance._matchManager.GameBoard.Items.Count; i++)
        {
            if (GameManager.Instance._matchManager.GameBoard.Items[i] != null)
            {
                GameManager.Instance._matchManager.GameBoard.Items[i].ItemAdjObject.num = i;
            }
        }
    }
}
