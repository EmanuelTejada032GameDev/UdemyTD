using Cinemachine;
using UnityEngine;

public class CameraShakeComponent : MonoBehaviour
{
    public static CameraShakeComponent Instance { get; private set; }

    private CinemachineVirtualCamera _virtualCamera;
    private CinemachineBasicMultiChannelPerlin _cinemachineBasicMultiChannelPerlin;
    private float _timer;
    private float _timerMax;
    private float _startingIntensity;

    private void Awake()
    {
        Instance = this;
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        _cinemachineBasicMultiChannelPerlin = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Update()
    {
        if (_timer < _timerMax)
        {
            _timer += Time.deltaTime;
            float amplitude = Mathf.Lerp(_startingIntensity, 0f, _timer/_timerMax);
            _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = amplitude;
        }
    }

    public void ShakeCamera(float intensity, float timerMax)
    {
        _timerMax = timerMax;
        _timer = 0f;
        _startingIntensity = intensity;
        _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
    }
}
