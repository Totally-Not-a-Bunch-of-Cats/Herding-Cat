using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// Controls the Options Menu
/// </summary>
public class OptionsMenuControl : MonoBehaviour
{
    public Slider SpeedAdjustSlider;
    public TMP_Text SpeedAdjustText;
    public Button[] ItemAffectButtons;

    private readonly string SpeedAdjustWording = "Cat Speed Augment: ";

    /// <summary>
    /// Sets the UI objects to the value that has been saved
    /// </summary>
    private void Awake()
    {
        SpeedAdjustSlider.value = (GameManager.Instance.CatSpeed - .75f) / .25f;
        ItemAffectButtons[1].interactable = !GameManager.Instance.ItemIndicators;
        ItemAffectButtons[0].interactable = GameManager.Instance.ItemIndicators;
    }

    /// <summary>
    /// Changes the speed of cat movement
    /// </summary>
    public void SpeedChange()
    {
        float AgmentAmount = SpeedAdjustSlider.value + 1 - (.25f + (0.75f * SpeedAdjustSlider.value));
        SpeedAdjustText.text = SpeedAdjustWording + AgmentAmount;
        GameManager.Instance._PlayerPrefsManager.SaveFloat("CatSpeed", AgmentAmount);
    }
    
    /// <summary>
    /// Controls turning on/off of the item affect indicators
    /// </summary>
    /// <param name="ItemAffect">If the item affect toggle is on or off</param>
    public void AffectToggle(bool ItemAffect)
    {
        GameManager.Instance.ItemIndicators = ItemAffect;
        if (ItemAffect)
        {
            ItemAffectButtons[1].interactable = !ItemAffect;
            ItemAffectButtons[0].interactable = ItemAffect;
        }
        else
        {
            ItemAffectButtons[0].interactable = ItemAffect;
            ItemAffectButtons[1].interactable = !ItemAffect;
        }
        GameManager.Instance._PlayerPrefsManager.SaveBool("ItemIndicators", ItemAffect);
    }
}
