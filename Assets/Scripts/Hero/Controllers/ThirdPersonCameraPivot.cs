using UnityEngine;

public class ThirdPersonCameraPivot : MonoBehaviour
{
    [SerializeField] private Transform _cameraRoot;
    [SerializeField] private Transform _cameraPivot;
    [SerializeField] private float _mouseSensitivity = 3f;
    [SerializeField] private float _minPitch = -35f;
    [SerializeField] private float _maxPitch = 70f;

    private float _yaw;
    private float _pitch;

    public Transform CameraRoot => _cameraRoot;
    public Transform CameraPivot => _cameraPivot;

    private void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        _yaw += mouseX * _mouseSensitivity;
        _pitch -= mouseY * _mouseSensitivity;
        _pitch = Mathf.Clamp(_pitch, _minPitch, _maxPitch);

        if (_cameraRoot != null)
            _cameraRoot.rotation = Quaternion.Euler(0f, _yaw, 0f);

        if (_cameraPivot != null)
            _cameraPivot.localRotation = Quaternion.Euler(_pitch, 0f, 0f);
    }
}