/** @Author Zachary Boehm */

using System;
using System.Collections.Generic;
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
    public List<Vector2Int> Cats;

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
        Cats = new List<Vector2Int>();
        if (Tiles != null)
        {
            foreach (PosTile tile in Tiles)
            {
                Set(tile.Position, tile.Slate);
                if (tile.Slate.Is<Cat>())
                {
                    Cats.Add(tile.Position);
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
        this._height = dimensions.y;
        this._width = dimensions.x;
        _cells = new Tile[this._height, this._width];
        Cats = new List<Vector2Int>();
        if (Tiles != null)
        {
            foreach (PosTile tile in Tiles)
            {
                _cells[tile.Position.x, tile.Position.y] = tile.Slate;
                if (tile.Slate.Is<Cat>())
                {
                    Cats.Add(tile.Position);
                }
            }
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
            //Debug.Log(x + " " + y);
            Debug.LogError($"Position must be between (0, 0) and ({this._width}, {this._height})");
            throw new ArgumentOutOfRangeException($"Position must be between (0, 0) and ({this._width}, {this._height})");
        }
    }
    public void CheckMovement(Vector2Int Cat, int ItemMoveDistance, Vector2Int Destination)
    {
        Vector2Int RealMovement = new Vector2Int(0,0);
        if (Cat.x == Destination.x)
        {
            if(Cat.y > Destination.y)
            {
                //go left
                // checks for left side of board
                if (Destination.y < 0)
                {
                    Destination.y = 0;
                } 
                for (int y = Cat.y; y >= Destination.y; y--)
                {
                    if(_cells[Destination.x, y] != null)
                    {
                        if (_cells[Destination.x, y].Is<Trap>())
                        {
                            RealMovement.y = y + 1;
                            break;
                        }
                        else
                        {
                            RealMovement.y = y;
                        }
                    }
                }
                MoveCat(Vector2Int.down, At(Cat), Cat, Destination);
            }
            else
            {
                MoveCat(Vector2Int.up, At(Cat), Cat, Destination);
            }
        }
        else //if Cat.y == Destination.y
        {
            if (Cat.x > Destination.x)
            {
                MoveCat(Vector2Int.left, At(Cat), Cat, Destination);
            }
            else 
            {
                MoveCat(Vector2Int.right, At(Cat), Cat, Destination);
            }
        }

        //make sure cat doesnt leave board
    }

    void MoveCat(Vector2Int Direction, Tile Cat, Vector2Int CatPos, Vector2Int FinialDestination)
    {
        if (Direction.x > 0)
        {
            for (int i = CatPos.x; i < FinialDestination.x; i++)
            {
                Cat.TileObject.localPosition += new Vector3(Direction.x, Direction.y, 0);
                //add a short wait function 
            }
        }
        else if (Direction.y > 0)
        {
            for (int i = CatPos.y; i < FinialDestination.y; i++)
            {
                Cat.TileObject.localPosition += new Vector3(Direction.x, Direction.y, 0);
            }
        }
        else if (Direction.x < 0)
        {
            for (int i = CatPos.x; i > FinialDestination.x; i--)
            {
                Cat.TileObject.localPosition += new Vector3(Direction.x, Direction.y, 0);
            }
        }
        else
        {
            for (int i = CatPos.y; i > FinialDestination.y; i--)
            {
                Cat.TileObject.localPosition += new Vector3(Direction.x, Direction.y, 0);
            }
        }
        //move cat in data structure all at once
        Set(FinialDestination, Cat);
        Set(CatPos, null);
    }



    
    public int GetWidth()
    {
        return _width;
    }

    public int GetHeight()
    {
        return _height;
    }
}
