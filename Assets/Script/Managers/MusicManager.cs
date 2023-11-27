using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public GameObject AudioPlayer;
    public AudioClip MainMenuTrack;
    public int CurrentLevelTrack = -1;
    [SerializeField] float TrackProgression = 0;
    public List<AudioClip> AudioTracks;
    public List<AudioClip> AudioEffect;
    float AudioLevel;
    [SerializeField] bool SwitchFromMainMenuMusic = true;
    [SerializeField] bool FirstPlay = true;
    [SerializeField] bool SoundChanging = true;



    private void Start()
    {
        if (CurrentLevelTrack == -1)
        {
            RandomTrack();
        }
    }
    private void LateUpdate()
    {
        if (AudioPlayer == null)
        {
            AudioPlayer = GameObject.FindGameObjectWithTag("Music");
        }
        if (AudioPlayer != null && SceneManager.GetActiveScene().name == "Main Menu" && FirstPlay)
        {
            FirstPlay = false;
            PlayMenuSong();
        }
        if (SceneManager.GetActiveScene().name == "Match" && SwitchFromMainMenuMusic)
        {
            StartCoroutine(FadeOut());
        }
        if (SceneManager.GetActiveScene().name == "Main Menu" && !SwitchFromMainMenuMusic)
        {
            StartCoroutine(FadeOutMainMenu());
        }
        if (AudioPlayer != null && SceneManager.GetActiveScene().name != "Main Menu" && AudioPlayer.GetComponent<AudioSource>().clip)
        {
            TrackProgression = AudioPlayer.GetComponent<AudioSource>().time;
            if (AudioPlayer.GetComponent<AudioSource>().clip.length <= TrackProgression)
            {
                NextTrack();
            }
        }
        if (AudioPlayer != null && SoundChanging)
        {
            GetAudioLevel();
        }
    }

    /// <summary>
    /// gets the audio level from the game manager and adjust it
    /// </summary>
    public void GetAudioLevel()
    {
        AudioLevel = GameManager.Instance.musicVolume;
        AudioPlayer.GetComponent<AudioSource>().volume = AudioLevel;
    }

    public void Mute()
    {
        if(GameManager.Instance.MusicToggle == true)
        {
            GameManager.Instance.musicVolume = 1;
            AudioPlayer.GetComponent<AudioSource>().volume = 1;
        }
        else
        {
            GameManager.Instance.musicVolume = 0;
            AudioPlayer.GetComponent<AudioSource>().volume = 0;
        }
    }

    /// <summary>
    /// randomly choses an aduio track
    /// </summary>
    public void RandomTrack()
    {
        CurrentLevelTrack = Random.Range(0, AudioTracks.Count);
    }
    /// <summary>
    /// Plays tracks for the game level
    /// </summary>
    public void PlayTrack()
    {
        SoundChanging = true;
        AudioPlayer.GetComponent<AudioSource>().clip = AudioTracks[CurrentLevelTrack];
        GetAudioLevel();
        AudioPlayer.GetComponent<AudioSource>().Play();
        StartCoroutine(FadeIn());
    }
    /// <summary>
    /// starts the main menu song
    /// </summary>
    public void PlayMenuSong()
    {
        SoundChanging = true;
        AudioPlayer.GetComponent<AudioSource>().clip = MainMenuTrack;
        GetAudioLevel();
        AudioPlayer.GetComponent<AudioSource>().Play();
        StartCoroutine(FadeIn());
    }


    //public void PlayAudioEffect(int location)
    //{
    //    AudioPlayer.GetComponent<AudioSource>().clip = AudioEffect[location];
    //    AudioPlayer.GetComponent<AudioSource>().Play();
    //}


    public void NextTrack()
    {
        if(CurrentLevelTrack + 1 < AudioTracks.Count)
        {
            CurrentLevelTrack += 1;
        }
        else
        {
            CurrentLevelTrack = 0;
        }
        PlayTrack();
    }



    /// <summary>
    /// fades in the aduio
    /// </summary>
    /// <returns></returns>
    IEnumerator FadeIn()
    {
        SoundChanging = false;
        yield return new WaitForSeconds(.5f);
        AudioPlayer.GetComponent<AudioSource>().volume += AudioLevel / 6;
        yield return new WaitForSeconds(.5f);
        AudioPlayer.GetComponent<AudioSource>().volume += AudioLevel / 6;
        yield return new WaitForSeconds(.5f);
        AudioPlayer.GetComponent<AudioSource>().volume += AudioLevel / 6;
        yield return new WaitForSeconds(.5f);
        AudioPlayer.GetComponent<AudioSource>().volume += AudioLevel / 6;
        yield return new WaitForSeconds(.5f);
        AudioPlayer.GetComponent<AudioSource>().volume += AudioLevel / 6;
        yield return new WaitForSeconds(.5f);
        AudioPlayer.GetComponent<AudioSource>().volume += AudioLevel / 6;
        SoundChanging = true;
    }
    /// <summary>
    /// fades the music out to start the level music 
    /// </summary>
    /// <returns></returns>
    public IEnumerator FadeOut()
    {
        SoundChanging = false;
        SwitchFromMainMenuMusic = false;
        yield return new WaitForSeconds(.5f);
        AudioPlayer.GetComponent<AudioSource>().volume -= AudioLevel / 6;
        yield return new WaitForSeconds(.5f);
        AudioPlayer.GetComponent<AudioSource>().volume -= AudioLevel / 6;
        yield return new WaitForSeconds(.5f);
        AudioPlayer.GetComponent<AudioSource>().volume -= AudioLevel / 6;
        yield return new WaitForSeconds(.5f);
        AudioPlayer.GetComponent<AudioSource>().volume -= AudioLevel / 6;
        yield return new WaitForSeconds(.5f);
        AudioPlayer.GetComponent<AudioSource>().volume -= AudioLevel / 6;
        yield return new WaitForSeconds(.5f);
        AudioPlayer.GetComponent<AudioSource>().volume -= AudioLevel / 8;
        PlayTrack();
    }
    /// <summary>
    /// fades the music out to start the main menu music
    /// </summary>
    /// <returns></returns>
    public IEnumerator FadeOutMainMenu()
    {
        SwitchFromMainMenuMusic = true;
        SoundChanging = false;
        yield return new WaitForSeconds(.5f);
        AudioPlayer.GetComponent<AudioSource>().volume -= AudioLevel / 6;
        yield return new WaitForSeconds(.5f);
        AudioPlayer.GetComponent<AudioSource>().volume -= AudioLevel / 6;
        yield return new WaitForSeconds(.5f);
        AudioPlayer.GetComponent<AudioSource>().volume -= AudioLevel / 6;
        yield return new WaitForSeconds(.5f);
        AudioPlayer.GetComponent<AudioSource>().volume -= AudioLevel / 6;
        yield return new WaitForSeconds(.5f);
        AudioPlayer.GetComponent<AudioSource>().volume -= AudioLevel / 6;
        yield return new WaitForSeconds(.5f);
        AudioPlayer.GetComponent<AudioSource>().volume -= AudioLevel / 8;
        PlayMenuSong();
    }
}
