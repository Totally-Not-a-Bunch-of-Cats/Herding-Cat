using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catmove : MonoBehaviour
{
    void Update()
    {
      if(Input.GetKeyDown(KeyCode.E))
        {
            transform.position += Vector3.left;
        }
    }

    /// <summary>
    /// moves the cat to the left a number of tiles equal to num
    /// </summary>
    /// <param name="Num"></param>
    void MoveLeft(int Num)
    {

    }

    void MoveRight(int Num)
    {

    }

    void MoveUp(int Num)
    {

    }

    void MoveDown(int Num)
    {

    }
}
