using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// A item class holds the name of the item, 
/// what selection type it is for cats, the number of tiles a cat will move, and
/// 
/// Action Amoutn can be +/-. Positive means away from item, while negative means to move toward.
/// </summary>
[System.Serializable]
public class Item
{
    [SerializeField]
    private string name;
    [SerializeField]
    private WhichCat whichCat;
    //TODO Add action types
    [SerializeField]
    private int actionAmount;
    [SerializeField]
    private Vector2 Location;
    [SerializeField]
    private GameObject Prefab;
    public Item(string name, WhichCat whichCat, int actionAmount)
    {
        this.name = name;
        this.whichCat = whichCat;
        this.actionAmount = actionAmount;
    }

    public string getName()
    {
        return this.name;
    }

    public WhichCat GetWhichCat()
    {
        return this.whichCat;
    }

    public int getActionAmount()
    {
        return this.actionAmount;
    }

    public Vector2 getLocation()
    {
        return this.Location;
    }

    public void setName(string name)
    {
        if (this.name != name)
        {
            this.name = name;
        }
    }

    public void setPrefab(GameObject Prefab)
    {
        if (this.Prefab != Prefab)
        {
            this.Prefab = Prefab;
        }
    }

    public GameObject getPrefab()
    {
        return this.Prefab;
    }

    public void setWhichCat(WhichCat whichCat)
    {
        if(this.whichCat != whichCat)
        {
            this.whichCat = whichCat;
        }
    }

    public void setActionAmount(int actionAmount)
    {
        if(this.actionAmount != actionAmount)
        {
            this.actionAmount = actionAmount;
        }
    }

    public void setLocation(Vector2 location)
    {
        if (this.Location != location)
        {
            this.Location = location;
        }
    }
}
