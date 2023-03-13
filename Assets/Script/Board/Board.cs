/** @Author Zachary Boehm */

using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// Grid of <see cref="BoardCell"/>'s.
/// </summary>
[System.Serializable]
public class Board
{
    [SerializeField]
    private Tile[,] _cells;
    public List<PosObject> Cats;
    public List<Vector2Int> CatVec2;
    //used to hold data on cats that just went into the pen for purpose of rewinding them
    public List<PosObject> SecondCatList;
    public int NumberofCats = 0;
    public List<PosObject> Items;
    public List<Vector2Int> CatPenLocation;
    public int NumCatinPen = 0;
    public List<int> SecondCatPos;
    private object currentLevel;
    private readonly int _width;
    private readonly int _height;

    /// <summary>
    /// Initialize 2D board with width and height
    /// </summary>
    /// <param name="Width">Board width</param>
    /// <param name="Height">Board height</param>
    public Board(int Width, int Height, PosTile[] Tiles = null)
    {
        this._height = Height;
        this._width = Width;
        _cells = new Tile[this._width, this._height];
        Cats = new List<PosObject>();
        SecondCatList = new List<PosObject>();
        Items = new List<PosObject>();
        CatPenLocation = new List<Vector2Int>();
        if (Tiles != null)
        {
            foreach (PosTile tile in Tiles)
            {
                Set(tile.Position, tile.Slate);
                if (tile.Slate.Is<Cat>())
                {
                    Cats.Add(new PosObject(tile.Position, tile.Slate.name));
                    NumberofCats++;
                }
                if (tile.Slate.Is<CatPen>())
                {
                    CatPenLocation.Add(tile.Position);
                }
            }
        }
    }

    /// <summary>
    /// Initialize 2D board with width and height
    /// </summary>
    /// <param name="dimensions">Board dimensions as a vector</param>
    public Board(Vector2Int dimensions, PosTile[] Tiles = null)
    {
        this._width = dimensions.x;
        this._height = dimensions.y;
        _cells = new Tile[this._width, this._height];
        Cats = new List<PosObject>();
        SecondCatList = new List<PosObject>();
        Items = new List<PosObject>();
        CatPenLocation = new List<Vector2Int>();
        if (Tiles != null)
        {
            foreach (PosTile tile in Tiles)
            {
                _cells[tile.Position.x, tile.Position.y] = tile.Slate;
                if (tile.Slate.Is<Cat>())
                {
                    Cats.Add(new PosObject(tile.Position, tile.Slate.name));
                    NumberofCats++;
                }
                if (tile.Slate.Is<CatPen>())
                {
                    CatPenLocation.Add(tile.Position);
                }
            }
        }
    }
    /// <summary>
    /// a deep copy constructor that copies the tiles and puts them into a different board 
    /// </summary>
    /// <param name="BoardToCopy">passes current board to board constructor</param>
    /// <param name="Tiles"> takes in an array of tiles to loopp through from the level data </param>
    public Board(Board BoardToCopy, PosTile[] Tiles = null)
    {
        this._width = BoardToCopy._width;
        this._height = BoardToCopy._height;
        CatPenLocation = new List<Vector2Int>();
        SecondCatPos = new List<int>();
        CatVec2 = new List<Vector2Int>();
        SecondCatList = new List<PosObject>();
        Cats = new List<PosObject>();
        _cells = new Tile[_width, _height];
        int i = 0;
        int j = 0;
        //loops through all non null tiles to update 
        if (Tiles != null)
        {
            foreach (PosTile tile in Tiles)
            {
                //_cells[tile.Position.x, tile.Position.y] = tile.Slate;
                if (tile.Slate.Is<Cat>())
                { 
                    if (BoardToCopy.Cats[i] != null)
                    {
                        //updates the cats position with each new board 
                        Cats.Add(new PosObject(BoardToCopy.Cats[i].Position, BoardToCopy.Cats[i].Object, BoardToCopy.Cats[i].ItemAdjObject, tile.Slate.name));
                        _cells[BoardToCopy.Cats[i].Position.x, BoardToCopy.Cats[i].Position.y] = tile.Slate;
                        CatVec2.Add(BoardToCopy.Cats[i].Position);
                        i++;
                    }
                    //this will never be called
                    else if(GameManager.Instance._matchManager.GameBoard.SecondCatList.Count > 0)
                    {
                        //add edge case for reverting a cat that just went into the cage 
                        Debug.Log("we added one");
                        Cats.Add(new PosObject(BoardToCopy.Cats[i].Position, GameManager.Instance._matchManager.GameBoard.SecondCatList[j].Object, GameManager.Instance._matchManager.GameBoard.SecondCatList[j].ItemAdjObject, GameManager.Instance._matchManager.GameBoard.SecondCatList[j].Name));
                        SecondCatPos.Add(i+j);
                        _cells[tile.Position.x, tile.Position.y] = tile.Slate;
                        j++;
                    }
                    else
                    {
                        //CatVec2.Add(BoardToCopy.Cats[i].Position);
                        Cats.Add(null);
                    }
                }
                if (tile.Slate.Is<CatPen>())
                {
                    CatPenLocation.Add(tile.Position);
                    _cells[tile.Position.x, tile.Position.y] = tile.Slate;
                }
                if (tile.Slate.Is<Trap>())
                {
                    _cells[tile.Position.x, tile.Position.y] = tile.Slate;
                }
            }
        }
        Items = BoardToCopy.Items;
        CatPenLocation = BoardToCopy.CatPenLocation;
        NumCatinPen = BoardToCopy.NumCatinPen;
        NumberofCats = BoardToCopy.NumberofCats;
    }

    /// <summary>
    /// Get a specific cell on the <see cref="Board"/> from a <see cref="Vector2Int"/>
    /// </summary>  
    /// <param name="_pos">A position on the board</param>
    /// <returns><see cref="BoardCell"/> of the specified location</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// If <paramref name="_pos"/> is out of bounds
    /// </exception>
    public Tile At(Vector2Int _pos)
    {
        if ((_pos.x >= 0 && _pos.x < this._width) && (_pos.y >= 0 && _pos.y < this._height))
        {
            return _cells[_pos.x, _pos.y];
        }
        else
        {
            Debug.LogError($"Position must be between (0, 0) and ({this._width}, {this._height})");
            throw new ArgumentOutOfRangeException($"Position must be between (0, 0) and ({this._width}, {this._height})");
        }
    }

    /// <summary>
    /// Get a specific cell on the <see cref="Board"/> from a <see cref="Vector2Int"/>
    /// </summary>
    /// <param name="_pos">A position on the board</param>
    /// <returns><see cref="BoardCell"/> of the specified location</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// If <paramref name="_pos"/> is out of bounds
    /// </exception>
    public Tile At(int x, int y)
    {
        if ((x >= 0 && x < this._width) && (y >= 0 && y < this._height))
        {
            return _cells[x, y];
        }
        else
        {
            Debug.LogError($"Position must be between (0, 0) and ({this._width}, {this._height})");
            throw new ArgumentOutOfRangeException($"Position must be between (0, 0) and ({this._width}, {this._height})");
        }
    }

    /// <summary>
    /// Set a specific <see cref="BoardCell"/> to a new <see cref="SpotType"/> from a <see cref="Vector2Int"/>. 
    /// </summary>
    /// <para name="_pos">A position on the <see cref="Board"/></para>
    /// <para name="_type">The new <see cref="SpotType"/></para>
    /// <exception cref="ArgumentOutOfRangeException">If <paramref name="_pos"/> is out of bounds</exception>
    public void Set(Vector2Int _pos, Tile _tile)
    {
        if ((_pos.x >= 0 && _pos.x < this._width) && (_pos.y >= 0 && _pos.y < this._height))
        {
            this._cells[_pos.x, _pos.y] = _tile;
        }
        else
        {
            Debug.LogError($"Position must be between (0, 0) and ({this._width}, {this._height})");
            throw new ArgumentOutOfRangeException($"Position must be between (0, 0) and ({this._width}, {this._height})");
        }
    }

    /// <summary>
    /// Set a specific <see cref="BoardCell"/> to a new <see cref="SpotType"/> from a <see cref="Vector2Int"/>. 
    /// </summary>
    /// <para name="_pos">A position on the <see cref="Board"/></para>
    /// <para name="_type">The new <see cref="SpotType"/></para>
    /// <exception cref="ArgumentOutOfRangeException">If <paramref name="_pos"/> is out of bounds</exception>
    public void Set(int x, int y, Tile _tile)
    {
        if ((x >= 0 && x < this._width) && (y >= 0 && y < this._height))
        {
            this._cells[x, y] = _tile;
        }
        else
        {
            Debug.LogError($"Position must be between (0, 0) and ({this._width}, {this._height})");
            throw new ArgumentOutOfRangeException($"Position must be between (0, 0) and ({this._width}, {this._height})");
        }
    }

    /// <summary>
    /// Moves a spcific cat to the farthest empty spot inbetween Destination and cats location
    /// </summary>
    /// <param name="ItemMoveDistance">Number of tiles the Item moves on the <see cref="Board"/></param>
    /// <param name="Destination">Farthest Location on the <see cref="Board"/> that the cat will move</param>
    /// <param name="ListPos">Position in the list that the moving cat is</param>
    public void CheckMovement(int ItemMoveDistance, Vector2Int Destination, int ListPos)
    {
        Vector2Int Cat = Cats[ListPos].Position;
        if (Cat.x == Destination.x)
        {
            if(Cat.y > Destination.y)
            {
                // Moves down
                if (Destination.y < 0)
                {
                    // Stops movement when at bottom of board
                    Destination.y = 0;
                } 
                for (int y = Cat.y - 1; y >= Destination.y; y--)
                {
                    //checks what the destination is
                    if (_cells[Destination.x, y] != null)
                    {
                        if (_cells[Destination.x, y].Is<Trap>() || _cells[Destination.x, y].Is<Item>() || _cells[Destination.x, y].Is<Cat>())
                        {
                            //if the destination has something the cat cant be on, make sure it does step on it
                            Destination.y = y + 1;
                            if (_cells[Destination.x, y].name == "Post")
                            {
                                Destination.y = y;
                                break;
                            }
                            if (_cells[Destination.x, y].name == "Bed")
                            {
                                Destination.y = y;
                                Cats[ListPos].Sleeping = true;
                                break;
                            }
                            if (_cells[Destination.x, y].name == "Toy")
                            {
                                //allows cat to move on cat pen
                                Destination.y = y;
                                break;
                            }
                            break;
                        }
                        else if (_cells[Destination.x, y].Is<CatPen>() || _cells[Destination.x, y].name == "Toy")
                        {
                            //allows cat to move on cat pen
                            Destination.y = y;
                            break;
                        }
                    }
                }
                GameManager.Instance._matchManager.MoveCat(Vector2Int.down, At(Cat), Destination, ListPos);
            }
            else
            {
                // Moves Up
                if (Destination.y > _height - 1)
                {
                    // Stops movement at top of board
                    Destination.y = _height - 1;
                }
                for (int y = Cat.y + 1; y <= Destination.y; y++)
                {
                    //checks what the destination is
                    if (_cells[Destination.x, y] != null)
                    {
                        if (_cells[Destination.x, y].Is<Trap>() || _cells[Destination.x, y].Is<Item>() || _cells[Destination.x, y].Is<Cat>())
                        {
                            //if the destination has something the cat cant be on, make sure it does step on it
                            Destination.y = y - 1;
                            if(_cells[Destination.x, y].name == "Post")
                            {
                                Destination.y = y;
                                break;
                            }
                            if (_cells[Destination.x, y].name == "Bed")
                            {
                                Destination.y = y;
                                Cats[ListPos].Sleeping = true;
                                break;
                            }
                            if (_cells[Destination.x, y].name == "Toy")
                            {
                                //allows cat to move on cat pen
                                Destination.y = y;
                                break;
                            }
                            break;
                        }
                        else if (_cells[Destination.x, y].Is<CatPen>())
                        {
                            //allows cat to move on cat pen
                            Destination.y = y;
                            break;
                        }
                    }
                }
                GameManager.Instance._matchManager.MoveCat(Vector2Int.up, At(Cat), Destination, ListPos);
            }
        }
        else //if Cat.y == Destination.y
        {
            // Moves Left
            if (Cat.x > Destination.x)
            {
                if (Destination.x < 0)
                {
                    // Stops at left Side of board
                    Destination.x = 0;
                }
                for (int x = Cat.x - 1; x >= Destination.x; x--)
                {
                    //checks what the destination is
                    if (_cells[x, Destination.y] != null)
                    {
                        if (_cells[x, Destination.y].Is<Trap>() || _cells[x, Destination.y].Is<Item>() 
                            || _cells[x, Destination.y].Is<Cat>())
                        {
                            //if the destination has something the cat cant be on, make sure it does step on it
                            Destination.x = x + 1;
                            if (_cells[x, Destination.y].name == "Post")
                            {
                                Destination.x = x;
                                break;
                            }
                            if (_cells[x, Destination.y].name == "Bed")
                            {
                                Destination.x = x;
                                Cats[ListPos].Sleeping = true;
                                break;
                            }
                            if (_cells[x, Destination.y].name == "Toy")
                            {
                                //allows cat to move on cat pen
                                Destination.x = x;
                                break;
                            }
                            break;
                        }
                        else if (_cells[x, Destination.y].Is<CatPen>())
                        {
                            //allows cat to move on cat pen
                            Destination.x = x;
                            break;
                        }
                    }
                }
               GameManager.Instance._matchManager.MoveCat(Vector2Int.left, At(Cat), Destination, ListPos);
            }
            else 
            {
                // Moves Right
                if (Destination.x > _width - 1)
                {
                    // Stops at Right Side of board
                    Destination.x = _width - 1;
                }
                for (int x = Cat.x + 1; x <= Destination.x; x++)
                {
                    //checks what the destination is
                    if (_cells[x, Destination.y] != null)
                    {
                        if (_cells[x, Destination.y].Is<Trap>() || _cells[x, Destination.y].Is<Item>() || _cells[x, Destination.y].Is<Cat>())
                        {
                            //if the destination has something the cat cant be on, make sure it does step on it
                            Destination.x = x - 1;
                            if (_cells[x, Destination.y].name == "Post")
                            {
                                Destination.x = x;
                                break;
                            }
                            if (_cells[x, Destination.y].name == "Bed")
                            {
                                Destination.x = x;
                                Cats[ListPos].Sleeping = true;
                                break;
                            }
                            if (_cells[x, Destination.y].name == "Toy")
                            {
                                //allows cat to move on cat pen
                                Destination.x = x;
                                break;
                            }
                            break;
                        }
                        else if(_cells[x, Destination.y].Is<CatPen>())
                        {
                            //allows cat to move on cat pen
                            Destination.x = x;
                            break;
                        }
                    }
                }
                GameManager.Instance._matchManager.MoveCat(Vector2Int.right, At(Cat), Destination, ListPos);
            }
        }
        if(Cats[ListPos] != null)
        {
            Cats[ListPos].Position = Destination;
        }
    }

 

    /// <summary>
    /// Gets the Width of the Board
    /// </summary>
    /// <returns>Width of the Board</returns>
    public int GetWidth()
    {
        return _width;
    }

    /// <summary>
    /// Gets the Height of the Board
    /// </summary>
    /// <returns>Height of the Board</returns>
    public int GetHeight()
    {
        return _height;
    }
}
