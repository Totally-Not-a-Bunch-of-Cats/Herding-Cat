/** @Author Zachary Boehm */

using System;
using UnityEngine;

/// <summary>
/// Grid of <see cref="BoardCell"/>'s.
/// </summary>
[System.Serializable]
public class Board
{
    [SerializeField]
    private BoardCell[,] _cells;

    private readonly int _width;
    private readonly int _height;

    /// <summary>
    /// Initialize 2D board with width and height
    /// </summary>
    /// <param name="Width">Board width</param>
    /// <param name="Height">Board height</param>
    public Board(int Width, int Height)
    {
        this._height = Height;
        this._width = Width;
        _cells = new BoardCell[Width, Height];

        for (int w = 0; w < Width; w++)
        {
            for (int h = 0; h < Height; h++)
            {
                _cells[w, h] = new BoardCell();
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
    public BoardCell GetCell(Vector2Int _pos)
    {
        if ((_pos.x >= 0 && _pos.x < this._width) && (_pos.y >= 0 && _pos.y < this._height))
        {
            return _cells[_pos.x, _pos.y];
        }

        Debug.LogError($"Position must be between (0, 0) and ({this._width}, {this._height})");
        throw new ArgumentOutOfRangeException($"Position must be between (0, 0) and ({this._width}, {this._height})");
    }

    /// <summary>
    /// Set a specific <see cref="BoardCell"/> to a new <see cref="SpotType"/> from a <see cref="Vector2Int"/>. 
    /// </summary>
    /// <para name="_pos">A position on the <see cref="Board"/></para>
    /// <para name="_type">The new <see cref="SpotType"/></para>
    /// <exception cref="ArgumentOutOfRangeException">If <paramref name="_pos"/> is out of bounds</exception>
    public void SetCell(Vector2Int _pos, SpotType _type)
    {
        Debug.Log($"{_pos.x}:{this._width}, {_pos.y}:{this._height}");
        if ((_pos.x >= 0 && _pos.x < this._width) && (_pos.y >= 0 && _pos.y < this._height))
        {
            this._cells[_pos.x, _pos.y].Set(_type);
        }

        Debug.LogError($"Position must be between (0, 0) and ({this._width}, {this._height})");
        throw new ArgumentOutOfRangeException($"Position must be between (0, 0) and ({this._width}, {this._height})");
    }
}
