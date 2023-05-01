using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds info related to the Help screen entry
/// </summary>
[System.Serializable]
public class HelpInfo
{
    public string name = "";
    public string Description = "";
    public Texture Video;
    public Sprite Icon;
    public Vector3 IconScale;
}
