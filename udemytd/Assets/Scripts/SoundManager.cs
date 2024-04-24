using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public enum Sound
    {
        BuildingPlaced,
        BuildingDamaged,
        BuildingDestroyed,
        EnemyHit,
        EnemyDie,
        GameOver,
        EnemyWaveStarting
    }

    private AudioSource _audioSource;
    private Dictionary<Sound, AudioClip> _soundAudioClipDictionary;

    private float _currentVolume = 0.5f;

    private void Awake()
    {
        Instance = this;
        _audioSource = GetComponent<AudioSource>();
        _soundAudioClipDictionary = new Dictionary<Sound, AudioClip>();

        _currentVolume = PlayerPrefs.GetFloat("soundVolumeValue", .5f);


        foreach (Sound sound in Enum.GetValues(typeof(Sound)))
        {
            _soundAudioClipDictionary[sound] = Resources.Load<AudioClip>(sound.ToString());
        }
    }

    public void PlaySound(Sound sound, float? volumeScale = null)
    {
        _audioSource.PlayOneShot(_soundAudioClipDictionary[sound], _currentVolume);
    }

    public void IncreaseVolume()
    {
        _currentVolume += 0.1f;
        _currentVolume = Mathf.Clamp01(_currentVolume);
        PlayerPrefs.SetFloat("soundVolumeValue", _currentVolume);

    }

    public void DecreaseVolume()
    {
        _currentVolume -= 0.1f;
        _currentVolume = Mathf.Clamp01(_currentVolume);
        PlayerPrefs.SetFloat("soundVolumeValue", _currentVolume);

    }

    public float GetVolume()
    {
        return _currentVolume;
    }
}
