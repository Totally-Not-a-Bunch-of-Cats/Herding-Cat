/** @Author Zachary Boehm */

using UnityEngine;

/// <summary>
/// Single cell in a <see cref="Board"/> that contains a <see cref="SpotType"/>.
/// </summary>
public class BoardCell
{
    private SpotType _state = SpotType.Blank;

    /// <summary>
    /// Sets the <see cref="BoardCell"/>'s type to a new <see cref="SpotType"/>.
    /// </summary>
    /// <param name="_type">The <see cref="SpotType"/> to assign to the <see cref="BoardCell"/>.</param>
    public void Set(SpotType _type)
    {
        this._state = _type;
    }

    /// <summary>
    /// Retrieves the <see cref="SpotType"/> from the <see cref="BoardCell"/>
    /// </summary>
    /// <returns>The <see cref="SpotType"/> of this <see cref="BoardCell"/></returns>
    public SpotType Get()
    {
        return this._state;
    }

    /// <summary>
    /// Check if the <see cref="BoardCell"/> is blank.
    /// </summary>
    /// <returns>True if <see cref="_state"/> is <see cref="SpotType.Blank"/></returns>
    public bool IsType(SpotType _cellType)
    {
        if (this._state == _cellType)
        {
            return true;
        }

        return false;
    }
}
