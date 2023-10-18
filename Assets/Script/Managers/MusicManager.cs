using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource AudioPlayer;
    public AudioClip MainMenuTrack;
    public List<AudioClip> AudioTracks;
    public List<AudioClip> AudioEffect;

    public void StartTrack(int location)
    {
        AudioPlayer.clip = AudioEffect[location];
        AudioPlayer.Play();
    }
    public void PlayAudioEffect(int location)
    {
        AudioPlayer.clip = AudioEffect[location];
        AudioPlayer.Play();
    }
}
