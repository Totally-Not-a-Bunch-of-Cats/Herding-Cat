/*
 * Author: 
 * Version: 
 */
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;



/// <summary>
/// 
/// </summary>
public class RoundManager : MonoBehaviour
{
    //-------------------------------------------
    // Varables
    //-------------------------------------------

    public enum Direction {Right, Left, Up, Down};

    private enums.spotType[,] Board;
    private Vector2Int BoardSize;
    private int ItemPlaced = 0;
    private int RoundPassed = 0;

    //-------------------------------------------
    // Functions
    //-------------------------------------------

    /// <summary>
    /// is triggured when the player hits the end turn button. And triggures all of the chained items
    /// </summary>
    void EndTurn()
    {

    }


    /// <summary>
    /// better way to do it
    /// </summary>
    /// <param name="Movement"></param>
    /// <param name="CoOrds"></param>
    /// <returns></returns>
    public Vector2Int CheckMovement(Vector2Int Movement, Vector2Int CoOrds)
    {

        Direction direction = CheckDirection(Movement);

        if (direction == Direction.Up || direction == Direction.Right)
        {
            return new Vector2Int(math.min(Movement.x, BoardSize.x - CoOrds.x), math.min(Movement.y, BoardSize.y - CoOrds.y));
        }
        else
        {
            return new Vector2Int(math.max(Movement.x, 0 - CoOrds.x), math.max(Movement.y, 0 - CoOrds.y));
        }
    }

    void CaculateStars()
    {

    }

    /// <summary>
    /// checks which direction the cat is moving for check movement
    /// </summary>
    Direction CheckDirection(Vector2Int DirectionCords)
    {
        if(DirectionCords.x != 0 && DirectionCords.y != 0)
        {
            if(DirectionCords.x == 0)
            {
                if(DirectionCords.y > 0)
                {
                    return Direction.Up;
                }
                else
                { 
                    return Direction.Down;
                }
            }
            else
            {
                if(DirectionCords.x > 0)
                {
                    return Direction.Right;
                }
                else
                {
                    return Direction.Left;
                }
            }
            
        }
        else
        {
            return 0;
        }
    }
}

/*
 *     public Vector2Int CheckMovement(Vector2Int Movement, Vector2Int Cords)
    {
        Direction TheWay;
        Vector2Int RealMovement;
        RealMovement = Movement;
        Vector2Int TempCords = Cords;      
        TempCords += Movement;
        TheWay = CheckDirection(RealMovement);
        bool OutBounds = Cords.x + RealMovement.x > BoardSize.x || Cords.y + RealMovement.y > BoardSize.y ||
            Cords.x + RealMovement.x < 0 || Cords.y + RealMovement.y < 0;


        while (RealMovement != Vector2Int.zero)
        {
            if (Board[TempCords.x, TempCords.y] == enums.spotType.Blank && !OutBounds)
            {
                return RealMovement;
            }
            else
            {
                switch (TheWay)
                {
                    case Direction.Right:
                        RealMovement.x -= 1;
                        break;
                    case Direction.Left:
                        RealMovement.x += 1;
                        break;
                    case Direction.Up:
                        RealMovement.y -= 1;
                        break;
                    case Direction.Down:
                        RealMovement.y += 1;
                        break;
                }
            }
        }
        return Vector2Int.zero;
    }
 */