using NUnit.Framework;
/// <summary>
/// Testing the Board creation
/// </summary>
public class BoardTest
{

    [Test]
    public void TestEmptyEvenBoard()
    {
        //Normal constructor test, with no given tiles
        Board Board1 = new Board(6, 6);

        bool isEmpty = true;
        for (int x = 0; x < Board1.GetWidth(); x++)
        {
            for (int y = 0; y < Board1.GetHeight(); y++)
            {
                if (Board1.At(x, y) != null)
                {
                    isEmpty = false;
                    break;
                }
            }
        }

        Assert.AreEqual(6, Board1.GetWidth());
        Assert.AreEqual(6, Board1.GetHeight());
        Assert.IsTrue(isEmpty);
    }

    [Test]
    public void TestEmptyOddBoard()
    {
        //Normal constructor test, with no given tiles
        Board Board1 = new Board(7, 7);

        bool isEmpty = true;
        for (int x = 0; x < Board1.GetWidth(); x++)
        {
            for (int y = 0; y < Board1.GetHeight(); y++)
            {
                if (Board1.At(x, y) != null)
                {
                    isEmpty = false;
                    break;
                }
            }
        }

        Assert.AreEqual(7, Board1.GetWidth());
        Assert.AreEqual(7, Board1.GetHeight());
        Assert.IsTrue(isEmpty);
    }
}
