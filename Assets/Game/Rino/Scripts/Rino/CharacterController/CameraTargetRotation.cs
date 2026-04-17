using UnityEngine;

public class CameraTargetRotation : MonoBehaviour
{
    #region Fields
    [SerializeField] private PlayerInputReader _playerInputReader;
    [SerializeField] private float _sensitivity = 120f;
    [SerializeField] private float _minPitch = -35f;
    [SerializeField] private float _maxPitch = 70f;
    #endregion
    
    #region Private Fields
    private float _yaw;
    private float _pitch;
    #endregion

    #region Unity Callbacks
    void Start()
    {
        Vector3 euler = transform.rotation.eulerAngles;
        _yaw = euler.y;
        _pitch = euler.x;

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Vector2 look = _playerInputReader.LookValue;

        float mouseX = look.x;
        float mouseY = look.y;

        _yaw += mouseX * _sensitivity * Time.deltaTime;
        _pitch -= mouseY * _sensitivity * Time.deltaTime;
        _pitch = Mathf.Clamp(_pitch, _minPitch, _maxPitch);

        transform.rotation = Quaternion.Euler(_pitch, _yaw, 0f); 
    }
    #endregion
    

    
}