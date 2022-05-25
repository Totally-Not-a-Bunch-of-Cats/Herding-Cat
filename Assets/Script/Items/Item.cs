/// <summary>
/// A item class holds the name of the item, 
/// what selection type it is for cats, the number of tiles a cat will move, and
/// 
/// Action Amoutn can be +/-. Positive means away from item, while negative means to move toward.
/// </summary>
public class Item
{
    private string name;
    private WhichCat whichCat;
    //TODO Add action types
    private int actionAmount;
    private Size size;

    public Item(string name, WhichCat whichCat, int actionAmount, Size size)
    {
        this.name = name;
        this.whichCat = whichCat;
        this.actionAmount = actionAmount;
        this.size = size;
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

    public Size getSize()
    {
        return this.size;
    }

    public void setName(string name)
    {
        if (this.name != name)
        {
            this.name = name;
        }
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

    public void setSize(Size size)
    {
        if (this.size.getWidth() != size.getWidth())
        {
            this.size = size;
        } else if (this.size.getHeight() != size.getHeight())
        {
            this.size = size;
        }
    }
}
