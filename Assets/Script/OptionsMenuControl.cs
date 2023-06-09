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

    private void Awake()
    {
        SpeedAdjustSlider.value = (GameManager.Instance.SpeedAdjustment - .75f) / .25f;
        ItemAffectButtons[1].interactable = !GameManager.Instance.ShowAffects;
        ItemAffectButtons[0].interactable = GameManager.Instance.ShowAffects;
    }

    public void SpeedChange()
    {
        float AgmentAmount = SpeedAdjustSlider.value + 1 - (.25f + (0.75f * SpeedAdjustSlider.value));
        SpeedAdjustText.text = SpeedAdjustWording + AgmentAmount;
    }

    public void AffectToggle(bool ItemAffect)
    {
        GameManager.Instance.ShowAffects = ItemAffect;
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
    }
}
