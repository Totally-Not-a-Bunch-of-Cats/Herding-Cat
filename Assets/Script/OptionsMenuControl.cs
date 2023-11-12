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
    public Slider MusicAdjustSlider;
    public TMP_Text MusicAdjustText;
    public Slider SFXAdjustSlider;
    public TMP_Text SFXAdjustText;
    public Button[] ItemAffectButtons;
    public Button[] MusicToggleButtons;
    public Button[] SFXToggleButtons;
    public Button[] SkipVideoButtons;

    private readonly string SpeedAdjustWording = "Cat Speed Augment: ";
    private readonly string MusicAdjustWording = "Music Level: ";
    private readonly string SFXAdjustWording = "Sound Effect Level: ";

    /// <summary>
    /// Sets the UI objects to the value that has been saved
    /// </summary>
    private void Awake()
    {
        SpeedAdjustSlider.value = (GameManager.Instance.CatSpeed - .75f) / .25f;
        MusicAdjustSlider.value = GameManager.Instance.musicVolume * 5;
        SFXAdjustSlider.value = GameManager.Instance.SFXVolume * 5;
        ItemAffectButtons[1].interactable = !GameManager.Instance.ItemIndicators;
        ItemAffectButtons[0].interactable = GameManager.Instance.ItemIndicators;
        if (MusicToggleButtons[0] != null)
        {
            MusicToggleButtons[1].interactable = !GameManager.Instance.MusicToggle;
            MusicToggleButtons[0].interactable = GameManager.Instance.MusicToggle;
        }
        if (SFXToggleButtons[0] != null)
        {
            SFXToggleButtons[1].interactable = !GameManager.Instance.SFXToggle;
            SFXToggleButtons[0].interactable = GameManager.Instance.SFXToggle;
        }
        if (SkipVideoButtons[0] != null)
        {
            SkipVideoButtons[1].interactable = !GameManager.Instance.SkipForcedVids;
            SkipVideoButtons[0].interactable = GameManager.Instance.SkipForcedVids;
        }
    }

    /// <summary>
    /// Changes the speed of cat movement
    /// </summary>
    public void SpeedChange()
    {
        float AgmentAmount = SpeedAdjustSlider.value + 1 - (.25f + (0.75f * SpeedAdjustSlider.value));
        SpeedAdjustText.text = SpeedAdjustWording + AgmentAmount;
        GameManager.Instance.CatSpeed = AgmentAmount;
        if(GameManager.Instance.PlayerPrefsTrue == true)
        {
            GameManager.Instance._PlayerPrefsManager.SaveFloat("CatSpeed", AgmentAmount);
        }
    }

    public void MusicChange()
    {
        float AgmentAmount = MusicAdjustSlider.value / 5;
        MusicAdjustText.text = MusicAdjustWording + AgmentAmount;
        GameManager.Instance.musicVolume = AgmentAmount;
        if (GameManager.Instance.PlayerPrefsTrue == true)
        {
            GameManager.Instance._PlayerPrefsManager.SaveFloat("MusicVolume", AgmentAmount);
        }
    }

    public void SFXChange()
    {
        float AgmentAmount = SFXAdjustSlider.value / 5;
        SFXAdjustText.text = SFXAdjustWording + AgmentAmount;
        GameManager.Instance.SFXVolume = AgmentAmount;
        if (GameManager.Instance.PlayerPrefsTrue == true)
        {
            GameManager.Instance._PlayerPrefsManager.SaveFloat("SFXVolume", AgmentAmount);
        }
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
        if (GameManager.Instance.PlayerPrefsTrue == true)
        {
            GameManager.Instance._PlayerPrefsManager.SaveBool("ItemIndicators", ItemAffect);
        }
    }
    public void MusicToggle(bool Music)
    {
        GameManager.Instance.MusicToggle = Music;
        if (Music)
        {
            MusicToggleButtons[1].interactable = !Music;
            MusicToggleButtons[0].interactable = Music;
        }
        else
        {
            MusicToggleButtons[0].interactable = Music;
            MusicToggleButtons[1].interactable = !Music;
        }
        if (GameManager.Instance.PlayerPrefsTrue == true)
        {
            GameManager.Instance._PlayerPrefsManager.SaveBool("MusicToggle", Music);
        }
    }
    public void SFXToggle(bool SFX)
    {
        GameManager.Instance.SFXToggle = SFX;
        if (SFX)
        {
            SFXToggleButtons[1].interactable = !SFX;
            SFXToggleButtons[0].interactable = SFX;
        }
        else
        {
            SFXToggleButtons[0].interactable = SFX;
            SFXToggleButtons[1].interactable = !SFX;
        }
        if (GameManager.Instance.PlayerPrefsTrue == true)
        {
            GameManager.Instance._PlayerPrefsManager.SaveBool("SFXToggle", SFX);
        }
    }

    public void SkipVideoToggle(bool ForcedVideo)
    {
        GameManager.Instance.SkipForcedVids = ForcedVideo;
        if (ForcedVideo)
        {
            SkipVideoButtons[1].interactable = !ForcedVideo;
            SkipVideoButtons[0].interactable = ForcedVideo;
        }
        else
        {
            SkipVideoButtons[0].interactable = ForcedVideo;
            SkipVideoButtons[1].interactable = !ForcedVideo;
        }
        if (GameManager.Instance.PlayerPrefsTrue == true)
        {
            GameManager.Instance._PlayerPrefsManager.SaveBool("SkipForcedVids", ForcedVideo);
        }
    }
}
