using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource bgmSource;
    public AudioSource seSource;
     
    [SerializeField] AudioClip bgm;
    [SerializeField] AudioClip shotgun;
    [SerializeField] AudioClip playerDead;
    [SerializeField] AudioClip itemDrop;
    [SerializeField] AudioClip sniper;
    [SerializeField] AudioClip zombieDead;
    [SerializeField] AudioClip carbine;
    [SerializeField] AudioClip playerHit;

    private void Awake()
    {
        instance = this;

        Play_BGM();
    }

    public void Play_BGM() { bgmSource.clip = bgm; bgmSource.Play(); }

    public void Play_Shotgun() { seSource.clip = shotgun; seSource.Play(); }
    public void Play_PlayerDead() { seSource.clip = playerDead; seSource.Play(); }
    public void Play_ItemDrop() { seSource.clip = itemDrop; seSource.Play(); }
    public void Play_Sniper() { seSource.clip = sniper; seSource.Play(); }
    public void Play_ZombieDead() { seSource.clip = zombieDead; seSource.Play(); }
    public void Play_Carbine() { seSource.clip = carbine; seSource.Play(); }
    public void Play_PlayerHit() { seSource.clip = playerHit; seSource.Play(); }
}
