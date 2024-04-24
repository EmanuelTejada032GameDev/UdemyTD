using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    private AudioSource _audioSource;

    private float _currentVolume = 0.5f;


    private void Awake()
    {
        Instance = this;
        _audioSource = transform.GetComponent<AudioSource>();
        _currentVolume = PlayerPrefs.GetFloat("musicVolumeValue", .5f);
        _audioSource.volume = _currentVolume;
    }

    public void IncreaseVolume()
    {
        _currentVolume += 0.1f;
        _currentVolume = Mathf.Clamp01(_currentVolume);
        _audioSource.volume = _currentVolume;
        PlayerPrefs.SetFloat("musicVolumeValue", _currentVolume);
    }

    public void DecreaseVolume()
    {
        _currentVolume -= 0.1f;
        _currentVolume = Mathf.Clamp01(_currentVolume);
        _audioSource.volume = _currentVolume;
        PlayerPrefs.SetFloat("musicVolumeValue", _currentVolume);

    }

    public float GetVolume()
    {
        return _currentVolume;
    }
}
