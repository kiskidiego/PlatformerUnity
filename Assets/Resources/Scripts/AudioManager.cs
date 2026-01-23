using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    class AudioBinding
    {
        public Sounds sound;
        public AudioClip[] clips = new AudioClip[0];
    }
    [SerializeField] AudioBinding[] _audioBindings = new AudioBinding[0];
    [SerializeField] AudioClip _musicClip;
    AudioSource _musicSource;
    AudioSource _sfxSource;
    public static AudioManager Instance = null;
    static float SfxVolume = 1f;
    static float MusicVolume = 1f;
    
    void Awake()
    {
        Instance = this;
        _musicSource = gameObject.AddComponent<AudioSource>();
        _musicSource.loop = true;
        _musicSource.volume = MusicVolume;
        _musicSource.clip = _musicClip;
        _musicSource.Play();
        _sfxSource = gameObject.AddComponent<AudioSource>();
        _sfxSource.loop = false;
        _sfxSource.volume = SfxVolume;
    }
    AudioBinding GetAudioBinding(Sounds sound)
    {
        foreach (var binding in _audioBindings)
        {
            if (binding.sound == sound)
            {
                return binding;
            }
        }
        return null;
    }
    public void PlaySound(Sounds sound)
    {
        AudioBinding binding = GetAudioBinding(sound);
        if (binding != null && binding.clips.Length > 0)
        {
            AudioClip clip = binding.clips[Random.Range(0, binding.clips.Length)];
            _sfxSource.PlayOneShot(clip, SfxVolume);
        }
    }
    void OnDestroy()
    {
        Instance = null;
    }
    public void SetSfxVolume(float volume)
    {
        SfxVolume = volume;
    }
    public void SetMusicVolume(float volume)
    {
        MusicVolume = volume;
        if (_musicSource != null)
        {
            _musicSource.volume = MusicVolume;
        }
    }

    public float GetSfxVolume()
    {
        return SfxVolume;
    }
    public float GetMusicVolume()
    {
        return MusicVolume;
    }
}
