using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    public static SoundManager Instance { get { return instance; } }

    [SerializeField]
    private AudioSource soundEffect;

    [SerializeField]
    private AudioSource soundMusic;

    [SerializeField]
    private SoundType[] sounds;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic(global::Sounds.MUSIC);
    }
    public void PlaySound(Sounds sound)
    {
        AudioClip clip = GetSoundClip(sound);
        if (clip != null)
        {
            soundEffect.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("Sound Couldn't be laoded");
        }

    }

    public void PlayMusic(Sounds sound)
    {
        AudioClip clip = GetSoundClip(sound);
        if (clip != null)
        {
            soundMusic.clip = clip;
            soundMusic.Play();
        }
        else
        {
            Debug.LogWarning("Sound Couldn't be laoded");
        }


    }


    private AudioClip GetSoundClip(Sounds sound)
    {
        SoundType item = Array.Find(sounds, i => i.soundType == sound);

        if (item != null)
            return item.soundClip;
        return null;

    }
}

[Serializable]
public class SoundType
{
    public Sounds soundType;
    public AudioClip soundClip;
}


public enum Sounds
{
    MUSIC,
    GAME_FINISHED,
    //POWERUP_SOUND,
    DEATH_SOUND,
    BUTTON_CLCK,
    SHOOTING,
}