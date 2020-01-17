using System;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField] AudioMixerGroup audioMixerGroup;

    [SerializeField] Sound[] sounds;

    private void Awake()
    {
        InitiateSingletonSoundManager();
        DontDestroyOnLoad(gameObject);

        SoundSetup();
    }

    public void Start()
    {
        Play("Background Music");
    }

    private void InitiateSingletonSoundManager()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void SoundSetup()
    {
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.outputAudioMixerGroup = audioMixerGroup;
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.mute = sound.mute;
            sound.source.loop = sound.loop;
            sound.source.spatialBlend = sound.spatialBlend;
            sound.source.reverbZoneMix = sound.reverbZoneMix;
        }
    }

    public void Play(string name)
    {
        Sound sound = Array.Find(sounds, s => s.name == name);
        if (sound == null)
        {
            Debug.Log("Could not find sound with name \"" + sound.name + "\"");
        }
        sound.source.Play();
    }

    public void PlayButtonSound()
    {
        Play("Click");
    }

    public void SetVolume(float volume)
    {
        audioMixerGroup.audioMixer.SetFloat("SoundVolume", Mathf.Log10(volume) * 20);
    }

    public float GetVolume()
    {
        return (sounds[0] != null) ? sounds[0].volume : 1f;
    }
}

[System.Serializable]
public class Sound
{
    public string name = "Sound Name";
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume = 1;
    [Range(0.1f, 3f)]
    public float pitch = 1;
    [Range(0f, 1f)]
    public float spatialBlend = 1;
    [Range(0f, 1.1f)]
    public float reverbZoneMix = 1;
    [HideInInspector]
    public AudioSource source;
    public bool mute = false;
    public bool loop = false;
}