using UnityEngine;

/// <summary>
/// Cat data class.
/// </summary>
[CreateAssetMenu(fileName = "Cat", menuName = "ScriptableObjects/Tiles/New Cat", order = 2)]
public class Cat : Tile
{
    [Tooltip("How many tiles the cat can travel.")]
    public int Distance;

    [Tooltip("How many turns left the cat's turn is skipped.")]
    public int Skipped;

    [Tooltip("The speed of the cat.")]
    public int Speed;

    [Tooltip("The animation controller of the cat.")]
    public RuntimeAnimatorController AnimationController;

    [Tooltip("The acessory 1 of the cat.")]
    public GameObject Acessory1;

    [Tooltip("The acessory 2 of the cat.")]
    public GameObject Acessory2;
}
