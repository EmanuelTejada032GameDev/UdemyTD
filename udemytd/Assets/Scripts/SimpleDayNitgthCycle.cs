using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SimpleDayNitgthCycle : MonoBehaviour
{

    [SerializeField] private Gradient _gradient;
    [SerializeField] private float secondsPerDay; // Represents how much times represents 24h

    private Light2D _2dLight;
    private float _dayTime;
    private float _dayTimeSpeed;



    private void Awake()
    {
        _2dLight = GetComponent<Light2D>();
        _dayTimeSpeed = 1 / secondsPerDay;
    }

    private void Update()
    {
        _dayTime += Time.deltaTime * _dayTimeSpeed;
        _2dLight.color = _gradient.Evaluate(_dayTime % 1f);
    }
}
