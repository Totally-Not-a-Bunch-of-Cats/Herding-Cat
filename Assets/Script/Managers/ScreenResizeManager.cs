using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls being able to scale to the size of the screen
/// </summary>
public class ScreenResizeManager : MonoBehaviour
{
    public enum Rotation {Landscape, Portrait }

    public Rotation CurrentRotation;
    public float TileSize;

    // Start is called before the first frame update
    void Start()
    {
        if (Screen.height > Screen.width)
        {
            CurrentRotation = Rotation.Portrait;
        } else
        {
            CurrentRotation = Rotation.Landscape;
        }
    }

    public void ScaleBoard ()
    {
        Debug.Log(CurrentRotation);
        if (CurrentRotation == Rotation.Landscape)
        {
            TileSize = Screen.height / (GameManager.Instance._matchManager.BoardSize[0] + 1);
        } else
        {
            TileSize = Screen.width / (GameManager.Instance._matchManager.BoardSize[1] + 1);
        }
        GameManager.Instance._matchManager.BoardTileMap.GetComponentInParent<Grid>().cellSize = new Vector3(TileSize, TileSize, 1);
    }


    // Update is called once per frame
    void Update()
    {
    }
}
