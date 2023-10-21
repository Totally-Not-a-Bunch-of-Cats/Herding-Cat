using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public GameObject AudioPlayer;
    public AudioClip MainMenuTrack;
    public int CurrentLevelTrack;
    public List<AudioClip> AudioTracks;
    public List<AudioClip> AudioEffect;


    public void updateVolume()
    {
        AudioPlayer.GetComponent<AudioSource>().volume = GameManager.Instance.musicVolume;
    }

    public void RandomTrack()
    {
        CurrentLevelTrack = Random.Range(0, AudioTracks.Count);
    }

    public void PlayTrack()
    {
        AudioPlayer = GameObject.Find("MainCamera/AudioSource");
        AudioPlayer.GetComponent<AudioSource>().clip = AudioTracks[CurrentLevelTrack];
        updateVolume();
        AudioPlayer.GetComponent<AudioSource>().Play();
    }

    public void PlayMenuSong()
    {
        AudioPlayer = GameObject.Find("MainCamera/AudioSource");
        AudioPlayer.GetComponent<AudioSource>().clip = MainMenuTrack;
        updateVolume();
        AudioPlayer.GetComponent<AudioSource>().Play();
    }




    public void PlayAudioEffect(int location)
    {
        AudioPlayer = GameObject.Find("MainCamera/AudioSource");
        AudioPlayer.GetComponent<AudioSource>().clip = AudioEffect[location];
        AudioPlayer.GetComponent<AudioSource>().Play();
    }
}
