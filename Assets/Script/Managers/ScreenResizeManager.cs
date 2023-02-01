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
    float ScreenScale;
    // Rotation of the device
    public Rotation CurrentRotation;

    /// <summary>
    /// Starts the script
    /// </summary>
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

    /// <summary>
    /// Sets orthographic size of camera to the size requried to make board fill screen
    /// </summary>
    public void ScaleBoard()
    {
        float OrthoSize = (GameManager.Instance._matchManager.BoardSize[1] + 2f) * 0.5f;
        Camera.main.orthographicSize = OrthoSize;

        //ScreenScale = (float)Screen.height / 1000;
        //GameObject.Find("End Turn Button").transform.localScale *= ScreenScale;
        //GameObject.Find("Restart Button").transform.localScale *= ScreenScale;
        //GameObject.Find("Pause Button").transform.localScale *= ScreenScale;
    }

    public void RescaleItem(GameObject button)
    {
        ScreenScale = (float)Screen.height / 1000;
        button.transform.localScale *= ScreenScale;
    }
}
