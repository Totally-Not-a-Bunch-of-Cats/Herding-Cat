using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public Items ItemsList = new Items();

    public List<Item> ItemOrder;
    public List<GameObject> ListofItemPrefabs;

    public static ItemManager Instance;
    private void Awake()
    {
        // If there is an instance, and it's not me, MURDER myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        CreateItemPrefabs();
    }

    void CreateItemPrefabs()
    {
        ItemsList["Snake"].setPrefab(ListofItemPrefabs[0]);
        ItemsList["Toy"].setPrefab(ListofItemPrefabs[1]);
        ItemsList["Loud Noise"].setPrefab(ListofItemPrefabs[2]);
        ItemsList["Treat"].setPrefab(ListofItemPrefabs[3]);
        ItemsList["Catnip"].setPrefab(ListofItemPrefabs[4]);
        ItemsList["Dog"].setPrefab(ListofItemPrefabs[5]);
    }

    public Items GetItemList()
    {
        return ItemsList;
    }
    public void AddItem(string name)
    {
        ItemOrder.Add(ItemsList[name]);
    }
    /// <summary>
    /// cycels through all of the items
    /// </summary>
    public void CycyleItems()
    {
        Debug.Log("im calling item movement");
        ItemMovement();
    }

    void ItemMovement()
    {
        Cat Curcat;
        //cycles through all placed items and hands them one at a time to the Cat manager
        for (int i = 0; i < ItemOrder.Count; i++)
        {
            Debug.Log("im in item movement");
            //checks if its all cats or not
            if (ItemOrder[i].GetWhichCat() != WhichCat.CURRENT)
            {
                //affects all cats
                GameManager.Instance._CatManager.MoveCats(ItemOrder[i]);
            }
            else
            {
                //gets the closes cat from the gamemanager instance catmanager by passing a location of the item we care about
                //moves said cat an amount determined by the items location from the cat  (This only moves 1 cat)
                Curcat = GameManager.Instance._CatManager.FindCat(ItemOrder[i].getLocation());
                GameManager.Instance._CatManager.MoveCat(Curcat, GetMovementAmount(ItemOrder[i], Curcat));
                Debug.Log(Curcat);
                Debug.Log(GetMovementAmount(ItemOrder[i], Curcat));
            }
        }
    }

    public Vector2Int GetMovementAmount(Item item, Cat cat)
    {
        Vector2 itemWorld;
        Vector2 catWorld;
        Vector2Int movemnetAmount = Vector2Int.zero;
        //math out the cat location and the item location to get movement.
        itemWorld = item.getLocation();
        catWorld = cat.WorldLocation;
        if(itemWorld.x == catWorld.x)
        {
            if(itemWorld.y >= catWorld.y)
            {
                movemnetAmount = new Vector2Int(item.getActionAmount(),0);
            }
            else
            {
                movemnetAmount = new Vector2Int(item.getActionAmount(), 0);
            }
        }
        if (itemWorld.y == catWorld.y)
        {
            if (itemWorld.x >= catWorld.x)
            {
                movemnetAmount = new Vector2Int(0, item.getActionAmount());
            }
            else
            {
                movemnetAmount = new Vector2Int(0, item.getActionAmount());
            }
        }
        return movemnetAmount;
    }


    public void ItemLocation(Vector2 Location)
    {
        int LengthofList;
        LengthofList = ItemOrder.Count - 1;
        ItemOrder[LengthofList].setLocation(Location);
    }
}
