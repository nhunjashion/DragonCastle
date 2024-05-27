using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; set; }

    [Header("---- Audio Source ----")]
    public AudioSource bgmSourceForest;
    public AudioSource bgmSourceCastleOn;
    public AudioSource bgmSourceCastleUnder;
    public AudioSource bgmSourceMenu;
    public AudioSource SfxSource;

    [Header("---- Audio Clip ----")]
    public AudioClip bgmForest;
    public AudioClip bgmCastleOn;
    public AudioClip bgmCastleUnder;
    public AudioClip bgmMainMenu;
    public AudioClip endGame;
    //public AudioClip waterfall;

    public AudioClip swordSlash;
    public AudioClip jump;
    public AudioClip run;
    public AudioClip dash;
    public AudioClip bash;
    public AudioClip ultimate;
    public AudioClip keyCollect;
    public AudioClip healing;
    public AudioClip dead;
    public AudioClip aBTreeActive;


    [Header("---- Other ----")]
    public GameObject player;

    private void Awake()
    {
        AudioManager.instance = this;
    }



    public void PlaySFX(AudioClip clip)
    {
        SfxSource.PlayOneShot(clip);
    }



    public void PlayBgmMusicForest()
    {
        bgmSourceForest.clip = bgmForest;
        bgmSourceForest.Play();
    }
    public void PlayBgmMusicCastleOn()
    {
        bgmSourceCastleOn.clip = bgmCastleOn;
        bgmSourceCastleOn.Play();
    }
    public void PlayBgmMusicCastleUnder()
    {
        bgmSourceCastleUnder.clip = bgmCastleUnder;
        bgmSourceCastleUnder.Play();
    }

    public void PlayMainMenuBGM()
    {
        bgmSourceMenu.clip = bgmMainMenu;
        bgmSourceMenu.Play();
    } 
    public void PlayEndMenuBGM()
    {
        bgmSourceMenu.clip = endGame;
        bgmSourceMenu.Play();
    }
}
