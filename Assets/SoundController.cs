using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nakama;
using Nakama.TinyJson;
using System.Text;

public class SoundController : MonoBehaviour
{
   
    public float StateFrequency = 0.05f;
    private float stateSyncTimer;
    
    [SerializeField] AudioClip mainTheme;
    [SerializeField] AudioClip dashAudio;
    [SerializeField] AudioClip jumpAudio;
    [SerializeField] AudioClip teleportAudio;
    [SerializeField] AudioClip platformButtonPressedAudio;
    [SerializeField] AudioClip levelEndedAudio;



    public AudioSource mainAudioSource;
    [SerializeField] GameStateManager gameStateManager;
    
    void Start()
    {
   
        mainAudioSource = GetComponent<AudioSource>();
        // PlayMainTheme();
    }
 
  
    
    // public void PlayMainTheme()
    // {
    //     Debug.Log("mainTheme");
    //     mainAudioSource.loop = true;
    //     mainAudioSource.clip = mainTheme;
    //     mainAudioSource.Play();
    // }
    public void PlayDashAudio()
    {
        // Debug.Log("dashAudio");
        mainAudioSource.loop = false;
        mainAudioSource.clip = dashAudio;
        mainAudioSource.PlayOneShot(dashAudio);
        // gameStateManager.SendMatchState(
        //     4, 
        //     MatchDataJson.Sound(mainAudioSource.clip.name));
    }
    public void PlayJumpAudio()
    {
        // Debug.Log("jumpAudio");
        mainAudioSource.loop = false;
        mainAudioSource.clip = jumpAudio;
        mainAudioSource.PlayOneShot(jumpAudio);
        // gameStateManager.SendMatchState(
        //     4, 
        //     MatchDataJson.Sound(mainAudioSource.clip.name));
    }
    public void PlayTeleportAudio()
    {
        // Debug.Log("teleportAudio");
        mainAudioSource.loop = false;
        mainAudioSource.clip = teleportAudio;
        mainAudioSource.PlayOneShot(teleportAudio);
        // gameStateManager.SendMatchState(
        //     4, 
        //     MatchDataJson.Sound(mainAudioSource.clip.name));
    }
    public void PlayPlatformButtonPressedAudio()
    {
        // Debug.Log("buttonPressedAudio");
        mainAudioSource.loop = false;
        mainAudioSource.clip = platformButtonPressedAudio;
        mainAudioSource.PlayOneShot(platformButtonPressedAudio);
        // gameStateManager.SendMatchState(
        //     4, 
        //     MatchDataJson.Sound(mainAudioSource.clip.name));
    }
    public void PlayLevelEndedAudio()
    {
        // Debug.Log("levelEndedAudio");
        mainAudioSource.loop = false;
        mainAudioSource.clip = levelEndedAudio;//send 1 time
        mainAudioSource.PlayOneShot(levelEndedAudio);
        // gameStateManager.SendMatchState(
        //     4, 
        //     MatchDataJson.Sound(mainAudioSource.clip.name));

    }
    



}
