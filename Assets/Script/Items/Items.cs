
/// <summary>
/// Holds reference to all the current items in the game
/// 
/// You may access an item but calling the instance of the Items class followed by the [] operator.
/// The syntax is Items["Name of Item"]. This will return the item given by the name or null if no item was found.
/// </summary>
public class Items
{
    //list of all items
    private Item[] allItems =
    {
        new Item("Snake", WhichCat.CURRENT, 3),
        new Item("Toy", WhichCat.CURRENT, -3),
        new Item("Loud Noise", WhichCat.ALL, 3),
        new Item("Treat", WhichCat.ALL, -3),
        new Item("Catnip", WhichCat.ALL, -5),
        new Item("Dog", WhichCat.ALL, 5)
    };


    /// <summary>
    /// Access the list of all items with it's name
    /// </summary>
    /// <param name="item">The string name of the item to be retrieved</param>
    /// <returns>The item found with the given name or null if no item found</returns>
    public Item this[string item]
    {
        get
        {
            foreach (Item i in allItems)
            {
                if (i.getName() == item)
                {
                    return i;
                }
            }
            return null;
        }
        private set { }
    }
}
