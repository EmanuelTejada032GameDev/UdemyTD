using Cinemachine;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public static CameraHandler Instance { get; private set; }

    private float _cameraMoveSpeed = 60f;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;

    private float _edgeScrollingSize = 30f;
    private bool _edgeScrolling;

    private float _orthographicSize;
    private float _targetOrtographicSize;

    private float _zoomAmount = 2f;
    private float _zoomSpeed = 5f;
    [SerializeField] private float _maxZoomIn = 10f;
    [SerializeField] private float _maxZoomOut = 30f;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _orthographicSize = _virtualCamera.m_Lens.OrthographicSize;
        _targetOrtographicSize = _orthographicSize;
    }

    private void Update()
    {
        HandleMovement();
        HandleZoom();
    }

    private void HandleZoom()
    {
        _targetOrtographicSize -= Input.mouseScrollDelta.y * _zoomAmount;
        _targetOrtographicSize = Mathf.Clamp(_targetOrtographicSize, _maxZoomIn, _maxZoomOut);
        _orthographicSize = Mathf.Lerp(_orthographicSize, _targetOrtographicSize, _zoomSpeed * Time.deltaTime);
        _virtualCamera.m_Lens.OrthographicSize = _orthographicSize;
    }

    private void HandleMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (_edgeScrolling)
        {
            if (Input.mousePosition.x > (Screen.width - _edgeScrollingSize))
            {
                x = 1f;
            }

            if (Input.mousePosition.x < _edgeScrollingSize)
            {
                x = -1f;
            }


            if (Input.mousePosition.y > (Screen.height - _edgeScrollingSize))
            {
                y = 1f;
            }

            if (Input.mousePosition.y < _edgeScrollingSize)
            {
                y = -1f;
            }
        }

        Vector3 moveDirection = new Vector3(x, y).normalized;
        transform.position += moveDirection * _cameraMoveSpeed * Time.deltaTime;
    }

    public void SetEdgeScrolling(bool newValue)
    {
        _edgeScrolling = newValue;
    }

    public bool GetScrollingValue() =>  _edgeScrolling;


}
