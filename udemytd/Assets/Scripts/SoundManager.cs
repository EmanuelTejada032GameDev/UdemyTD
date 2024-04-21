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

    private void Awake()
    {
        Instance = this;
        _audioSource = GetComponent<AudioSource>();
        _soundAudioClipDictionary = new Dictionary<Sound, AudioClip>();

        foreach (Sound sound in Enum.GetValues(typeof(Sound)))
        {
            _soundAudioClipDictionary[sound] = Resources.Load<AudioClip>(sound.ToString());
        }
    }

    public void PlaySound(Sound sound, float volumeScale = 1)
    {
        _audioSource.PlayOneShot(_soundAudioClipDictionary[sound], volumeScale);
    }
}
