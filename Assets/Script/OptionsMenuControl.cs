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
    }

    public void SpeedChange()
    {
        float AgmentAmount = SpeedAdjustSlider.value + 1 - (.25f + (0.75f * SpeedAdjustSlider.value));
        SpeedAdjustText.text = SpeedAdjustWording + AgmentAmount;
    }

    public void AffectToggle(bool ItemAffect)
    {
        //GameManager.Instance.ShowAffects = ItemAffect;
        if (ItemAffect)//true(1)
        {
            //0: true, 1: false
            ItemAffectButtons[1].interactable = !ItemAffect;
            ItemAffectButtons[0].interactable = ItemAffect;
        }
        else
        {
            //0: false, 1: true
            ItemAffectButtons[0].interactable = ItemAffect;
            ItemAffectButtons[1].interactable = !ItemAffect;
        }
    }
}
