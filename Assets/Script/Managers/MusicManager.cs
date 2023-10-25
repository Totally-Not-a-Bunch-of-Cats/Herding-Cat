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
    float AudioLevel;


    public void GetAudioLevel()
    {
        AudioLevel = GameManager.Instance.musicVolume;
        //AudioPlayer.GetComponent<AudioSource>().volume = GameManager.Instance.musicVolume;
    }

    public void RandomTrack()
    {
        CurrentLevelTrack = Random.Range(0, AudioTracks.Count);
    }

    public void PlayTrack()
    {
        AudioPlayer = GameObject.Find("MainCamera/AudioSource");
        AudioPlayer.GetComponent<AudioSource>().clip = AudioTracks[CurrentLevelTrack];
        GetAudioLevel();
        AudioPlayer.GetComponent<AudioSource>().Play();
        StartCoroutine(FadeIn());
    }

    public void PlayMenuSong()
    {
        AudioPlayer = GameObject.Find("MainCamera/AudioSource");
        AudioPlayer.GetComponent<AudioSource>().clip = MainMenuTrack;
        GetAudioLevel();
        AudioPlayer.GetComponent<AudioSource>().Play();
        StartCoroutine(FadeIn());
    }




    public void PlayAudioEffect(int location)
    {
        AudioPlayer = GameObject.Find("MainCamera/AudioSource");
        AudioPlayer.GetComponent<AudioSource>().clip = AudioEffect[location];
        AudioPlayer.GetComponent<AudioSource>().Play();
    }


    IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(1);
        AudioPlayer.GetComponent<AudioSource>().volume += AudioLevel / 4;
        yield return new WaitForSeconds(1);
        AudioPlayer.GetComponent<AudioSource>().volume += AudioLevel / 4;
        yield return new WaitForSeconds(1);
        AudioPlayer.GetComponent<AudioSource>().volume += AudioLevel / 4;
        yield return new WaitForSeconds(1);
        AudioPlayer.GetComponent<AudioSource>().volume += AudioLevel / 4;
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(1);
        AudioPlayer.GetComponent<AudioSource>().volume -= AudioLevel / 4;
        yield return new WaitForSeconds(1);
        AudioPlayer.GetComponent<AudioSource>().volume -= AudioLevel / 4;
        yield return new WaitForSeconds(1);
        AudioPlayer.GetComponent<AudioSource>().volume -= AudioLevel / 4;
        yield return new WaitForSeconds(1);
        AudioPlayer.GetComponent<AudioSource>().volume -= AudioLevel / 4;
    }
}
